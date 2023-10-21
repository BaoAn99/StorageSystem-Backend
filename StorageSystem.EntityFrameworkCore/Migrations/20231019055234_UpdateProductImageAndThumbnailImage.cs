using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorageSystem.EntityFrameworkCore.Migrations
{
    public partial class UpdateProductImageAndThumbnailImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbnailImage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsImageFeature",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailImage",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsImageFeature",
                table: "ProductImages");
        }
    }
}
