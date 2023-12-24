using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapZone.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_User_Address",
                table: "_Address");

            migrationBuilder.DeleteData(
                table: "_User",
                keyColumn: "UserID",
                keyValue: 13);

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "_Address",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_User_Address",
                table: "_Address");

            migrationBuilder.DeleteData(
                table: "_User",
                keyColumn: "UserID",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "_Address",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "_User",
                columns: new[] { "UserID", "Email", "FullName", "ImagePath", "PasswordHash", "PhoneNumber", "RoleID" },
                values: new object[] { 13, "owner@owner", "Owner", "Admin", "1305485a712608fdc4d2fd1780c72919f2f54cf288525814bff7120737f6ddad", "012", 1 });

            migrationBuilder.CreateIndex(
                name: "UQ_User_Address",
                table: "_Address",
                columns: new[] { "UserID", "Country", "Governorate", "City", "AddressLine" },
                unique: true);
        }
    }
}
