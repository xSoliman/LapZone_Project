using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapZone.Migrations
{
    /// <inheritdoc />
    public partial class userIdAdressNull2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "UQ_User_Address",
                table: "_Address",
                columns: new[] { "UserID", "Country", "Governorate", "City", "AddressLine" },
                unique: true,
                filter: "[UserID] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "UQ_User_Address",
                table: "_Address",
                columns: new[] { "UserID", "Country", "Governorate", "City", "AddressLine" },
                unique: true);
        }
    }
}
