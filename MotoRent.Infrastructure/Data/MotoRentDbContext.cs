using Microsoft.EntityFrameworkCore;
using MotoRent.Domain.Entities;

namespace MotoRent.Infrastructure.Data;

public class MotoRentDbContext : DbContext
{
    public MotoRentDbContext(DbContextOptions<MotoRentDbContext> options) : base(options) {}

    public DbSet<Moto> Motos { get; set; }
    public DbSet<Deliveryman> Deliverymen { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<User> Users { get; set; }

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

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = new Guid("a1f5a1d0-57d8-4a3c-9999-123456789abc"),
                Name = "Administrator",
                Email = "admin@motorrent.com",
                PasswordHash = "$2a$11$kt3sttPZjh6qXrlOL9UPh.JeigGW4LwMuJw5W87LfrMXDO.vqobhy",
                Role = Domain.Enums.UserRole.Admin
            }
        );
    }
}
