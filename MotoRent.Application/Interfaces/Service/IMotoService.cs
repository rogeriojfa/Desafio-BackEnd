using MotoRent.Application.DTOs;
using MotoRent.Domain.Entities;

public interface IMotoService
    {
        Task<Moto?> GetByLicensePlateAsync(string licensePlate);
        Task<IEnumerable<Moto>> GetAllAsync();
        Task AddAsync(Moto moto);
        Task UpdatePlateAsync(Guid id, string newPlate);
        Task DeleteAsync(Guid id);
    }