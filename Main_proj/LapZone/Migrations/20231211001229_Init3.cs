using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LapZone.Migrations
{
    /// <inheritdoc />
    public partial class Init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Address__User_UserID",
                table: "_Address");

            migrationBuilder.DropForeignKey(
                name: "FK__Order__User_UserID",
                table: "_Order");

            migrationBuilder.DropForeignKey(
                name: "FK__User_UserRole_RoleID",
                table: "_User");

            migrationBuilder.DropForeignKey(
                name: "FK_Cart__User_UserID",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Cart_CartID",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Product_ProductID",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_LaptopDetail_Product_ProductID",
                table: "LaptopDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Product_ProductID",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem__Order_OrderID",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryID",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Product_ProductID",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review__User_UserID",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_Product_ProductID",
                table: "Wishlist");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist__User_UserID",
                table: "Wishlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wishlist",
                table: "Wishlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LaptopDetail",
                table: "LaptopDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                table: "Cart");

            migrationBuilder.DropPrimaryKey(
                name: "PK__User",
                table: "_User");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Order",
                table: "_Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Address",
                table: "_Address");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReviewDate",
                table: "Review",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "_Order",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Wishlist__233189CB7587DEA0",
                table: "Wishlist",
                column: "WishlistID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__UserRole__8AFACE3A89F2E742",
                table: "UserRole",
                column: "RoleID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Review__74BC79AEB92B741D",
                table: "Review",
                column: "ReviewID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Product__B40CC6ED28F7B0BB",
                table: "Product",
                column: "ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__OrderIte__57ED06A1F3AF9887",
                table: "OrderItem",
                column: "OrderItemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__LaptopDe__19F026A4D5C77AFB",
                table: "LaptopDetail",
                column: "LaptopID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Category__19093A2BE052224C",
                table: "Category",
                column: "CategoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__CartItem__488B0B2A4900B0C4",
                table: "CartItem",
                column: "CartItemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Cart__51BCD79729848FB3",
                table: "Cart",
                column: "CartID");

            migrationBuilder.AddPrimaryKey(
                name: "PK___User__1788CCACF6F582C2",
                table: "_User",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK___Order__C3905BAF0DB0B467",
                table: "_Order",
                column: "OrderID");

            migrationBuilder.AddPrimaryKey(
                name: "PK___Address__091C2A1B6B3F29E0",
                table: "_Address",
                column: "AddressID");

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Customer" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_User_Address",
                table: "_Address",
                column: "UserID",
                principalTable: "_User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK__User_Order",
                table: "_Order",
                column: "UserID",
                principalTable: "_User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role__User",
                table: "_User",
                column: "RoleID",
                principalTable: "UserRole",
                principalColumn: "RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK__User_Cart",
                table: "Cart",
                column: "UserID",
                principalTable: "_User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_CartItem",
                table: "CartItem",
                column: "CartID",
                principalTable: "Cart",
                principalColumn: "CartID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_CartItem",
                table: "CartItem",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_LaptopDetail",
                table: "LaptopDetail",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderItem",
                table: "OrderItem",
                column: "OrderID",
                principalTable: "_Order",
                principalColumn: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_OrderItem",
                table: "OrderItem",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Product",
                table: "Product",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Review",
                table: "Review",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK__User_Review",
                table: "Review",
                column: "UserID",
                principalTable: "_User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Wishlist",
                table: "Wishlist",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK__User_Wishlist",
                table: "Wishlist",
                column: "UserID",
                principalTable: "_User",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Address",
                table: "_Address");

            migrationBuilder.DropForeignKey(
                name: "FK__User_Order",
                table: "_Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Role__User",
                table: "_User");

            migrationBuilder.DropForeignKey(
                name: "FK__User_Cart",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Cart_CartItem",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_CartItem",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_LaptopDetail",
                table: "LaptopDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderItem",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_OrderItem",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Product",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Review",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK__User_Review",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Wishlist",
                table: "Wishlist");

            migrationBuilder.DropForeignKey(
                name: "FK__User_Wishlist",
                table: "Wishlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Wishlist__233189CB7587DEA0",
                table: "Wishlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK__UserRole__8AFACE3A89F2E742",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Review__74BC79AEB92B741D",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Product__B40CC6ED28F7B0BB",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK__OrderIte__57ED06A1F3AF9887",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK__LaptopDe__19F026A4D5C77AFB",
                table: "LaptopDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Category__19093A2BE052224C",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK__CartItem__488B0B2A4900B0C4",
                table: "CartItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Cart__51BCD79729848FB3",
                table: "Cart");

            migrationBuilder.DropPrimaryKey(
                name: "PK___User__1788CCACF6F582C2",
                table: "_User");

            migrationBuilder.DropPrimaryKey(
                name: "PK___Order__C3905BAF0DB0B467",
                table: "_Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK___Address__091C2A1B6B3F29E0",
                table: "_Address");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "RoleID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "RoleID",
                keyValue: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReviewDate",
                table: "Review",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "_Order",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wishlist",
                table: "Wishlist",
                column: "WishlistID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                column: "RoleID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "ReviewID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem",
                column: "OrderItemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LaptopDetail",
                table: "LaptopDetail",
                column: "LaptopID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "CategoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem",
                column: "CartItemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                table: "Cart",
                column: "CartID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__User",
                table: "_User",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Order",
                table: "_Order",
                column: "OrderID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Address",
                table: "_Address",
                column: "AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK__Address__User_UserID",
                table: "_Address",
                column: "UserID",
                principalTable: "_User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Order__User_UserID",
                table: "_Order",
                column: "UserID",
                principalTable: "_User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__User_UserRole_RoleID",
                table: "_User",
                column: "RoleID",
                principalTable: "UserRole",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cart__User_UserID",
                table: "Cart",
                column: "UserID",
                principalTable: "_User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Cart_CartID",
                table: "CartItem",
                column: "CartID",
                principalTable: "Cart",
                principalColumn: "CartID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Product_ProductID",
                table: "CartItem",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LaptopDetail_Product_ProductID",
                table: "LaptopDetail",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Product_ProductID",
                table: "OrderItem",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem__Order_OrderID",
                table: "OrderItem",
                column: "OrderID",
                principalTable: "_Order",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryID",
                table: "Product",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Product_ProductID",
                table: "Review",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review__User_UserID",
                table: "Review",
                column: "UserID",
                principalTable: "_User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_Product_ProductID",
                table: "Wishlist",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist__User_UserID",
                table: "Wishlist",
                column: "UserID",
                principalTable: "_User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
