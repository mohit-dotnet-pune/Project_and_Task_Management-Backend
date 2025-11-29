using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project___Task_Management_Backend.Migrations
{
    /// <inheritdoc />
    public partial class fourteen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EmailOtp",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailOtpExpiry",
                table: "users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JwtToken",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "JwtTokenExpiry",
                table: "users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordResetOtp",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetOtpExpiry",
                table: "users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiry",
                table: "users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "userId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EmailConfirmed", "EmailOtp", "EmailOtpExpiry", "JwtToken", "JwtTokenExpiry", "PasswordResetOtp", "PasswordResetOtpExpiry", "RefreshToken", "RefreshTokenExpiry" },
                values: new object[] { new DateTime(2025, 11, 28, 5, 43, 15, 200, DateTimeKind.Utc).AddTicks(3941), false, null, null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "userId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "EmailConfirmed", "EmailOtp", "EmailOtpExpiry", "JwtToken", "JwtTokenExpiry", "PasswordResetOtp", "PasswordResetOtpExpiry", "RefreshToken", "RefreshTokenExpiry" },
                values: new object[] { new DateTime(2025, 11, 28, 5, 43, 15, 239, DateTimeKind.Utc).AddTicks(1409), false, null, null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "userId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "EmailConfirmed", "EmailOtp", "EmailOtpExpiry", "JwtToken", "JwtTokenExpiry", "PasswordResetOtp", "PasswordResetOtpExpiry", "RefreshToken", "RefreshTokenExpiry" },
                values: new object[] { new DateTime(2025, 11, 28, 5, 43, 15, 239, DateTimeKind.Utc).AddTicks(1518), false, null, null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "userId",
                keyValue: 4,
                columns: new[] { "CreatedAt", "EmailConfirmed", "EmailOtp", "EmailOtpExpiry", "JwtToken", "JwtTokenExpiry", "PasswordResetOtp", "PasswordResetOtpExpiry", "RefreshToken", "RefreshTokenExpiry" },
                values: new object[] { new DateTime(2025, 11, 28, 5, 43, 15, 239, DateTimeKind.Utc).AddTicks(1545), false, null, null, null, null, null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "users");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "users");

            migrationBuilder.DropColumn(
                name: "EmailOtp",
                table: "users");

            migrationBuilder.DropColumn(
                name: "EmailOtpExpiry",
                table: "users");

            migrationBuilder.DropColumn(
                name: "JwtToken",
                table: "users");

            migrationBuilder.DropColumn(
                name: "JwtTokenExpiry",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PasswordResetOtp",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PasswordResetOtpExpiry",
                table: "users");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiry",
                table: "users");
        }
    }
}
