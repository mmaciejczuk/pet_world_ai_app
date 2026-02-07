namespace PetWorld.Domain.Entities;

public sealed class ChatHistoryItem
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public string Question { get; init; } = string.Empty;
    public string Answer { get; init; } = string.Empty;

    public int Iterations { get; init; }
}
