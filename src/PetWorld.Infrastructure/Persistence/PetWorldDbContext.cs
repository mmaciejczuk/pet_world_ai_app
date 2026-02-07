using Microsoft.EntityFrameworkCore;
using PetWorld.Domain.Entities;

namespace PetWorld.Infrastructure.Persistence;

public sealed class PetWorldDbContext : DbContext
{
    public PetWorldDbContext(DbContextOptions<PetWorldDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<ChatHistoryItem> ChatHistory => Set<ChatHistoryItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(b =>
        {
            b.ToTable("products");
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).HasMaxLength(200).IsRequired();
            b.Property(x => x.Category).HasMaxLength(100).IsRequired();
            b.Property(x => x.Description).HasMaxLength(500).IsRequired();
            b.Property(x => x.PricePln).HasPrecision(10, 2);
            b.HasData(SeedData.Products);
        });

        modelBuilder.Entity<ChatHistoryItem>(b =>
        {
            b.ToTable("chat_history");
            b.HasKey(x => x.Id);
            b.Property(x => x.CreatedAt).IsRequired();
            b.Property(x => x.Question).HasMaxLength(2000).IsRequired();
            b.Property(x => x.Answer).HasMaxLength(8000).IsRequired();
            b.Property(x => x.Iterations).IsRequired();
        });
    }
}
