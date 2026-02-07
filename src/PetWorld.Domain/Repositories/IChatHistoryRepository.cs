using PetWorld.Domain.Entities;

namespace PetWorld.Domain.Repositories;

public interface IChatHistoryRepository
{
    Task AddAsync(ChatHistoryItem item, CancellationToken ct = default);
    Task<IReadOnlyList<ChatHistoryItem>> GetLatestAsync(int take = 200, CancellationToken ct = default);
}
