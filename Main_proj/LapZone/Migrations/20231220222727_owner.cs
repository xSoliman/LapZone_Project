using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapZone.Migrations
{
    /// <inheritdoc />
    public partial class owner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "_User",
                keyColumn: "UserID",
                keyValue: 13,
                columns: new[] { "Email", "FullName", "PasswordHash" },
                values: new object[] { "owner@owner", "Owner", "43a0d17178a9d26c9e0fe9a74b0b45e38d32f27aed887a008a54bf6e033bf7b9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "_User",
                keyColumn: "UserID",
                keyValue: 13,
                columns: new[] { "Email", "FullName", "PasswordHash" },
                values: new object[] { "Admin@Admin", "Admin1", "admin123" });
        }
    }
}
