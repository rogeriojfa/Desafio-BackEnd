using MotoRent.Application.DTOs;
using MotoRent.Domain.Entities;

namespace MotoRent.Application.Interfaces;

public interface IRentalService
{
    Task<Rental> CreateRentalAsync(Rental rental);
    Task<Rental?> FinishRentalAsync(Guid rentalId, DateTime realEndDate);
    Task<IEnumerable<Rental>> GetAllAsync();
    Task<Rental?> GetByIdAsync(Guid id);
    Task<Rental?> GetRentalByIdAsync(Guid id);
    Task<RentalResponse> CompleteRentalAsync(Guid rentalId, DateTime actualEndDate);
}
