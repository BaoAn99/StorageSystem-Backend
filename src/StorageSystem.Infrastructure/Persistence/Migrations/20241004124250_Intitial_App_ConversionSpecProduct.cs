using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorageSystem.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Intitial_App_ConversionSpecProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConversionSpecProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConvertUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConvertUnitName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversionSpecProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversionSpecProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConversionSpecProduct_ConvertUnitId",
                table: "ConversionSpecProduct",
                column: "ConvertUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionSpecProduct_CreatedAt",
                table: "ConversionSpecProduct",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionSpecProduct_CreatedByUserId",
                table: "ConversionSpecProduct",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionSpecProduct_IsDeleted",
                table: "ConversionSpecProduct",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionSpecProduct_IsPublished",
                table: "ConversionSpecProduct",
                column: "IsPublished");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionSpecProduct_ProductId",
                table: "ConversionSpecProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionSpecProduct_UnitId",
                table: "ConversionSpecProduct",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionSpecProduct_UpdatedByUserId",
                table: "ConversionSpecProduct",
                column: "UpdatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConversionSpecProduct");
        }
    }
}
