using MotoRent.Infrastructure.Repositories;

using MotoRent.Application.Interfaces;
using MassTransit;

using MotoRent.Application.Services;
using MotoRent.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🧩 Injeção de dependências
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<IMotoService, MotoService>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IRentalService, RentalService>();

builder.Services.AddDbContext<MotoRentDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

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
