namespace PetWorld.Domain.Entities;

public sealed class ChatHistoryItem
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Question { get; set; } = "";
    public string Answer { get; set; } = "";
    public int Iterations { get; set; }
}
