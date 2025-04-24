using MongoDB.Driver;
using MotoRent.Application.Interfaces;
using MotoRent.Domain.Entities;

namespace MotoRent.Infrastructure.Repositories;

public class MotoRepository : IMotoRepository
{
    private readonly IMongoCollection<Moto> _motos;

    public MotoRepository(IMongoDatabase database)
    {
        _motos = database.GetCollection<Moto>("motos");
    }

    public Task AddAsync(Moto moto) => _motos.InsertOneAsync(moto);

    public async Task<Moto?> GetByLicensePlateAsync(string licensePlate)
    {
        return await _motos.Find(m => m.Plate == licensePlate).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Moto>> GetAllAsync()
    {
        return await _motos.Find(_ => true).ToListAsync();
    }

    public Task UpdatePlateAsync(Guid id, string newPlate) =>
        _motos.UpdateOneAsync(x => x.Id == id,
            Builders<Moto>.Update.Set(m => m.Plate, newPlate));

    public Task DeleteAsync(Guid id) =>
        _motos.DeleteOneAsync(x => x.Id == id);

    public Task<bool> HasAnyRentalAsync(Guid id)
    {
        // Simulação. Implementar integração com repositório de locações
        return Task.FromResult(false);
    }
}