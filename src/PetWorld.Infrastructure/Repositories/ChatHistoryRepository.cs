using Microsoft.EntityFrameworkCore;
using PetWorld.Domain.Entities;
using PetWorld.Domain.Repositories;
using PetWorld.Infrastructure.Persistence;

namespace PetWorld.Infrastructure.Repositories;

public sealed class ChatHistoryRepository : IChatHistoryRepository
{
    private readonly PetWorldDbContext _db;

    public ChatHistoryRepository(PetWorldDbContext db) => _db = db;

    public async Task AddAsync(ChatHistoryItem item, CancellationToken ct = default)
    {
        _db.ChatHistory.Add(item);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<ChatHistoryItem>> GetLatestAsync(int take = 200, CancellationToken ct = default)
    {
        take = Math.Clamp(take, 1, 500);

        return await _db.ChatHistory
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .Take(take)
            .ToListAsync(ct);
    }
}
