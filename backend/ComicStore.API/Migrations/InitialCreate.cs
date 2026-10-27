// =====================================================
// MIGRATION: InitialCreate
// File này được EF Core tự tạo khi chạy: dotnet ef migrations add InitialCreate
// ĐÂY LÀ FILE MẪU ĐỂ THAM KHẢO - EF sẽ tạo lại khi build
// =====================================================
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicStore.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    Genre = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CoverImage = table.Column<string>(type: "TEXT", nullable: true),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Comics", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerEmail = table.Column<string>(type: "TEXT", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Orders", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComicId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey("FK_OrderItems_Orders_OrderId", x => x.OrderId, "Orders", "Id", onDelete: ReferentialAction.Cascade);
                    table.ForeignKey("FK_OrderItems_Comics_ComicId", x => x.ComicId, "Comics", "Id", onDelete: ReferentialAction.Cascade);
                });

            // Seed data
            migrationBuilder.InsertData("Comics", new[] { "Id","Title","Author","Genre","Price","Stock","Description","CoverImage","CreatedAt" },
                new object[] { 1, "One Piece Vol.1", "Oda Eiichiro", "Action", 35000m, 50, "Cuoc hanh trinh cua Luffy", null, DateTime.UtcNow });
            migrationBuilder.InsertData("Comics", new[] { "Id","Title","Author","Genre","Price","Stock","Description","CoverImage","CreatedAt" },
                new object[] { 2, "Naruto Vol.1", "Masashi Kishimoto", "Action", 30000m, 30, "Nhat ky ninja", null, DateTime.UtcNow });
            migrationBuilder.InsertData("Comics", new[] { "Id","Title","Author","Genre","Price","Stock","Description","CoverImage","CreatedAt" },
                new object[] { 3, "Doraemon Vol.1", "Fujiko Fujio", "Comedy", 25000m, 100, "Meo may den tu tuong lai", null, DateTime.UtcNow });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "OrderItems");
            migrationBuilder.DropTable(name: "Orders");
            migrationBuilder.DropTable(name: "Comics");
        }
    }
}
