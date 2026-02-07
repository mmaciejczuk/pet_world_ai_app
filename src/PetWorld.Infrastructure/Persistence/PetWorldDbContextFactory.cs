using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PetWorld.Infrastructure.Persistence;

// Używane TYLKO przez dotnet-ef, żeby nie wymagać działającego MySQL na dev machine.
public sealed class PetWorldDbContextFactory : IDesignTimeDbContextFactory<PetWorldDbContext>
{
    public PetWorldDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<PetWorldDbContext>()
            .UseSqlite("Data Source=petworld.design.db")
            .Options;

        return new PetWorldDbContext(options);
    }
}
