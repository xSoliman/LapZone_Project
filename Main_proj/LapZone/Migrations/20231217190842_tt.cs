﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapZone.Migrations
{
    /// <inheritdoc />
    public partial class tt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "_User",
                keyColumn: "UserID",
                keyValue: 13,
                column: "ImagePath",
                value: "Admin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "_User",
                keyColumn: "UserID",
                keyValue: 13,
                column: "ImagePath",
                value: "Images/Users/Admin");
        }
    }
}
