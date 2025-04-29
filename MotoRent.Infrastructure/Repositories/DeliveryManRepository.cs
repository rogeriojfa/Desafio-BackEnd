using Microsoft.EntityFrameworkCore;
using MotoRent.Application.Interfaces;
using MotoRent.Infrastructure.Data;

public class DeliverymanRepository : IDeliverymanRepository
{
    private readonly MotoRentDbContext _context;

    public DeliverymanRepository(MotoRentDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Deliveryman deliveryman)
    {
        await _context.Deliverymen.AddAsync(deliveryman);
        await _context.SaveChangesAsync();
    }

    public async Task<Deliveryman?> GetByCnpjAsync(string cnpj)
    {
        return await _context.Deliverymen.FirstOrDefaultAsync(d => d.Cnpj == cnpj);
    }

    public async Task<Deliveryman?> GetByCnhAsync(string cnh)
    {
        return await _context.Deliverymen.FirstOrDefaultAsync(d => d.Cnh == cnh);
    }

    public async Task<Deliveryman?> GetByIdAsync(Guid id)
    {
        return await _context.Deliverymen.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<IEnumerable<Deliveryman>> GetAllAsync()
    {
        return await _context.Deliverymen.ToListAsync();
    }

    public async Task UpdateCnhImagePathAsync(Guid id, string imagePath)
    {
        var deliveryman = await _context.Deliverymen.FindAsync(id);
        if (deliveryman != null)
        {
            deliveryman.CnhImagePath = imagePath;
            await _context.SaveChangesAsync();
        }
    }
}
