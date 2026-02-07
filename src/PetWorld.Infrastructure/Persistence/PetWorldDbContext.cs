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
        modelBuilder.Entity<Product>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired();
            e.Property(x => x.Category).IsRequired();
            e.Property(x => x.Description).IsRequired();
            e.Property(x => x.PricePln).HasPrecision(10, 2);

            e.HasData(
                new Product { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Royal Canin Adult Dog 15kg", Category = "Karma dla psów", PricePln = 289m, Description = "Premium karma dla dorosłych psów średnich ras" },
                new Product { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Whiskas Adult Kurczak 7kg", Category = "Karma dla kotów", PricePln = 129m, Description = "Sucha karma dla dorosłych kotów z kurczakiem" },
                new Product { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Tetra AquaSafe 500ml", Category = "Akwarystyka", PricePln = 45m, Description = "Uzdatniacz wody do akwarium, neutralizuje chlor" },
                new Product { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Trixie Drapak XL 150cm", Category = "Akcesoria dla kotów", PricePln = 399m, Description = "Wysoki drapak z platformami i domkiem" },
                new Product { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Kong Classic Large", Category = "Zabawki dla psów", PricePln = 69m, Description = "Wytrzymała zabawka do napełniania smakołykami" },
                new Product { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), Name = "Ferplast Klatka dla chomika", Category = "Gryzonie", PricePln = 189m, Description = "Klatka 60x40cm z wyposażeniem" },
                new Product { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), Name = "Flexi Smycz automatyczna 8m", Category = "Akcesoria dla psów", PricePln = 119m, Description = "Smycz zwijana dla psów do 50kg" },
                new Product { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), Name = "Brit Premium Kitten 8kg", Category = "Karma dla kotów", PricePln = 159m, Description = "Karma dla kociąt do 12 miesiąca życia" },
                new Product { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Name = "JBL ProFlora CO2 Set", Category = "Akwarystyka", PricePln = 549m, Description = "Kompletny zestaw CO2 dla roślin akwariowych" },
                new Product { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "Vitapol Siano dla królików 1kg", Category = "Gryzonie", PricePln = 25m, Description = "Naturalne siano łąkowe, podstawa diety" }
            );
        });

        modelBuilder.Entity<ChatHistoryItem>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Question).IsRequired();
            e.Property(x => x.Answer).IsRequired();
            e.Property(x => x.CreatedAt).IsRequired();
        });
    }
}
