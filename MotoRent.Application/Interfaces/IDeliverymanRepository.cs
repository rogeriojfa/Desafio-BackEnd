using MotoRent.Domain.Entities;

namespace MotoRent.Application.Interfaces;

public interface IDeliverymanRepository
{
    Task AddAsync(Deliveryman deliveryman);
    Task<Deliveryman?> GetByIdAsync(Guid id);
    Task<Deliveryman?> GetByCnpjAsync(string cnpj);
    Task<Deliveryman?> GetByCnhAsync(string cnhNumber);
    Task<IEnumerable<Deliveryman>> GetAllAsync();
    Task UpdateCnhImagePathAsync(Guid id, string imagePath);
}
