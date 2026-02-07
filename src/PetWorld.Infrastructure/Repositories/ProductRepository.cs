using Microsoft.EntityFrameworkCore;
using PetWorld.Domain.Entities;
using PetWorld.Domain.Repositories;
using PetWorld.Infrastructure.Persistence;

namespace PetWorld.Infrastructure.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly PetWorldDbContext _db;

    public ProductRepository(PetWorldDbContext db) => _db = db;

    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken ct = default)
    {
        return await _db.Products
            .AsNoTracking()
            .OrderBy(p => p.Category)
            .ThenBy(p => p.Name)
            .ToListAsync(ct);
    }
}
