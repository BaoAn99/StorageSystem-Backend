using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorageSystem.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Intitial_App_Add_Product_Field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SmallestUnitId",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Product_SmallestUnitId",
                table: "Product",
                column: "SmallestUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductUnit_SmallestUnitId",
                table: "Product",
                column: "SmallestUnitId",
                principalTable: "ProductUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductUnit_SmallestUnitId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_SmallestUnitId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SmallestUnitId",
                table: "Product");
        }
    }
}
