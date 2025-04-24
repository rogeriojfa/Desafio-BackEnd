using MotoRent.Domain.Entities;

namespace MotoRent.Application.Interfaces
{
    public interface IMotoRepository
    {
        Task AddAsync(Moto moto);
        Task<Moto?> GetByLicensePlateAsync(string licensePlate);
        Task<IEnumerable<Moto>> GetAllAsync();
        Task UpdatePlateAsync(Guid id, string newPlate);
        Task DeleteAsync(Guid id);
        Task<bool> HasAnyRentalAsync(Guid id);
    }
}