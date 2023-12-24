using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapZone.Migrations
{
    /// <inheritdoc />
    public partial class oneoneWishlisttUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Wishlist",
                table: "Wishlist");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_UserID",
                table: "Wishlist",
                column: "UserID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_Product_ProductID",
                table: "Wishlist",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_Product_ProductID",
                table: "Wishlist");

            migrationBuilder.DropIndex(
                name: "IX_Wishlist_UserID",
                table: "Wishlist");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Wishlist",
                table: "Wishlist",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID");
        }
    }
}
