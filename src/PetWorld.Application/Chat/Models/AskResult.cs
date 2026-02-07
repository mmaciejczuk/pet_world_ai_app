namespace PetWorld.Application.Chat.Models;

public sealed class AskResult
{
    public string Answer { get; init; } = string.Empty;
    public int Iterations { get; init; }
}
