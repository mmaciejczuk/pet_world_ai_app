namespace PetWorld.Application.Abstractions.AI;

public sealed class CriticVerdict
{
    public bool Approved { get; init; }
    public string Feedback { get; init; } = string.Empty;
}
