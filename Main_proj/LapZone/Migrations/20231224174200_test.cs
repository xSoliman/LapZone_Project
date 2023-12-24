using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LapZone.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    _Description = table.Column<string>(type: "text", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__19093A2BE052224C", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserRole__8AFACE3A89F2E742", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    _Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Product__B40CC6ED28F7B0BB", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Category_Product",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.CreateTable(
                name: "_User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___User__1788CCACF6F582C2", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Role__User",
                        column: x => x.RoleID,
                        principalTable: "UserRole",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "_Address",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Governorate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AddressLine = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___Address__091C2A1B6B3F29E0", x => x.AddressID);
                    table.ForeignKey(
                        name: "FK_User_Address",
                        column: x => x.UserID,
                        principalTable: "_User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cart__51BCD79729848FB3", x => x.CartID);
                    table.ForeignKey(
                        name: "FK__User_Cart",
                        column: x => x.UserID,
                        principalTable: "_User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Wishlist",
                columns: table => new
                {
                    WishlistID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Wishlist__233189CB7587DEA0", x => x.WishlistID);
                    table.ForeignKey(
                        name: "FK_Product_Wishlist",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK__User_Wishlist",
                        column: x => x.UserID,
                        principalTable: "_User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "_Order",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    AddressID = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___Order__C3905BAF0DB0B467", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Order_Address",
                        column: x => x.AddressID,
                        principalTable: "_Address",
                        principalColumn: "AddressID");
                    table.ForeignKey(
                        name: "FK__User_Order",
                        column: x => x.UserID,
                        principalTable: "_User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    CartItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CartItem__488B0B2A4900B0C4", x => x.CartItemID);
                    table.ForeignKey(
                        name: "FK_Cart_CartItem",
                        column: x => x.CartID,
                        principalTable: "Cart",
                        principalColumn: "CartID");
                    table.ForeignKey(
                        name: "FK_Product_CartItem",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderIte__57ED06A1F3AF9887", x => x.OrderItemID);
                    table.ForeignKey(
                        name: "FK_Order_OrderItem",
                        column: x => x.OrderID,
                        principalTable: "_Order",
                        principalColumn: "OrderID");
                    table.ForeignKey(
                        name: "FK_Product_OrderItem",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID");
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[,]
                {
                    { 1, "Owner" },
                    { 2, "Admin" },
                    { 3, "Customer" }
                });

            migrationBuilder.InsertData(
                table: "_User",
                columns: new[] { "UserID", "Email", "FullName", "ImagePath", "PasswordHash", "PhoneNumber", "RoleID" },
                values: new object[] { 1, "owner@owner.com", "The Owner", "Owner.png", "1305485a712608fdc4d2fd1780c72919f2f54cf288525814bff7120737f6ddad", "01284101351", 1 });

            migrationBuilder.CreateIndex(
                name: "UQ_User_Address",
                table: "_Address",
                columns: new[] { "UserID", "Country", "Governorate", "City", "AddressLine" },
                unique: true,
                filter: "[UserID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX__Order_AddressID",
                table: "_Order",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX__Order_UserID",
                table: "_Order",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX__User_RoleID",
                table: "_User",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "UQ___User__A9D1053460F40BD9",
                table: "_User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserID",
                table: "Cart",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ProductID",
                table: "CartItem",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "UQ_CartItem",
                table: "CartItem",
                columns: new[] { "CartID", "ProductID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductID",
                table: "OrderItem",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "UQ_OrderItem",
                table: "OrderItem",
                columns: new[] { "OrderID", "ProductID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryID",
                table: "Product",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_ProductID",
                table: "Wishlist",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "UQ_Wishlist",
                table: "Wishlist",
                columns: new[] { "UserID", "ProductID" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Wishlist");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "_Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "_Address");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "_User");

            migrationBuilder.DropTable(
                name: "UserRole");
        }
    }
}
