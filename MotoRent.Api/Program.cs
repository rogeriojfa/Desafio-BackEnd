using MotoRent.Infrastructure.Repositories;
using MongoDB.Driver;
using MotoRent.Application.Interfaces;
using MassTransit;
using MotoRent.Infrastructure.Messaging.Consumers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MotoRent.Application.Services;


//BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3; // (padrão moderno)
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

var builder = WebApplication.CreateBuilder(args);

// 🔧 MongoDB
builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));

// 🧩 Injeção de dependências
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<IMotoService, MotoService>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IRentalService, RentalService>();


builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("motorent");
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
