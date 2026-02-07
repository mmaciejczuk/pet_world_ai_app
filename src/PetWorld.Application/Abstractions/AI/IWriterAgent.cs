using PetWorld.Domain.Entities;

namespace PetWorld.Application.Abstractions.AI;

public interface IWriterAgent
{
    Task<WriterResponse> WriteAsync(
        string question,
        IReadOnlyList<Product> products,
        string? criticFeedback,
        CancellationToken ct = default);
}
