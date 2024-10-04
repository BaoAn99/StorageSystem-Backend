using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorageSystem.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Intitial_App_ProductUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductUnit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_ProductUnit", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnit_CreatedAt",
                table: "ProductUnit",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnit_CreatedByUserId",
                table: "ProductUnit",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnit_IsDeleted",
                table: "ProductUnit",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnit_IsPublished",
                table: "ProductUnit",
                column: "IsPublished");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnit_UpdatedByUserId",
                table: "ProductUnit",
                column: "UpdatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductUnit");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductType");
        }
    }
}
