using MotoRent.Application.Interfaces;
using MotoRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MotoRent.Infrastructure.Data;

namespace MotoRent.Infrastructure.Repositories;

public class RentalRepository : IRentalRepository
{
    private readonly MotoRentDbContext _context;

    public RentalRepository(MotoRentDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Rental rental)
    {
        await _context.Rentals.AddAsync(rental);
        await _context.SaveChangesAsync();
    }

    public async Task<Rental?> GetByIdAsync(Guid id)
    {
        return await _context.Rentals.FindAsync(id);
    }

    public async Task<IEnumerable<Rental>> GetAllAsync()
    {
        return await _context.Rentals.ToListAsync();
    }

    public async Task<IEnumerable<Rental>> GetByDeliverymanIdAsync(Guid deliverymanId)
    {
        return await _context.Rentals
            .Where(r => r.DeliverymanId == deliverymanId)
            .ToListAsync();
    }

    public async Task<Rental?> GetActiveRentalByMotoIdAsync(Guid motoId)
    {
        return await _context.Rentals
            .FirstOrDefaultAsync(r => r.MotoId == motoId && r.ActualEndDate == null);
    }

    public async Task UpdateAsync(Rental rental)
    {
        _context.Rentals.Update(rental);
        await _context.SaveChangesAsync();
    }
}
