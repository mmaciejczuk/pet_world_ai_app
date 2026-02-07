namespace PetWorld.Domain.Entities;

public sealed class Product
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public decimal PricePln { get; init; }
    public string Description { get; init; } = string.Empty;
}
