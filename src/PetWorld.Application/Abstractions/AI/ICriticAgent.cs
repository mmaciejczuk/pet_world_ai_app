using PetWorld.Domain.Entities;

namespace PetWorld.Application.Abstractions.AI;

public interface ICriticAgent
{
    Task<CriticVerdict> CriticizeAsync(
        string question,
        string answer,
        IReadOnlyList<Product> products,
        CancellationToken ct = default);
}
