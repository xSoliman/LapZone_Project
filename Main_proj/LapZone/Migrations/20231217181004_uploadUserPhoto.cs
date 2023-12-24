using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapZone.Migrations
{
    /// <inheritdoc />
    public partial class uploadUserPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_User_Address",
                table: "_Address");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "_User",
                newName: "ImagePath");

            migrationBuilder.AlterColumn<string>(
                name: "Governorate",
                table: "_Address",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "_Address",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "_Address",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "_User",
                keyColumn: "UserID",
                keyValue: 13,
                column: "ImagePath",
                value: "Images/Users/Admin");

            migrationBuilder.CreateIndex(
                name: "UQ_User_Address",
                table: "_Address",
                columns: new[] { "UserID", "Country", "Governorate", "City", "AddressLine" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_User_Address",
                table: "_Address");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "_User",
                newName: "Photo");

            migrationBuilder.AlterColumn<string>(
                name: "Governorate",
                table: "_Address",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "_Address",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "_Address",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "_User",
                keyColumn: "UserID",
                keyValue: 13,
                column: "Photo",
                value: "qweqwe");

            migrationBuilder.CreateIndex(
                name: "UQ_User_Address",
                table: "_Address",
                columns: new[] { "UserID", "Country", "Governorate", "City", "AddressLine" },
                unique: true,
                filter: "[Country] IS NOT NULL AND [Governorate] IS NOT NULL AND [City] IS NOT NULL");
        }
    }
}
