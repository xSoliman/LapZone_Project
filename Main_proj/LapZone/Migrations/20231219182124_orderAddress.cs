using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LapZone.Migrations
{
    /// <inheritdoc />
    public partial class orderAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "_Address",
                keyColumn: "AddressID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "_Address",
                keyColumn: "AddressID",
                keyValue: 100);

            migrationBuilder.AddColumn<int>(
                name: "AddressID",
                table: "_Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX__Order_AddressID",
                table: "_Order",
                column: "AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Address",
                table: "_Order",
                column: "AddressID",
                principalTable: "_Address",
                principalColumn: "AddressID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Address",
                table: "_Order");

            migrationBuilder.DropIndex(
                name: "IX__Order_AddressID",
                table: "_Order");

            migrationBuilder.DropColumn(
                name: "AddressID",
                table: "_Order");

            migrationBuilder.InsertData(
                table: "_Address",
                columns: new[] { "AddressID", "AddressLine", "City", "Country", "Governorate", "UserID" },
                values: new object[,]
                {
                    { 10, "elmaktbat", "Assiut", "Egypt", "Assiut", 11 },
                    { 100, "7agmohammed", "8anaiem", "Egypt", "Assiut", 11 }
                });
        }
    }
}
