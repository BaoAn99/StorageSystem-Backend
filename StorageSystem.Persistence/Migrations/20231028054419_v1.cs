using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorageSystem.Persistence.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StorageSystem.Persistence.Contracts.IApplicationDbContext.Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageSystem.Persistence.Contracts.IApplicationDbContext.Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StorageSystem.Persistence.Contracts.IApplicationDbContext.Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OriginalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ThumbnailImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageSystem.Persistence.Contracts.IApplicationDbContext.Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorageSystem.Persistence.Contracts.IApplicationDbContext.Products_StorageSystem.Persistence.Contracts.IApplicationDbContext~",
                        column: x => x.CategoryId,
                        principalTable: "StorageSystem.Persistence.Contracts.IApplicationDbContext.Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StorageSystem.Persistence.Contracts.IApplicationDbContext.ProductImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsImageFeature = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageSystem.Persistence.Contracts.IApplicationDbContext.ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorageSystem.Persistence.Contracts.IApplicationDbContext.ProductImages_StorageSystem.Persistence.Contracts.IApplicationDbCo~",
                        column: x => x.ProductId,
                        principalTable: "StorageSystem.Persistence.Contracts.IApplicationDbContext.Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StorageSystem.Persistence.Contracts.IApplicationDbContext.ProductImages_ProductId",
                table: "StorageSystem.Persistence.Contracts.IApplicationDbContext.ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageSystem.Persistence.Contracts.IApplicationDbContext.Products_CategoryId",
                table: "StorageSystem.Persistence.Contracts.IApplicationDbContext.Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StorageSystem.Persistence.Contracts.IApplicationDbContext.ProductImages");

            migrationBuilder.DropTable(
                name: "StorageSystem.Persistence.Contracts.IApplicationDbContext.Products");

            migrationBuilder.DropTable(
                name: "StorageSystem.Persistence.Contracts.IApplicationDbContext.Categories");
        }
    }
}
