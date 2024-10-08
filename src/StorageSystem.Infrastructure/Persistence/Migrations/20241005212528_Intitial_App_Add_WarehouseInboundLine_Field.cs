using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorageSystem.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Intitial_App_Add_WarehouseInboundLine_Field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConvertQuantity",
                table: "WarehouseInboundLine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WarehouseInboundLine",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "NetPrice",
                table: "WarehouseInboundLine",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "WarehouseInboundLine",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "SmallestUnitName",
                table: "WarehouseInboundLine",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseInboundId",
                table: "WarehouseInboundLine",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "WarehouseInbound",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NetAmount",
                table: "WarehouseInbound",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseId",
                table: "WarehouseInbound",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInboundLine_WarehouseInboundId",
                table: "WarehouseInboundLine",
                column: "WarehouseInboundId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInbound_WarehouseId",
                table: "WarehouseInbound",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInbound_Warehouse_WarehouseId",
                table: "WarehouseInbound",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInboundLine_WarehouseInbound_WarehouseInboundId",
                table: "WarehouseInboundLine",
                column: "WarehouseInboundId",
                principalTable: "WarehouseInbound",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInbound_Warehouse_WarehouseId",
                table: "WarehouseInbound");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInboundLine_WarehouseInbound_WarehouseInboundId",
                table: "WarehouseInboundLine");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseInboundLine_WarehouseInboundId",
                table: "WarehouseInboundLine");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseInbound_WarehouseId",
                table: "WarehouseInbound");

            migrationBuilder.DropColumn(
                name: "ConvertQuantity",
                table: "WarehouseInboundLine");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "WarehouseInboundLine");

            migrationBuilder.DropColumn(
                name: "NetPrice",
                table: "WarehouseInboundLine");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "WarehouseInboundLine");

            migrationBuilder.DropColumn(
                name: "SmallestUnitName",
                table: "WarehouseInboundLine");

            migrationBuilder.DropColumn(
                name: "WarehouseInboundId",
                table: "WarehouseInboundLine");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "WarehouseInbound");

            migrationBuilder.DropColumn(
                name: "NetAmount",
                table: "WarehouseInbound");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "WarehouseInbound");
        }
    }
}
