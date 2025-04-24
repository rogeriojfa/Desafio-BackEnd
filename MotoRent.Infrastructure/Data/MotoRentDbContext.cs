using Microsoft.EntityFrameworkCore;
using MotoRent.Domain.Entities;

namespace MotoRent.Infrastructure.Data;

public class MotoRentDbContext : DbContext
{
    public MotoRentDbContext(DbContextOptions<MotoRentDbContext> options) : base(options) {}

    public DbSet<Moto> Motos { get; set; }
    public DbSet<Deliveryman> Deliverymen { get; set; }
    public DbSet<Rental> Rentals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Moto>()
            .HasIndex(m => m.Plate)
            .IsUnique();

        modelBuilder.Entity<Deliveryman>()
            .HasIndex(d => d.CNPJ)
            .IsUnique();

        modelBuilder.Entity<Deliveryman>()
            .HasIndex(d => d.CNH)
            .IsUnique();
    }
}
