using MotoRent.Application.DTOs;
using MotoRent.Application.Interfaces.Service;
using MotoRent.Application.Interfaces;

public class DeliverymanService : IDeliverymanService
{
    private readonly IDeliverymanRepository _repository;

    public DeliverymanService(IDeliverymanRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeliverymanResponseDto> CreateAsync(CreateDeliverymanDto dto)
    {
        var deliveryman = new Deliveryman
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Cnpj = dto.Cnpj,
            BirthDate = dto.BirthDate,
            Cnh = dto.Cnh,
            CnhType = dto.CnhType,
            CnhImagePath = dto.CnhImagePath
        };

        await _repository.AddAsync(deliveryman);

        return MapToDto(deliveryman);
    }

    public async Task<IEnumerable<DeliverymanResponseDto>> GetAllAsync()
    {
        var deliverymen = await _repository.GetAllAsync();
        return deliverymen.Select(MapToDto);
    }

    public async Task<DeliverymanResponseDto> GetByIdAsync(Guid id)
    {
        var deliveryman = await _repository.GetByIdAsync(id);

        if (deliveryman == null)
            throw new KeyNotFoundException("Entregador não encontrado.");

        return MapToDto(deliveryman);
    }

    public async Task UpdateAsync(Guid id, UpdateDeliverymanDto dto)
    {
        var deliveryman = await _repository.GetByIdAsync(id);

        if (deliveryman == null)
            throw new KeyNotFoundException("Entregador não encontrado.");

        deliveryman.Name = dto.Name;
        deliveryman.BirthDate = dto.BirthDate;
        deliveryman.Cnh = dto.Cnh;
        deliveryman.CnhType = dto.CnhType;

        await _repository.UpdateCnhImagePathAsync(id, dto.CnhImagePath);
    }

    private static DeliverymanResponseDto MapToDto(Deliveryman deliveryman)
    {
        return new DeliverymanResponseDto
        {
            Id = deliveryman.Id,
            Name = deliveryman.Name,
            Cnpj = deliveryman.Cnpj,
            BirthDate = deliveryman.BirthDate,
            Cnh = deliveryman.Cnh,
            CnhType = deliveryman.CnhType,
            CnhImagePath = deliveryman.CnhImagePath
        };
    }
}
