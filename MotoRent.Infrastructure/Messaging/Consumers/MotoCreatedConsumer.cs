using MassTransit;
using MotoRent.Application.Events;
using MotoRent.Application.Interfaces;
using MotoRent.Domain.Entities;

public class MotoCreatedConsumer : IConsumer<MotoCreatedEvent>
{
    private readonly IMotoRepository _motoRepository;

    public MotoCreatedConsumer(IMotoRepository motoRepository)
    {
        _motoRepository = motoRepository;
    }

    public async Task Consume(ConsumeContext<MotoCreatedEvent> context)
    {
        var motoCreatedEvent = context.Message;
        
        // Crie o novo objeto Moto a partir do evento
        var moto = new Moto
        {
            Id = motoCreatedEvent.Id,
            Plate = motoCreatedEvent.Plate,
            Model = motoCreatedEvent.Model,
            Year = motoCreatedEvent.Year
        };

        // Salve no repositório, que agora é PostgreSQL
        await _motoRepository.AddAsync(moto);
        
        // Caso precise, pode publicar outro evento ou realizar alguma ação adicional
        // Ex: _bus.Publish(new MotoCreatedConfirmationEvent());
    }
}
