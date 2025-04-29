using MotoRent.Domain.Entities;

namespace MotoRent.Application.Interfaces;

public interface IRentalRepository
{
    Task AddAsync(Rental rental);
    Task<Rental?> GetByIdAsync(Guid id);
    Task<IEnumerable<Rental>> GetAllAsync();
    Task<IEnumerable<Rental>> GetByDeliverymanIdAsync(Guid deliverymanId);
    Task<Rental?> GetActiveRentalByMotoIdAsync(Guid motoId);
    Task UpdateAsync(Rental rental);
}
