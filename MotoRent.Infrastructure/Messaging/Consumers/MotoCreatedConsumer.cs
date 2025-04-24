using MassTransit;
using MongoDB.Driver;
using MotoRent.Domain.Entities;
using MotoRent.Application.Events;

namespace MotoRent.Infrastructure.Messaging.Consumers;

public class MotoCreatedConsumer : IConsumer<MotoRegisteredEvent>
{
    private readonly IMongoCollection<Moto> _collection;

    public MotoCreatedConsumer(IMongoClient mongoClient)
    {
        var db = mongoClient.GetDatabase("motorent");
        _collection = db.GetCollection<Moto>("motos2024"); // salva separado
    }

    public async Task Consume(ConsumeContext<MotoRegisteredEvent> context)
    {
        var message = context.Message;

        if (message.Year == 2024)
        {
            var moto = new Moto
            {
                Id = message.Id,
                Year = message.Year,
                Model = message.Model,
                Plate = message.Plate
            };

            await _collection.InsertOneAsync(moto);
        }
    }
}
