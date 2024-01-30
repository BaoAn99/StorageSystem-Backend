using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorageSystem.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    OriginalPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ThumbnailImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deposit = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coupons_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StatusOrder = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId1",
                        column: x => x.CustomerId1,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsImageFeature = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSuppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSuppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSuppliers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSuppliers_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductUnits_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductUnits_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillDetails_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CouponDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CouponId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CouponDetails_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CouponDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DateCreated", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2648), new TimeSpan(0, 7, 0, 0, 0)), false, "Vật liệu xây dựng" },
                    { new Guid("a4533b2f-1174-4587-8fe8-3333295cc0ac"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2665), new TimeSpan(0, 7, 0, 0, 0)), false, "Vật phẩm trang trí" },
                    { new Guid("cf846c38-b43c-494d-bbbf-838a65ec3299"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2658), new TimeSpan(0, 7, 0, 0, 0)), false, "Đồ điện" },
                    { new Guid("de6b4a9e-1d88-48db-b21d-cfb3ee139df9"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2662), new TimeSpan(0, 7, 0, 0, 0)), false, "Đồ gia dụng" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "DateCreated", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("4b703409-a32a-49cf-9943-d5a68dbedd6f"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2789), new TimeSpan(0, 7, 0, 0, 0)), false, "Thùng" },
                    { new Guid("54cb56aa-a710-47e0-a386-4cc493d46747"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2814), new TimeSpan(0, 7, 0, 0, 0)), false, "Hộp" },
                    { new Guid("bb7b0969-4383-446f-8ab9-6d766df08359"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2804), new TimeSpan(0, 7, 0, 0, 0)), false, "Kg" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "DateCreated", "Description", "IsDeleted", "Name", "OriginalPrice", "Price", "ThumbnailImage" },
                values: new object[,]
                {
                    { new Guid("0a5b4248-5d36-45aa-89fc-dd913824d34e"), new Guid("de6b4a9e-1d88-48db-b21d-cfb3ee139df9"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2885), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Emma Adams", 110000m, 414000m, "https://placewaifu.com/image/89" },
                    { new Guid("0de34a77-a035-4eac-8f1b-5f21ebef4569"), new Guid("cf846c38-b43c-494d-bbbf-838a65ec3299"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2852), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Jane Fisher", 110000m, 407000m, "https://placewaifu.com/image/82" },
                    { new Guid("163bd018-c22a-49e5-adb0-2ca2942bc420"), new Guid("de6b4a9e-1d88-48db-b21d-cfb3ee139df9"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2857), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "William Howard", 110000m, 408000m, "https://placewaifu.com/image/83" },
                    { new Guid("19400bf1-1da8-4a52-a515-4c36577bc98b"), new Guid("cf846c38-b43c-494d-bbbf-838a65ec3299"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2891), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Megan Richards", 110000m, 416000m, "https://placewaifu.com/image/91" },
                    { new Guid("1a1f7026-eb10-4632-ba16-c12bbea207b7"), new Guid("a4533b2f-1174-4587-8fe8-3333295cc0ac"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2862), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Kristen Copper", 110000m, 409000m, "https://placewaifu.com/image/84" },
                    { new Guid("3238c0b8-d4a3-4cfb-9167-695ef4224b6b"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2848), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Zoey Lang", 110000m, 406000m, "https://placewaifu.com/image/81" },
                    { new Guid("484c3f38-bfe0-42b9-8e9c-f30f1aa86fd3"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2841), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Tony Reichert", 110000m, 405000m, "https://placewaifu.com/image/80" },
                    { new Guid("5d01611b-e2ad-4abd-b40e-0450f50d2ec9"), new Guid("cf846c38-b43c-494d-bbbf-838a65ec3299"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2895), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Oliver Scott", 110000m, 305000m, "https://placewaifu.com/image/92" },
                    { new Guid("6a9955a7-5c78-40fa-8020-174f1018f797"), new Guid("a4533b2f-1174-4587-8fe8-3333295cc0ac"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2909), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Noah Carter", 110000m, 406000m, "https://placewaifu.com/image/96" },
                    { new Guid("9089f19c-473d-40b1-b7b3-e0f5f6c8f8df"), new Guid("a4533b2f-1174-4587-8fe8-3333295cc0ac"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2901), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Noah Carter", 110000m, 307000m, "https://placewaifu.com/image/94" },
                    { new Guid("9244f89d-1b5d-44cd-8a7f-1922d464e336"), new Guid("a4533b2f-1174-4587-8fe8-3333295cc0ac"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2966), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Ava Perez", 110000m, 300000m, "https://placewaifu.com/image/97" },
                    { new Guid("97aac145-947d-4da4-8191-a2a3faa81fce"), new Guid("de6b4a9e-1d88-48db-b21d-cfb3ee139df9"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2882), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Frank Harrison", 110000m, 413000m, "https://placewaifu.com/image/88" },
                    { new Guid("c861791b-bce9-47a9-84c7-1abdb578d88b"), new Guid("a4533b2f-1174-4587-8fe8-3333295cc0ac"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2867), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Brian Kim", 110000m, 410000m, "https://placewaifu.com/image/85" },
                    { new Guid("cff0eb9e-3b50-4ade-9f56-cb0e4ab4d617"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2898), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Grace Allen", 110000m, 200000m, "https://placewaifu.com/image/93" },
                    { new Guid("d0d58f20-abe1-4f54-8e44-50e1d226ead4"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2871), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Michael Hunt", 110000m, 411000m, "https://placewaifu.com/image/86" },
                    { new Guid("da3156b8-3324-4684-8789-9af51316865b"), new Guid("de6b4a9e-1d88-48db-b21d-cfb3ee139df9"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2888), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Brandon Stevens", 110000m, 415000m, "https://placewaifu.com/image/90" },
                    { new Guid("dcbeea7e-4675-4a01-b59a-a9e1f3e5f4b4"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2875), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Samantha Brooks", 110000m, 412000m, "https://placewaifu.com/image/87" },
                    { new Guid("e9416abf-ab8a-4b69-8bb5-3f3a8b7024ad"), new Guid("a4533b2f-1174-4587-8fe8-3333295cc0ac"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2962), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Ava Perez", 110000m, 405000m, "https://placewaifu.com/image/97" },
                    { new Guid("f35c176b-c70a-4fb5-b9b3-885eb8731901"), new Guid("de6b4a9e-1d88-48db-b21d-cfb3ee139df9"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2912), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Ava Perez", 110000m, 417000m, "https://placewaifu.com/image/97" },
                    { new Guid("f9ac3e9c-da9e-4aba-8a12-291a3655864b"), new Guid("de6b4a9e-1d88-48db-b21d-cfb3ee139df9"), new DateTimeOffset(new DateTime(2024, 1, 30, 19, 15, 29, 323, DateTimeKind.Unspecified).AddTicks(2904), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Ava Perez", 110000m, 405000m, "https://placewaifu.com/image/95" }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "Caption", "ImagePath", "IsImageFeature", "ProductId" },
                values: new object[,]
                {
                    { new Guid("09084a61-12ed-49b1-aa39-e33015d8b544"), "Caption", "https://placewaifu.com/image/91", false, new Guid("9089f19c-473d-40b1-b7b3-e0f5f6c8f8df") },
                    { new Guid("09b46007-e91e-4935-be97-530f82143c3d"), "Caption", "https://placewaifu.com/image/96", true, new Guid("6a9955a7-5c78-40fa-8020-174f1018f797") },
                    { new Guid("0d70e2fa-632a-4bb9-9d74-60eb600d2e34"), "Caption", "https://placewaifu.com/image/80", true, new Guid("484c3f38-bfe0-42b9-8e9c-f30f1aa86fd3") },
                    { new Guid("0d93bbf7-6e58-4eac-a827-bb77eea088df"), "Caption", "https://placewaifu.com/image/92", false, new Guid("97aac145-947d-4da4-8191-a2a3faa81fce") },
                    { new Guid("11770058-d059-4dea-9edf-890a1e672285"), "Caption", "https://placewaifu.com/image/91", false, new Guid("0de34a77-a035-4eac-8f1b-5f21ebef4569") },
                    { new Guid("127114cc-6332-4ff9-8194-9c640d10e1db"), "Caption", "https://placewaifu.com/image/91", true, new Guid("19400bf1-1da8-4a52-a515-4c36577bc98b") },
                    { new Guid("1ee23024-f48c-4d4c-a27f-9a90eca9d136"), "Caption", "https://placewaifu.com/image/89", true, new Guid("0a5b4248-5d36-45aa-89fc-dd913824d34e") },
                    { new Guid("2066dfc9-3543-4471-8820-1d024afbb8fd"), "Caption", "https://placewaifu.com/image/97", true, new Guid("9244f89d-1b5d-44cd-8a7f-1922d464e336") },
                    { new Guid("28f952fa-5a43-41a3-879b-af279ec7d111"), "Caption", "https://placewaifu.com/image/93", false, new Guid("9244f89d-1b5d-44cd-8a7f-1922d464e336") },
                    { new Guid("28ff4f76-994a-4af3-9fb1-70c38ca075fd"), "Caption", "https://placewaifu.com/image/91", false, new Guid("f35c176b-c70a-4fb5-b9b3-885eb8731901") },
                    { new Guid("2c93c143-3657-460a-8af0-c63077d4d4b4"), "Caption", "https://placewaifu.com/image/91", false, new Guid("f9ac3e9c-da9e-4aba-8a12-291a3655864b") },
                    { new Guid("2dec6b16-d280-47d7-be3e-45ca5ab56711"), "Caption", "https://placewaifu.com/image/91", false, new Guid("6a9955a7-5c78-40fa-8020-174f1018f797") },
                    { new Guid("2e150789-5f8c-46d7-a396-55ac76950fd1"), "Caption", "https://placewaifu.com/image/84", true, new Guid("1a1f7026-eb10-4632-ba16-c12bbea207b7") },
                    { new Guid("30a59c82-f9c4-4717-ba0a-e38feecb35fc"), "Caption", "https://placewaifu.com/image/93", false, new Guid("f9ac3e9c-da9e-4aba-8a12-291a3655864b") },
                    { new Guid("3148ad2b-cc52-4b66-b4bd-1e937c73b9c4"), "Caption", "https://placewaifu.com/image/92", false, new Guid("5d01611b-e2ad-4abd-b40e-0450f50d2ec9") },
                    { new Guid("31b7bc68-a438-4171-9f96-6140dc90998e"), "Caption", "https://placewaifu.com/image/88", true, new Guid("97aac145-947d-4da4-8191-a2a3faa81fce") },
                    { new Guid("34714690-11f5-4284-b061-bd8a268413ef"), "Caption", "https://placewaifu.com/image/91", false, new Guid("9244f89d-1b5d-44cd-8a7f-1922d464e336") },
                    { new Guid("352b1f53-1a8e-44c1-8998-b312f5b98cd9"), "Caption", "https://placewaifu.com/image/93", false, new Guid("1a1f7026-eb10-4632-ba16-c12bbea207b7") },
                    { new Guid("367a882c-b915-4bd7-9e6e-1079127de2c5"), "Caption", "https://placewaifu.com/image/92", false, new Guid("3238c0b8-d4a3-4cfb-9167-695ef4224b6b") },
                    { new Guid("36bfe868-bc33-42c4-9a9c-2c97c22c2e98"), "Caption", "https://placewaifu.com/image/91", false, new Guid("97aac145-947d-4da4-8191-a2a3faa81fce") },
                    { new Guid("3946bc3b-46ba-42b9-b2f8-f5486d137716"), "Caption", "https://placewaifu.com/image/90", true, new Guid("da3156b8-3324-4684-8789-9af51316865b") },
                    { new Guid("3ddf6fe4-6fe9-4013-83c4-9de112189e10"), "Caption", "https://placewaifu.com/image/93", false, new Guid("5d01611b-e2ad-4abd-b40e-0450f50d2ec9") },
                    { new Guid("424d2af5-a9b8-4c42-8680-d1e0c3e1a575"), "Caption", "https://placewaifu.com/image/93", false, new Guid("0a5b4248-5d36-45aa-89fc-dd913824d34e") },
                    { new Guid("44c0aaa5-8a9f-44c2-9339-842720f7c507"), "Caption", "https://placewaifu.com/image/91", false, new Guid("cff0eb9e-3b50-4ade-9f56-cb0e4ab4d617") },
                    { new Guid("477cc35b-5903-4c15-a3d2-8f3f31d0f1ec"), "Caption", "https://placewaifu.com/image/93", false, new Guid("da3156b8-3324-4684-8789-9af51316865b") },
                    { new Guid("48e09370-4e1b-4465-8f41-ffde199d4fcb"), "Caption", "https://placewaifu.com/image/92", false, new Guid("484c3f38-bfe0-42b9-8e9c-f30f1aa86fd3") },
                    { new Guid("4b5b02eb-7856-4c6b-a4cc-1a777498b123"), "Caption", "https://placewaifu.com/image/87", true, new Guid("dcbeea7e-4675-4a01-b59a-a9e1f3e5f4b4") },
                    { new Guid("4ec7b63d-242d-4359-b544-c9b83b421e56"), "Caption", "https://placewaifu.com/image/92", false, new Guid("9089f19c-473d-40b1-b7b3-e0f5f6c8f8df") },
                    { new Guid("531f5f7d-f6e0-405b-99ae-10f8c7d724e4"), "Caption", "https://placewaifu.com/image/93", false, new Guid("e9416abf-ab8a-4b69-8bb5-3f3a8b7024ad") },
                    { new Guid("5458d0ae-2789-4db8-bcef-770f2c582d95"), "Caption", "https://placewaifu.com/image/92", false, new Guid("da3156b8-3324-4684-8789-9af51316865b") },
                    { new Guid("5461b7bc-4484-4678-9277-0dababb5b5c1"), "Caption", "https://placewaifu.com/image/92", false, new Guid("f9ac3e9c-da9e-4aba-8a12-291a3655864b") },
                    { new Guid("57b346d8-5da5-4fa1-b202-8bdeb4424a03"), "Caption", "https://placewaifu.com/image/93", false, new Guid("d0d58f20-abe1-4f54-8e44-50e1d226ead4") },
                    { new Guid("5d50baa1-34c3-41a9-8f6b-8f68854ebbea"), "Caption", "https://placewaifu.com/image/93", false, new Guid("c861791b-bce9-47a9-84c7-1abdb578d88b") },
                    { new Guid("60c4eb56-8500-4512-a499-baf5fefbd2f7"), "Caption", "https://placewaifu.com/image/86", true, new Guid("d0d58f20-abe1-4f54-8e44-50e1d226ead4") },
                    { new Guid("65aba86d-89dc-490e-94d5-2be685c9794c"), "Caption", "https://placewaifu.com/image/85", true, new Guid("c861791b-bce9-47a9-84c7-1abdb578d88b") },
                    { new Guid("6626b486-2c91-4ee2-9595-a91ff1753b86"), "Caption", "https://placewaifu.com/image/92", false, new Guid("e9416abf-ab8a-4b69-8bb5-3f3a8b7024ad") },
                    { new Guid("6e79aed1-3bd0-4232-ab12-af720fbac013"), "Caption", "https://placewaifu.com/image/92", true, new Guid("5d01611b-e2ad-4abd-b40e-0450f50d2ec9") },
                    { new Guid("723b90f8-d187-42f1-b3b8-402b1ccc5610"), "Caption", "https://placewaifu.com/image/93", false, new Guid("9089f19c-473d-40b1-b7b3-e0f5f6c8f8df") },
                    { new Guid("7458a76d-8b2d-443f-8e7c-9240a341bbca"), "Caption", "https://placewaifu.com/image/91", false, new Guid("5d01611b-e2ad-4abd-b40e-0450f50d2ec9") },
                    { new Guid("773c37d8-3cde-47dc-8422-5d43be71e140"), "Caption", "https://placewaifu.com/image/92", false, new Guid("1a1f7026-eb10-4632-ba16-c12bbea207b7") },
                    { new Guid("77d6ec4a-ff6c-4651-9aa8-8fe0dabf9dec"), "Caption", "https://placewaifu.com/image/93", false, new Guid("97aac145-947d-4da4-8191-a2a3faa81fce") },
                    { new Guid("7cb52260-78f7-4b8d-b1a3-ded6cf07e521"), "Caption", "https://placewaifu.com/image/82", true, new Guid("0de34a77-a035-4eac-8f1b-5f21ebef4569") }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "Caption", "ImagePath", "IsImageFeature", "ProductId" },
                values: new object[,]
                {
                    { new Guid("7d902e49-3858-4b12-8300-79fda12bcaf4"), "Caption", "https://placewaifu.com/image/92", false, new Guid("d0d58f20-abe1-4f54-8e44-50e1d226ead4") },
                    { new Guid("7fbdcd3a-9b73-419b-9a35-6dff09c4950e"), "Caption", "https://placewaifu.com/image/93", false, new Guid("cff0eb9e-3b50-4ade-9f56-cb0e4ab4d617") },
                    { new Guid("84327376-5b8c-468e-a009-b7c2d4ff9a06"), "Caption", "https://placewaifu.com/image/93", false, new Guid("19400bf1-1da8-4a52-a515-4c36577bc98b") },
                    { new Guid("9274a6ff-c84e-46a3-86bf-1ccf25b55990"), "Caption", "https://placewaifu.com/image/93", false, new Guid("f35c176b-c70a-4fb5-b9b3-885eb8731901") },
                    { new Guid("97b9c89c-2ab2-4cf0-867c-f1d2f89a9da9"), "Caption", "https://placewaifu.com/image/93", false, new Guid("6a9955a7-5c78-40fa-8020-174f1018f797") },
                    { new Guid("98c068aa-3d4e-4a5e-b4ec-3e8e81adb140"), "Caption", "https://placewaifu.com/image/93", false, new Guid("0de34a77-a035-4eac-8f1b-5f21ebef4569") },
                    { new Guid("9fc53e32-b914-4766-b6e0-e6372fd3fccd"), "Caption", "https://placewaifu.com/image/93", false, new Guid("484c3f38-bfe0-42b9-8e9c-f30f1aa86fd3") },
                    { new Guid("9fd65472-1769-4bc1-b901-1901919a33d0"), "Caption", "https://placewaifu.com/image/91", false, new Guid("3238c0b8-d4a3-4cfb-9167-695ef4224b6b") },
                    { new Guid("a47c5a32-4726-468c-bf4d-953943b883c5"), "Caption", "https://placewaifu.com/image/91", false, new Guid("e9416abf-ab8a-4b69-8bb5-3f3a8b7024ad") },
                    { new Guid("a787402b-d851-408b-90f2-6e3703ccdc17"), "Caption", "https://placewaifu.com/image/91", false, new Guid("c861791b-bce9-47a9-84c7-1abdb578d88b") },
                    { new Guid("a962caf3-7b4c-47dd-bbb5-42fe669ac0af"), "Caption", "https://placewaifu.com/image/92", false, new Guid("cff0eb9e-3b50-4ade-9f56-cb0e4ab4d617") },
                    { new Guid("b4b6a00f-a32c-4fcc-a101-01f1995d29eb"), "Caption", "https://placewaifu.com/image/91", false, new Guid("484c3f38-bfe0-42b9-8e9c-f30f1aa86fd3") },
                    { new Guid("b56aab3a-e291-468a-81d2-c4caeafc71ae"), "Caption", "https://placewaifu.com/image/93", false, new Guid("163bd018-c22a-49e5-adb0-2ca2942bc420") },
                    { new Guid("bd7141a2-4a00-463e-9519-e0c8124a051f"), "Caption", "https://placewaifu.com/image/93", true, new Guid("cff0eb9e-3b50-4ade-9f56-cb0e4ab4d617") },
                    { new Guid("be396d5c-a51e-4c31-9a91-78ccf237c469"), "Caption", "https://placewaifu.com/image/83", true, new Guid("163bd018-c22a-49e5-adb0-2ca2942bc420") },
                    { new Guid("bef6dc99-cfbd-4bdf-a1be-1c1e5a6f8eec"), "Caption", "https://placewaifu.com/image/93", false, new Guid("3238c0b8-d4a3-4cfb-9167-695ef4224b6b") },
                    { new Guid("bf8e4c50-917e-4b81-aa68-91995fe886be"), "Caption", "https://placewaifu.com/image/91", false, new Guid("1a1f7026-eb10-4632-ba16-c12bbea207b7") },
                    { new Guid("cb25729f-8388-48e1-b090-fa6489d36a14"), "Caption", "https://placewaifu.com/image/92", false, new Guid("163bd018-c22a-49e5-adb0-2ca2942bc420") },
                    { new Guid("ccae4843-4f33-4dfd-a928-0bf1009e5185"), "Caption", "https://placewaifu.com/image/92", false, new Guid("6a9955a7-5c78-40fa-8020-174f1018f797") },
                    { new Guid("cf897f87-01c9-4ce2-908d-89082daadda6"), "Caption", "https://placewaifu.com/image/92", false, new Guid("19400bf1-1da8-4a52-a515-4c36577bc98b") },
                    { new Guid("d26d575d-90eb-485c-b6a0-3bb7179a91d1"), "Caption", "https://placewaifu.com/image/91", false, new Guid("d0d58f20-abe1-4f54-8e44-50e1d226ead4") },
                    { new Guid("d7154a3d-eeb7-4e51-b954-25e2a09956a1"), "Caption", "https://placewaifu.com/image/97", true, new Guid("e9416abf-ab8a-4b69-8bb5-3f3a8b7024ad") },
                    { new Guid("d96e3ae2-9e00-462b-9838-70b67cf0144b"), "Caption", "https://placewaifu.com/image/81", true, new Guid("3238c0b8-d4a3-4cfb-9167-695ef4224b6b") },
                    { new Guid("dc731071-a736-408b-afce-08a7a2189842"), "Caption", "https://placewaifu.com/image/92", false, new Guid("0de34a77-a035-4eac-8f1b-5f21ebef4569") },
                    { new Guid("e05e7ebf-1891-453d-9771-76557af780b4"), "Caption", "https://placewaifu.com/image/94", true, new Guid("9089f19c-473d-40b1-b7b3-e0f5f6c8f8df") },
                    { new Guid("e4a2c317-d328-416f-b2ed-cd21b557baf1"), "Caption", "https://placewaifu.com/image/92", false, new Guid("f35c176b-c70a-4fb5-b9b3-885eb8731901") },
                    { new Guid("eb53010b-b72d-4d89-ad11-92617b62472e"), "Caption", "https://placewaifu.com/image/92", false, new Guid("9244f89d-1b5d-44cd-8a7f-1922d464e336") },
                    { new Guid("ed0b132c-aa81-42ba-9f00-04a89a02a076"), "Caption", "https://placewaifu.com/image/95", true, new Guid("f9ac3e9c-da9e-4aba-8a12-291a3655864b") },
                    { new Guid("ef7f44e5-4994-4368-925a-58628e26c005"), "Caption", "https://placewaifu.com/image/92", false, new Guid("0a5b4248-5d36-45aa-89fc-dd913824d34e") },
                    { new Guid("f2ac7dd0-d3a7-4bdb-97a7-2e34608f2bf5"), "Caption", "https://placewaifu.com/image/91", false, new Guid("163bd018-c22a-49e5-adb0-2ca2942bc420") },
                    { new Guid("f2fc1ebe-a5e1-4bc0-b234-3d1cc8715e1b"), "Caption", "https://placewaifu.com/image/91", false, new Guid("da3156b8-3324-4684-8789-9af51316865b") },
                    { new Guid("f5a606b1-7da1-427c-9665-2ab4360d4bb4"), "Caption", "https://placewaifu.com/image/91", false, new Guid("19400bf1-1da8-4a52-a515-4c36577bc98b") },
                    { new Guid("f5e15f27-00d2-4326-b6dd-2cb662c620f3"), "Caption", "https://placewaifu.com/image/92", false, new Guid("dcbeea7e-4675-4a01-b59a-a9e1f3e5f4b4") },
                    { new Guid("f710336b-e3ab-4046-a0f9-f9bda64a3735"), "Caption", "https://placewaifu.com/image/91", false, new Guid("0a5b4248-5d36-45aa-89fc-dd913824d34e") },
                    { new Guid("fb5308e3-ec7c-4243-a0fd-f2766137ae2f"), "Caption", "https://placewaifu.com/image/93", false, new Guid("dcbeea7e-4675-4a01-b59a-a9e1f3e5f4b4") },
                    { new Guid("fc27e1e9-63e8-4233-a1ac-b3839433ee62"), "Caption", "https://placewaifu.com/image/97", true, new Guid("f35c176b-c70a-4fb5-b9b3-885eb8731901") },
                    { new Guid("fc9e178f-d69a-4b8e-bda7-99f1e684e0be"), "Caption", "https://placewaifu.com/image/91", false, new Guid("dcbeea7e-4675-4a01-b59a-a9e1f3e5f4b4") },
                    { new Guid("fcd2ae74-3b86-4363-90be-8e042e2982ea"), "Caption", "https://placewaifu.com/image/92", false, new Guid("c861791b-bce9-47a9-84c7-1abdb578d88b") }
                });

            migrationBuilder.InsertData(
                table: "ProductUnits",
                columns: new[] { "Id", "ProductId", "Quantity", "UnitId" },
                values: new object[,]
                {
                    { new Guid("418692b1-7e02-4c3b-8fc5-f3559c196e16"), new Guid("5d01611b-e2ad-4abd-b40e-0450f50d2ec9"), 100, new Guid("4b703409-a32a-49cf-9943-d5a68dbedd6f") },
                    { new Guid("d8d9e88c-7f75-4d5e-9716-36f8c4461608"), new Guid("c861791b-bce9-47a9-84c7-1abdb578d88b"), 100, new Guid("bb7b0969-4383-446f-8ab9-6d766df08359") },
                    { new Guid("f3261f56-4e36-4e1a-866b-591fe403159e"), new Guid("5d01611b-e2ad-4abd-b40e-0450f50d2ec9"), 100, new Guid("54cb56aa-a710-47e0-a386-4cc493d46747") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_BillId",
                table: "BillDetails",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_ProductId",
                table: "BillDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CustomerId",
                table: "Bills",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponDetails_CouponId",
                table: "CouponDetails",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponDetails_ProductId",
                table: "CouponDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_SupplierId",
                table: "Coupons",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId1",
                table: "Orders",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UnitId",
                table: "Orders",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSuppliers_ProductId",
                table: "ProductSuppliers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSuppliers_SupplierId",
                table: "ProductSuppliers",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnits_ProductId",
                table: "ProductUnits",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnits_UnitId",
                table: "ProductUnits",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillDetails");

            migrationBuilder.DropTable(
                name: "CouponDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductSuppliers");

            migrationBuilder.DropTable(
                name: "ProductUnits");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
