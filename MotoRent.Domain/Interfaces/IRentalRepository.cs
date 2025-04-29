using MotoRent.Domain.Entities;

namespace MotoRent.Domain.Interfaces.Repositories
{
    public interface IRentalRepository
    {
        Task<Rental> GetByIdAsync(Guid id);
        Task<IEnumerable<Rental>> GetAllAsync();
        Task<IEnumerable<Rental>> GetByDeliverymanIdAsync(Guid deliverymanId);
        Task<IEnumerable<Rental>> GetByMotoIdAsync(Guid motoId);
        Task AddAsync(Rental rental);
        Task UpdateAsync(Rental rental);
        Task DeleteAsync(Guid id);
    }
}
