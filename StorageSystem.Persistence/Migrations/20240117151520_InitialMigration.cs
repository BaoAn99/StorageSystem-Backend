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
                values: new object[] { new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 17, 22, 15, 20, 46, DateTimeKind.Unspecified).AddTicks(3932), new TimeSpan(0, 7, 0, 0, 0)), false, "Vật liệu xây dựng" });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "DateCreated", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("4b703409-a32a-49cf-9943-d5a68dbedd6f"), new DateTimeOffset(new DateTime(2024, 1, 17, 22, 15, 20, 46, DateTimeKind.Unspecified).AddTicks(4071), new TimeSpan(0, 7, 0, 0, 0)), false, "Thùng" },
                    { new Guid("54cb56aa-a710-47e0-a386-4cc493d46747"), new DateTimeOffset(new DateTime(2024, 1, 17, 22, 15, 20, 46, DateTimeKind.Unspecified).AddTicks(4097), new TimeSpan(0, 7, 0, 0, 0)), false, "Hộp" },
                    { new Guid("bb7b0969-4383-446f-8ab9-6d766df08359"), new DateTimeOffset(new DateTime(2024, 1, 17, 22, 15, 20, 46, DateTimeKind.Unspecified).AddTicks(4087), new TimeSpan(0, 7, 0, 0, 0)), false, "Kg" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "DateCreated", "Description", "IsDeleted", "Name", "OriginalPrice", "Price", "ThumbnailImage" },
                values: new object[,]
                {
                    { new Guid("0a5b4248-5d36-45aa-89fc-dd913824d34e"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 17, 22, 15, 20, 46, DateTimeKind.Unspecified).AddTicks(4157), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Emma Adams", 110000m, 405000m, "https://placewaifu.com/image/89" },
                    { new Guid("0de34a77-a035-4eac-8f1b-5f21ebef4569"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 17, 22, 15, 20, 46, DateTimeKind.Unspecified).AddTicks(4118), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Jane Fisher", 110000m, 405000m, "https://placewaifu.com/image/82" },
                    { new Guid("163bd018-c22a-49e5-adb0-2ca2942bc420"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 17, 22, 15, 20, 46, DateTimeKind.Unspecified).AddTicks(4137), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "William Howard", 110000m, 405000m, "https://placewaifu.com/image/83" },
                    { new Guid("19400bf1-1da8-4a52-a515-4c36577bc98b"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 17, 22, 15, 20, 46, DateTimeKind.Unspecified).AddTicks(4166), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Megan Richards", 110000m, 405000m, "https://placewaifu.com/image/91" },
                    { new Guid("1a1f7026-eb10-4632-ba16-c12bbea207b7"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 17, 22, 15, 20, 46, DateTimeKind.Unspecified).AddTicks(4140), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Kristen Copper", 110000m, 405000m, "https://placewaifu.com/image/84" },
                    { new Guid("3238c0b8-d4a3-4cfb-9167-695ef4224b6b"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 17, 22, 15, 20, 46, DateTimeKind.Unspecified).AddTicks(4114), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Zoey Lang", 110000m, 405000m, "https://placewaifu.com/image/81" },
                    { new Guid("484c3f38-bfe0-42b9-8e9c-f30f1aa86fd3"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 17, 22, 15, 20, 46, DateTimeKind.Unspecified).AddTicks(4108), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Tony Reichert", 110000m, 405000m, "https://placewaifu.com/image/80" },
                    { new Guid("5d01611b-e2ad-4abd-b40e-0450f50d2ec9"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 17, 22, 15, 20, 46, DateTimeKind.Unspecified).AddTicks(4169), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Oliver Scott", 110000m, 405000m, "https://placewaifu.com/image/92" },
                    { new Guid("9089f19c-473d-40b1-b7b3-e0f5f6c8f8df"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 17, 22, 15, 20, 46, DateTimeKind.Unspecified).AddTicks(4225), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Noah Carter", 110000m, 405000m, "https://placewaifu.com/image/94" },
                    { new Guid("97aac145-947d-4da4-8191-a2a3faa81fce"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 17, 22, 15, 20, 46, DateTimeKind.Unspecified).AddTicks(4154), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Frank Harrison", 110000m, 405000m, "https://placewaifu.com/image/88" },
                    { new Guid("c861791b-bce9-47a9-84c7-1abdb578d88b"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 17, 22, 15, 20, 46, DateTimeKind.Unspecified).AddTicks(4144), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Brian Kim", 110000m, 405000m, "https://placewaifu.com/image/85" },
                    { new Guid("cff0eb9e-3b50-4ade-9f56-cb0e4ab4d617"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 17, 22, 15, 20, 46, DateTimeKind.Unspecified).AddTicks(4173), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Grace Allen", 110000m, 405000m, "https://placewaifu.com/image/93" },
                    { new Guid("d0d58f20-abe1-4f54-8e44-50e1d226ead4"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 17, 22, 15, 20, 46, DateTimeKind.Unspecified).AddTicks(4147), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Michael Hunt", 110000m, 405000m, "https://placewaifu.com/image/86" },
                    { new Guid("da3156b8-3324-4684-8789-9af51316865b"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 17, 22, 15, 20, 46, DateTimeKind.Unspecified).AddTicks(4160), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Brandon Stevens", 110000m, 405000m, "https://placewaifu.com/image/90" },
                    { new Guid("dcbeea7e-4675-4a01-b59a-a9e1f3e5f4b4"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 17, 22, 15, 20, 46, DateTimeKind.Unspecified).AddTicks(4150), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Samantha Brooks", 110000m, 405000m, "https://placewaifu.com/image/87" },
                    { new Guid("f9ac3e9c-da9e-4aba-8a12-291a3655864b"), new Guid("9be53c65-521f-417a-b7de-13af7b0bdfc4"), new DateTimeOffset(new DateTime(2024, 1, 17, 22, 15, 20, 46, DateTimeKind.Unspecified).AddTicks(4229), new TimeSpan(0, 7, 0, 0, 0)), "Description", false, "Ava Perez", 110000m, 405000m, "https://placewaifu.com/image/95" }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "Caption", "ImagePath", "IsImageFeature", "ProductId" },
                values: new object[,]
                {
                    { new Guid("055a6494-112e-4657-b999-fceff2e4805c"), "Caption", "https://placewaifu.com/image/93", false, new Guid("5d01611b-e2ad-4abd-b40e-0450f50d2ec9") },
                    { new Guid("07494c60-cf32-434f-9173-85025b7aaec6"), "Caption", "https://placewaifu.com/image/91", false, new Guid("dcbeea7e-4675-4a01-b59a-a9e1f3e5f4b4") },
                    { new Guid("0a793a41-7409-4f3c-933c-105c2e707611"), "Caption", "https://placewaifu.com/image/92", false, new Guid("0a5b4248-5d36-45aa-89fc-dd913824d34e") },
                    { new Guid("0e4b13b0-53f6-4e7e-a0c2-0389b49f28aa"), "Caption", "https://placewaifu.com/image/91", false, new Guid("9089f19c-473d-40b1-b7b3-e0f5f6c8f8df") },
                    { new Guid("1426016b-4f4c-4ff7-8a23-3c3a5bc19067"), "Caption", "https://placewaifu.com/image/92", false, new Guid("3238c0b8-d4a3-4cfb-9167-695ef4224b6b") },
                    { new Guid("16617ad3-42c6-4ab2-805a-3613ad324165"), "Caption", "https://placewaifu.com/image/93", false, new Guid("9089f19c-473d-40b1-b7b3-e0f5f6c8f8df") },
                    { new Guid("17e3ecc3-f7ae-4dbb-8bd3-31a172488a85"), "Caption", "https://placewaifu.com/image/92", false, new Guid("5d01611b-e2ad-4abd-b40e-0450f50d2ec9") },
                    { new Guid("1f1243ad-4913-49d4-b4d9-0a1b5f676a92"), "Caption", "https://placewaifu.com/image/91", false, new Guid("484c3f38-bfe0-42b9-8e9c-f30f1aa86fd3") },
                    { new Guid("23226b87-5338-4300-a561-9b283803430b"), "Caption", "https://placewaifu.com/image/93", false, new Guid("97aac145-947d-4da4-8191-a2a3faa81fce") },
                    { new Guid("243c8ae6-9375-4f32-a1e4-5d7a9092a2e9"), "Caption", "https://placewaifu.com/image/92", false, new Guid("484c3f38-bfe0-42b9-8e9c-f30f1aa86fd3") },
                    { new Guid("2516d616-4f28-4194-ba3c-de1bd1b5f158"), "Caption", "https://placewaifu.com/image/93", false, new Guid("cff0eb9e-3b50-4ade-9f56-cb0e4ab4d617") },
                    { new Guid("2713178c-e436-4b43-8179-e261178b1725"), "Caption", "https://placewaifu.com/image/90", true, new Guid("da3156b8-3324-4684-8789-9af51316865b") },
                    { new Guid("286e522a-2ff1-4af6-b77f-d1e94f4c86e5"), "Caption", "https://placewaifu.com/image/91", false, new Guid("f9ac3e9c-da9e-4aba-8a12-291a3655864b") },
                    { new Guid("28f89bc8-9f0b-4d0b-9942-8da7f5c60e9a"), "Caption", "https://placewaifu.com/image/86", true, new Guid("d0d58f20-abe1-4f54-8e44-50e1d226ead4") },
                    { new Guid("2d60dc64-6836-452c-b5cf-d32af165abc2"), "Caption", "https://placewaifu.com/image/93", false, new Guid("19400bf1-1da8-4a52-a515-4c36577bc98b") },
                    { new Guid("37fe768d-0324-4401-82e0-7e3a405e071c"), "Caption", "https://placewaifu.com/image/84", true, new Guid("1a1f7026-eb10-4632-ba16-c12bbea207b7") },
                    { new Guid("3d06b946-2031-4ca7-ae53-7fc4ec497e5b"), "Caption", "https://placewaifu.com/image/85", true, new Guid("c861791b-bce9-47a9-84c7-1abdb578d88b") },
                    { new Guid("4657bb12-b1ee-4c88-bd71-06d27fec3844"), "Caption", "https://placewaifu.com/image/91", true, new Guid("19400bf1-1da8-4a52-a515-4c36577bc98b") },
                    { new Guid("4a6cf011-acb6-49d0-84f9-7ba10d522b81"), "Caption", "https://placewaifu.com/image/91", false, new Guid("cff0eb9e-3b50-4ade-9f56-cb0e4ab4d617") },
                    { new Guid("4cf2ec35-7560-4d62-823b-ca47adf8a153"), "Caption", "https://placewaifu.com/image/92", false, new Guid("c861791b-bce9-47a9-84c7-1abdb578d88b") },
                    { new Guid("4dbce868-13e7-43b6-9514-72ffce22613b"), "Caption", "https://placewaifu.com/image/91", false, new Guid("5d01611b-e2ad-4abd-b40e-0450f50d2ec9") },
                    { new Guid("579fd32c-4b0f-4db8-ada4-0cd008903af3"), "Caption", "https://placewaifu.com/image/81", true, new Guid("3238c0b8-d4a3-4cfb-9167-695ef4224b6b") },
                    { new Guid("5a8afac1-c689-42d5-8be0-77cbccd5e06d"), "Caption", "https://placewaifu.com/image/82", true, new Guid("0de34a77-a035-4eac-8f1b-5f21ebef4569") },
                    { new Guid("5feaeff3-9daa-467a-9090-cfd721069447"), "Caption", "https://placewaifu.com/image/93", false, new Guid("f9ac3e9c-da9e-4aba-8a12-291a3655864b") },
                    { new Guid("61692503-060e-45f4-9cc7-27ea914b2c67"), "Caption", "https://placewaifu.com/image/94", true, new Guid("9089f19c-473d-40b1-b7b3-e0f5f6c8f8df") },
                    { new Guid("6bb8e92a-b620-4b27-ab27-56597f3d3d78"), "Caption", "https://placewaifu.com/image/93", false, new Guid("0a5b4248-5d36-45aa-89fc-dd913824d34e") },
                    { new Guid("6fb022c7-7e09-4273-af39-cb6980b88872"), "Caption", "https://placewaifu.com/image/91", false, new Guid("0de34a77-a035-4eac-8f1b-5f21ebef4569") },
                    { new Guid("7029a69a-dd17-49f4-be40-825b87d26ad6"), "Caption", "https://placewaifu.com/image/91", false, new Guid("19400bf1-1da8-4a52-a515-4c36577bc98b") },
                    { new Guid("718ffbdb-213c-4010-99bc-dec59db60834"), "Caption", "https://placewaifu.com/image/91", false, new Guid("da3156b8-3324-4684-8789-9af51316865b") },
                    { new Guid("722e7126-86ef-4b92-b6f3-23a792e13cd1"), "Caption", "https://placewaifu.com/image/93", false, new Guid("0de34a77-a035-4eac-8f1b-5f21ebef4569") },
                    { new Guid("7a1c3f21-d40d-4137-8b92-01d6a644d26f"), "Caption", "https://placewaifu.com/image/89", true, new Guid("0a5b4248-5d36-45aa-89fc-dd913824d34e") },
                    { new Guid("7a5bc91f-4739-424a-8f2b-f34fdaccda78"), "Caption", "https://placewaifu.com/image/92", false, new Guid("0de34a77-a035-4eac-8f1b-5f21ebef4569") },
                    { new Guid("7ca04698-1faa-45ce-8c75-f89aca3ccdf3"), "Caption", "https://placewaifu.com/image/92", false, new Guid("97aac145-947d-4da4-8191-a2a3faa81fce") },
                    { new Guid("7ce38eba-2c6f-444e-981a-3d2108e947ae"), "Caption", "https://placewaifu.com/image/92", false, new Guid("dcbeea7e-4675-4a01-b59a-a9e1f3e5f4b4") },
                    { new Guid("7d7be661-d8a5-4757-b4af-bb20aab57509"), "Caption", "https://placewaifu.com/image/91", false, new Guid("97aac145-947d-4da4-8191-a2a3faa81fce") },
                    { new Guid("819114c8-a3cd-4443-a65a-0087722157a2"), "Caption", "https://placewaifu.com/image/83", true, new Guid("163bd018-c22a-49e5-adb0-2ca2942bc420") },
                    { new Guid("896649d3-f2de-489d-ade4-42710192465a"), "Caption", "https://placewaifu.com/image/93", false, new Guid("1a1f7026-eb10-4632-ba16-c12bbea207b7") },
                    { new Guid("8d82d82c-1b95-43c6-b9ca-1cb2151364db"), "Caption", "https://placewaifu.com/image/92", false, new Guid("9089f19c-473d-40b1-b7b3-e0f5f6c8f8df") },
                    { new Guid("8e4e13c3-261b-4112-b4c4-b16120941f12"), "Caption", "https://placewaifu.com/image/93", true, new Guid("cff0eb9e-3b50-4ade-9f56-cb0e4ab4d617") },
                    { new Guid("916fb773-9961-4b0e-aee8-3c37648d7338"), "Caption", "https://placewaifu.com/image/92", false, new Guid("da3156b8-3324-4684-8789-9af51316865b") },
                    { new Guid("93ff9327-f755-47f5-800f-c239a6b8515a"), "Caption", "https://placewaifu.com/image/93", false, new Guid("163bd018-c22a-49e5-adb0-2ca2942bc420") },
                    { new Guid("9520dd3e-20ab-4e9a-8d10-6e8eb8feb1a0"), "Caption", "https://placewaifu.com/image/91", false, new Guid("163bd018-c22a-49e5-adb0-2ca2942bc420") }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "Caption", "ImagePath", "IsImageFeature", "ProductId" },
                values: new object[,]
                {
                    { new Guid("97ca3d0c-af09-4a53-aab5-5bbb1f5b9a4d"), "Caption", "https://placewaifu.com/image/93", false, new Guid("3238c0b8-d4a3-4cfb-9167-695ef4224b6b") },
                    { new Guid("a27dd9e3-1c7e-4c7d-9ab1-d1607e581018"), "Caption", "https://placewaifu.com/image/92", false, new Guid("1a1f7026-eb10-4632-ba16-c12bbea207b7") },
                    { new Guid("af06c214-76d9-4e5c-8452-62ac096bbd60"), "Caption", "https://placewaifu.com/image/93", false, new Guid("d0d58f20-abe1-4f54-8e44-50e1d226ead4") },
                    { new Guid("b1ca0420-7b45-4362-b893-0def6d86dbf5"), "Caption", "https://placewaifu.com/image/93", false, new Guid("dcbeea7e-4675-4a01-b59a-a9e1f3e5f4b4") },
                    { new Guid("c01647a5-f396-4913-98b0-37eb7da084b9"), "Caption", "https://placewaifu.com/image/93", false, new Guid("484c3f38-bfe0-42b9-8e9c-f30f1aa86fd3") },
                    { new Guid("c5444310-7782-4eff-9961-f5b0cf3571fa"), "Caption", "https://placewaifu.com/image/91", false, new Guid("0a5b4248-5d36-45aa-89fc-dd913824d34e") },
                    { new Guid("cb194c55-32c7-46bb-9e49-b4190c677ac5"), "Caption", "https://placewaifu.com/image/91", false, new Guid("1a1f7026-eb10-4632-ba16-c12bbea207b7") },
                    { new Guid("cf5d58af-7ce5-4ee4-9556-6ea873965fca"), "Caption", "https://placewaifu.com/image/95", true, new Guid("f9ac3e9c-da9e-4aba-8a12-291a3655864b") },
                    { new Guid("d2095942-97f0-45ef-ba8b-ad4b4f9c9a17"), "Caption", "https://placewaifu.com/image/92", true, new Guid("5d01611b-e2ad-4abd-b40e-0450f50d2ec9") },
                    { new Guid("d3e9a654-f80f-4403-b2b7-163e70ca0ed3"), "Caption", "https://placewaifu.com/image/92", false, new Guid("cff0eb9e-3b50-4ade-9f56-cb0e4ab4d617") },
                    { new Guid("d7aaf288-4819-40ed-a7f6-48d99e51236b"), "Caption", "https://placewaifu.com/image/91", false, new Guid("c861791b-bce9-47a9-84c7-1abdb578d88b") },
                    { new Guid("dbdcb7dd-1fd3-457c-85e1-eeb5b7c7b4d2"), "Caption", "https://placewaifu.com/image/80", true, new Guid("484c3f38-bfe0-42b9-8e9c-f30f1aa86fd3") },
                    { new Guid("dd5b3cb8-ac46-4acc-a86e-9d3dc80155e7"), "Caption", "https://placewaifu.com/image/91", false, new Guid("3238c0b8-d4a3-4cfb-9167-695ef4224b6b") },
                    { new Guid("de02a3e6-e0b9-4689-90f4-f108ff1a9908"), "Caption", "https://placewaifu.com/image/93", false, new Guid("da3156b8-3324-4684-8789-9af51316865b") },
                    { new Guid("de0b8227-243b-4b83-bd5f-7b8e8fee9d4f"), "Caption", "https://placewaifu.com/image/93", false, new Guid("c861791b-bce9-47a9-84c7-1abdb578d88b") },
                    { new Guid("e1f1127d-27d3-468d-abfd-c28f4ed38381"), "Caption", "https://placewaifu.com/image/92", false, new Guid("d0d58f20-abe1-4f54-8e44-50e1d226ead4") },
                    { new Guid("e84a56e9-0d91-4df7-b4e9-cb4f8b13ce42"), "Caption", "https://placewaifu.com/image/88", true, new Guid("97aac145-947d-4da4-8191-a2a3faa81fce") },
                    { new Guid("ea436739-d720-4ec2-af89-22597a350541"), "Caption", "https://placewaifu.com/image/92", false, new Guid("f9ac3e9c-da9e-4aba-8a12-291a3655864b") },
                    { new Guid("ebed1d8f-96be-4a0d-a9e5-7e77b302f8ed"), "Caption", "https://placewaifu.com/image/87", true, new Guid("dcbeea7e-4675-4a01-b59a-a9e1f3e5f4b4") },
                    { new Guid("ebffb68c-81fb-437e-9195-62f57db09755"), "Caption", "https://placewaifu.com/image/92", false, new Guid("163bd018-c22a-49e5-adb0-2ca2942bc420") },
                    { new Guid("ec6cf9cb-3305-49cd-a256-acb540006295"), "Caption", "https://placewaifu.com/image/91", false, new Guid("d0d58f20-abe1-4f54-8e44-50e1d226ead4") },
                    { new Guid("f8c37062-0f97-44bd-945d-7286e49b0d5d"), "Caption", "https://placewaifu.com/image/92", false, new Guid("19400bf1-1da8-4a52-a515-4c36577bc98b") }
                });

            migrationBuilder.InsertData(
                table: "ProductUnits",
                columns: new[] { "Id", "ProductId", "Quantity", "UnitId" },
                values: new object[,]
                {
                    { new Guid("276d8bb6-39d6-4b0c-b800-f6dfbbec3e5a"), new Guid("5d01611b-e2ad-4abd-b40e-0450f50d2ec9"), 100, new Guid("54cb56aa-a710-47e0-a386-4cc493d46747") },
                    { new Guid("38d78523-7310-48ec-829d-a987808a619d"), new Guid("c861791b-bce9-47a9-84c7-1abdb578d88b"), 100, new Guid("bb7b0969-4383-446f-8ab9-6d766df08359") },
                    { new Guid("793a75ec-eed8-4043-acca-f508caa0db71"), new Guid("5d01611b-e2ad-4abd-b40e-0450f50d2ec9"), 100, new Guid("4b703409-a32a-49cf-9943-d5a68dbedd6f") }
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
