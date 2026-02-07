using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PetWorld.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Question = table.Column<string>(type: "TEXT", nullable: false),
                    Answer = table.Column<string>(type: "TEXT", nullable: false),
                    Iterations = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    PricePln = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "Name", "PricePln" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Karma dla psów", "Premium karma dla dorosłych psów średnich ras", "Royal Canin Adult Dog 15kg", 289m },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Karma dla kotów", "Sucha karma dla dorosłych kotów z kurczakiem", "Whiskas Adult Kurczak 7kg", 129m },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Akwarystyka", "Uzdatniacz wody do akwarium, neutralizuje chlor", "Tetra AquaSafe 500ml", 45m },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Akcesoria dla kotów", "Wysoki drapak z platformami i domkiem", "Trixie Drapak XL 150cm", 399m },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Zabawki dla psów", "Wytrzymała zabawka do napełniania smakołykami", "Kong Classic Large", 69m },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Gryzonie", "Klatka 60x40cm z wyposażeniem", "Ferplast Klatka dla chomika", 189m },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "Akcesoria dla psów", "Smycz zwijana dla psów do 50kg", "Flexi Smycz automatyczna 8m", 119m },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "Karma dla kotów", "Karma dla kociąt do 12 miesiąca życia", "Brit Premium Kitten 8kg", 159m },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "Akwarystyka", "Kompletny zestaw CO2 dla roślin akwariowych", "JBL ProFlora CO2 Set", 549m },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Gryzonie", "Naturalne siano łąkowe, podstawa diety", "Vitapol Siano dla królików 1kg", 25m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatHistory");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
