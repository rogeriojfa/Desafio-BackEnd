using MotoRent.Infrastructure.Repositories;
using MotoRent.Application.Interfaces;
using MassTransit;
using MotoRent.Application.Services;
using MotoRent.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MotoRent.Services;
using MotoRent.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MotoRent.Application.Interfaces.Service;


var builder = WebApplication.CreateBuilder(args);

// 🧩 Injeção de dependências
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<IMotoService, MotoService>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IDeliverymanRepository, DeliverymanRepository>();
builder.Services.AddScoped<IDeliverymanService, DeliverymanService>();

builder.Services.AddDbContext<MotoRentDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<MotoRentDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var settings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>() ?? throw new InvalidOperationException("JWT settings not found in configuration.");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = settings.Issuer,
            ValidAudience = settings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey))
        };
    });
// 🔧 Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<MotoCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        var settings = builder.Configuration.GetSection("RabbitMq");

        cfg.Host(settings["Host"], "/", h =>
        {
            h.Username(settings["Username"]);
            h.Password(settings["Password"]);
        });

        cfg.ReceiveEndpoint(settings["Queue"], e =>
        {
            e.ConfigureConsumer<MotoCreatedConsumer>(context);
        });
    });
});


// 🚀 Aplicação
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();
