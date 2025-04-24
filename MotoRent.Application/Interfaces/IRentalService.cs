using MotoRent.Domain.Entities;

namespace MotoRent.Application.Interfaces;

public interface IRentalService
{
    Task<Rental> CreateAsync(Rental rental);
    Task<Rental?> FinishRentalAsync(Guid rentalId, DateTime realEndDate);
    Task<IEnumerable<Rental>> GetAllAsync();
    Task<Rental?> GetByIdAsync(Guid id);
}
