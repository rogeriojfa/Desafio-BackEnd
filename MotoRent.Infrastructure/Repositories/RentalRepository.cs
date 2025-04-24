using MongoDB.Driver;
using MotoRent.Domain.Entities;
using MotoRent.Application.Interfaces;

namespace MotoRent.Infrastructure.Repositories;

public class RentalRepository : IRentalRepository
{
    private readonly IMongoCollection<Rental> _rentals;

    public RentalRepository(IMongoClient client)
    {
        var db = client.GetDatabase("motorent");
        _rentals = db.GetCollection<Rental>("rentals");
    }

    public Task AddAsync(Rental rental) =>
        _rentals.InsertOneAsync(rental);

    public async Task<Rental?> GetByIdAsync(Guid id) =>
        await _rentals.Find(x => x.Id == id).FirstOrDefaultAsync();

    public Task<IEnumerable<Rental>> GetAllAsync() =>
        Task.FromResult(_rentals.Find(_ => true).ToEnumerable());

    public Task UpdateAsync(Rental rental) =>
        _rentals.ReplaceOneAsync(x => x.Id == rental.Id, rental);
}
