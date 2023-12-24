using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LapZone.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "_Address",
                columns: new[] { "AddressID", "AddressLine", "City", "Country", "Governorate", "UserID" },
                values: new object[,]
                {
                    { 10, "elmaktbat", "Assiut", "Egypt", "Assiut", 11 },
                    { 100, "7agmohammed", "8anaiem", "Egypt", "Assiut", 11 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "_Address",
                keyColumn: "AddressID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "_Address",
                keyColumn: "AddressID",
                keyValue: 100);
        }
    }
}
