using MotoRent.Application.DTOs;
using MotoRent.Domain.Entities;
using static MassTransit.ValidationResultExtensions;

namespace MotoRent.Application.Interfaces;

public interface IRentalService
{
    Task<Rental> CreateRentalAsync(Rental rental);
    Task<Result> FinishRentalAsync(Guid rentalId);
    Task<IEnumerable<Rental>> GetAllAsync();
    Task<Rental?> GetRentalByIdAsync(Guid id);
    Task<RentalResponse> CompleteRentalAsync(Guid rentalId, DateTime actualEndDate);
}
