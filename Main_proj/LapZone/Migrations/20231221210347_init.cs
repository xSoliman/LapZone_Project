using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapZone.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "_User",
                keyColumn: "UserID",
                keyValue: 13,
                column: "PasswordHash",
                value: "80d41c54a8ce6d26ae0bdd509db6b187140cae39b4b771269a0d006b0620e2d2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "_User",
                keyColumn: "UserID",
                keyValue: 13,
                column: "PasswordHash",
                value: "43a0d17178a9d26c9e0fe9a74b0b45e38d32f27aed887a008a54bf6e033bf7b9");
        }
    }
}
