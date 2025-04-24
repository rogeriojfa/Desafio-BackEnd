using Microsoft.EntityFrameworkCore;
using MotoRent.Application.Interfaces;
using MotoRent.Domain.Entities;
using MotoRent.Infrastructure.Data;

public class MotoRepository : IMotoRepository
{
    private readonly MotoRentDbContext _context;

    public MotoRepository(MotoRentDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Moto moto)
    {
        await _context.Motos.AddAsync(moto);
        await _context.SaveChangesAsync();
    }

    public async Task<Moto?> GetByLicensePlateAsync(string licensePlate)
    {
        return await _context.Motos.FirstOrDefaultAsync(m => m.Plate == licensePlate);
    }

    public async Task<IEnumerable<Moto>> GetAllAsync()
    {
        return await _context.Motos.ToListAsync();
    }

    public async Task UpdatePlateAsync(Guid id, string newPlate)
    {
        var moto = await _context.Motos.FindAsync(id);
        if (moto != null)
        {
            moto.Plate = newPlate;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var moto = await _context.Motos.FindAsync(id);
        if (moto != null)
        {
            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> HasAnyRentalAsync(Guid id)
    {
        return await _context.Rentals.AnyAsync(r => r.MotoId == id);
    }

    public async Task<Moto?> GetByIdAsync(string id)
    {
        return await _context.Motos.FirstOrDefaultAsync(m => m.Id.ToString() == id);
    }
}
