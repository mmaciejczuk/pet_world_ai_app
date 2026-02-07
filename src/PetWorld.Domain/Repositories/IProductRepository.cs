using PetWorld.Domain.Entities;

namespace PetWorld.Domain.Repositories;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken ct = default);
}
