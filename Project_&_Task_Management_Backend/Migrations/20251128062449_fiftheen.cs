using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project___Task_Management_Backend.Migrations
{
    /// <inheritdoc />
    public partial class fiftheen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "userId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 6, 24, 29, 729, DateTimeKind.Utc).AddTicks(2740));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "userId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 6, 24, 29, 732, DateTimeKind.Utc).AddTicks(4239));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "userId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 6, 24, 29, 732, DateTimeKind.Utc).AddTicks(4341));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "userId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 6, 24, 29, 732, DateTimeKind.Utc).AddTicks(4372));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "userId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 5, 43, 15, 200, DateTimeKind.Utc).AddTicks(3941));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "userId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 5, 43, 15, 239, DateTimeKind.Utc).AddTicks(1409));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "userId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 5, 43, 15, 239, DateTimeKind.Utc).AddTicks(1518));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "userId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 28, 5, 43, 15, 239, DateTimeKind.Utc).AddTicks(1545));
        }
    }
}
