using MotoRent.Domain.Entities;

public interface IMotoRepository
{
    Task AddAsync(Moto moto);
    Task<Moto?> GetByLicensePlateAsync(string plate);
    Task<Moto?> GetByIdAsync(Guid id);
    Task<IEnumerable<Moto>> GetAllAsync();
    Task UpdatePlateAsync(Guid id, string newPlate);
    Task DeleteAsync(Guid id);
    Task<bool> HasAnyRentalAsync(Guid id);
    Task UpdateAsync(Moto moto);
}
