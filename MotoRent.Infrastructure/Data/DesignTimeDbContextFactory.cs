using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MotoRent.Infrastructure.Data;

namespace MotoRent.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MotoRentDbContext>
{
    public MotoRentDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MotoRentDbContext>();
        
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=motorentdb;Username=postgres;Password=postgres");

        return new MotoRentDbContext(optionsBuilder.Options);
    }
}
