using Microsoft.EntityFrameworkCore.Migrations;

namespace INFRASTRUCTURE.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "ProductBrands",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_ProductBrands", x => x.Id); });

            migrationBuilder.CreateTable(
                "ProductTypes",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_ProductTypes", x => x.Id); });

            migrationBuilder.CreateTable(
                "Products",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>("TEXT", maxLength: 180, nullable: false),
                    Price = table.Column<decimal>("decimal(18,2)", nullable: false),
                    PictureUrl = table.Column<string>("TEXT", nullable: false),
                    ProductTypeId = table.Column<int>("INTEGER", nullable: false),
                    ProductBrandId = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        "FK_Products_ProductBrands_ProductBrandId",
                        x => x.ProductBrandId,
                        "ProductBrands",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Products_ProductTypes_ProductTypeId",
                        x => x.ProductTypeId,
                        "ProductTypes",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Products_ProductBrandId",
                "Products",
                "ProductBrandId");

            migrationBuilder.CreateIndex(
                "IX_Products_ProductTypeId",
                "Products",
                "ProductTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Products");

            migrationBuilder.DropTable(
                "ProductBrands");

            migrationBuilder.DropTable(
                "ProductTypes");
        }
    }
}