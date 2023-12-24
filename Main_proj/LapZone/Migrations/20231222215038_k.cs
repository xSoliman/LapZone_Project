using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapZone.Migrations
{
    /// <inheritdoc />
    public partial class k : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_Product_ProductID",
                table: "Wishlist");

            migrationBuilder.DropIndex(
                name: "IX_Wishlist_UserID",
                table: "Wishlist");

            migrationBuilder.DropIndex(
                name: "IX_Cart_UserID",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "UQ_User_Address",
                table: "_Address");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "_Address",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "_User",
                keyColumn: "UserID",
                keyValue: 13,
                column: "PasswordHash",
                value: "1305485a712608fdc4d2fd1780c72919f2f54cf288525814bff7120737f6ddad");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserID",
                table: "Cart",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ_User_Address",
                table: "_Address",
                columns: new[] { "UserID", "Country", "Governorate", "City", "AddressLine" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Wishlist",
                table: "Wishlist",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Wishlist",
                table: "Wishlist");

            migrationBuilder.DropIndex(
                name: "IX_Cart_UserID",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "UQ_User_Address",
                table: "_Address");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "_Address",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "_User",
                keyColumn: "UserID",
                keyValue: 13,
                column: "PasswordHash",
                value: "80d41c54a8ce6d26ae0bdd509db6b187140cae39b4b771269a0d006b0620e2d2");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_UserID",
                table: "Wishlist",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserID",
                table: "Cart",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_User_Address",
                table: "_Address",
                columns: new[] { "UserID", "Country", "Governorate", "City", "AddressLine" },
                unique: true,
                filter: "[UserID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_Product_ProductID",
                table: "Wishlist",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
