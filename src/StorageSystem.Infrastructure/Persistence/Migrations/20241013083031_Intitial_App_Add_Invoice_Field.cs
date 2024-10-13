using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorageSystem.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Intitial_App_Add_Invoice_Field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OldInvoiceId",
                table: "Invoice",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_OldInvoiceId",
                table: "Invoice",
                column: "OldInvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invoice_OldInvoiceId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "OldInvoiceId",
                table: "Invoice");
        }
    }
}
