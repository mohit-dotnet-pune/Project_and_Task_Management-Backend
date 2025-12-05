using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project___Task_Management_Backend.Migrations
{
    /// <inheritdoc />
    public partial class sixteen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "activities",
                keyColumn: "activityId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "activities",
                keyColumn: "activityId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "activities",
                keyColumn: "activityId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "activities",
                keyColumn: "activityId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "activities",
                keyColumn: "activityId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "activities",
                keyColumn: "activityId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "userId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "projects",
                keyColumn: "projectId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "projects",
                keyColumn: "projectId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "projects",
                keyColumn: "projectId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "userId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "userId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "userId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "docs",
                columns: new[] { "fileId", "fileName", "fileURL" },
                values: new object[,]
                {
                    { 1, "homepage-design.sketch", "/files/designs/homepage-design.sketch" },
                    { 2, "shopping-cart-specs.pdf", "/files/specs/shopping-cart-specs.pdf" },
                    { 3, "ui-design-mockups.fig", "/files/designs/ui-design-mockups.fig" },
                    { 4, "database-schema.sql", "/files/sql/database-schema.sql" }
                });

            migrationBuilder.InsertData(
                table: "projects",
                columns: new[] { "projectId", "fileId", "projectCreatedAt", "projectDescription", "projectEndDate", "projectName", "projectStartDate" },
                values: new object[,]
                {
                    { 2, null, new DateTime(2023, 12, 31, 10, 0, 0, 0, DateTimeKind.Utc), "Create a cross-platform mobile application", new DateTime(2024, 2, 29, 10, 0, 0, 0, DateTimeKind.Utc), "Mobile App Development", new DateTime(2023, 12, 31, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, null, new DateTime(2024, 1, 8, 10, 0, 0, 0, DateTimeKind.Utc), "Migrate from legacy database to cloud solution", new DateTime(2024, 2, 14, 10, 0, 0, 0, DateTimeKind.Utc), "Database Migration", new DateTime(2024, 1, 8, 10, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "userId", "CreatedAt", "EmailConfirmed", "EmailOtp", "EmailOtpExpiry", "JwtToken", "JwtTokenExpiry", "PasswordResetOtp", "PasswordResetOtpExpiry", "RefreshToken", "RefreshTokenExpiry", "userEmail", "userName", "userPassword", "userRole" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 28, 6, 24, 29, 729, DateTimeKind.Utc).AddTicks(2740), false, null, null, null, null, null, null, null, null, "john.doe@company.com", "John Doe", "hashed_password_1", 1 },
                    { 2, new DateTime(2025, 11, 28, 6, 24, 29, 732, DateTimeKind.Utc).AddTicks(4239), false, null, null, null, null, null, null, null, null, "jane.smith@company.com", "Jane Smith", "hashed_password_2", 0 },
                    { 3, new DateTime(2025, 11, 28, 6, 24, 29, 732, DateTimeKind.Utc).AddTicks(4341), false, null, null, null, null, null, null, null, null, "mike.johnson@company.com", "Mike Johnson", "hashed_password_3", 0 },
                    { 4, new DateTime(2025, 11, 28, 6, 24, 29, 732, DateTimeKind.Utc).AddTicks(4372), false, null, null, null, null, null, null, null, null, "sarah.wilson@company.com", "Sarah Wilson", "hashed_password_4", 0 }
                });

            migrationBuilder.InsertData(
                table: "activities",
                columns: new[] { "activityId", "activityCreatedAt", "activityDescription", "activityEntityId", "activityEntityType", "userId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 16, 10, 0, 0, 0, DateTimeKind.Utc), "Created project E-Commerce Website", 1, 0, 1 },
                    { 2, new DateTime(2024, 1, 5, 10, 0, 0, 0, DateTimeKind.Utc), "Completed task Design Homepage", 1, 1, 2 },
                    { 3, new DateTime(2024, 1, 10, 10, 0, 0, 0, DateTimeKind.Utc), "Started working on Implement Shopping Cart", 2, 1, 3 },
                    { 4, new DateTime(2024, 1, 11, 10, 0, 0, 0, DateTimeKind.Utc), "Commented on shopping cart implementation", 4, 3, 1 },
                    { 5, new DateTime(2024, 1, 12, 10, 0, 0, 0, DateTimeKind.Utc), "Uploaded design file ui-design-mockups.fig", 3, 2, 4 },
                    { 6, new DateTime(2024, 1, 14, 10, 0, 0, 0, DateTimeKind.Utc), "Completed data analysis for migration", 6, 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "projects",
                columns: new[] { "projectId", "fileId", "projectCreatedAt", "projectDescription", "projectEndDate", "projectName", "projectStartDate" },
                values: new object[] { 1, 4, new DateTime(2023, 12, 16, 10, 0, 0, 0, DateTimeKind.Utc), "Build a modern e-commerce platform with React and .NET", new DateTime(2024, 3, 15, 10, 0, 0, 0, DateTimeKind.Utc), "E-Commerce Website", new DateTime(2023, 12, 16, 10, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "tasks",
                columns: new[] { "taskId", "fileId", "projectId", "taskCreatedAt", "taskDescription", "taskDueDate", "taskPriority", "taskStatus", "taskTitle", "userId" },
                values: new object[,]
                {
                    { 4, null, 2, new DateTime(2024, 1, 3, 10, 0, 0, 0, DateTimeKind.Utc), "Design mobile app interface and user experience", new DateTime(2024, 1, 25, 10, 0, 0, 0, DateTimeKind.Utc), 1, 1, "UI/UX Design", 4 },
                    { 5, null, 2, new DateTime(2024, 1, 7, 10, 0, 0, 0, DateTimeKind.Utc), "Connect mobile app to backend REST API", new DateTime(2024, 2, 4, 10, 0, 0, 0, DateTimeKind.Utc), 2, 0, "Backend API Integration", 2 },
                    { 6, null, 3, new DateTime(2024, 1, 8, 10, 0, 0, 0, DateTimeKind.Utc), "Analyze existing database structure and data", new DateTime(2024, 1, 13, 10, 0, 0, 0, DateTimeKind.Utc), 1, 2, "Data Analysis", 3 },
                    { 7, null, 3, new DateTime(2024, 1, 10, 10, 0, 0, 0, DateTimeKind.Utc), "Create scripts to migrate data to new cloud database", new DateTime(2024, 1, 30, 10, 0, 0, 0, DateTimeKind.Utc), 2, 1, "Migration Script Development", 4 }
                });

            migrationBuilder.InsertData(
                table: "userProjects",
                columns: new[] { "userProjectId", "projectId", "userId" },
                values: new object[,]
                {
                    { 4, 2, 1 },
                    { 5, 2, 2 },
                    { 6, 2, 4 },
                    { 7, 3, 1 },
                    { 8, 3, 3 },
                    { 9, 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "comments",
                columns: new[] { "commentId", "commentCreatedAt", "commentMessage", "fileId", "taskId", "userId" },
                values: new object[,]
                {
                    { 5, new DateTime(2024, 1, 12, 10, 0, 0, 0, DateTimeKind.Utc), "The design is 80% complete. Waiting for feedback on the color scheme.", null, 4, 4 },
                    { 6, new DateTime(2024, 1, 14, 10, 0, 0, 0, DateTimeKind.Utc), "Data analysis completed. Found some inconsistencies in the legacy data.", null, 6, 3 }
                });

            migrationBuilder.InsertData(
                table: "tasks",
                columns: new[] { "taskId", "fileId", "projectId", "taskCreatedAt", "taskDescription", "taskDueDate", "taskPriority", "taskStatus", "taskTitle", "userId" },
                values: new object[,]
                {
                    { 1, 3, 1, new DateTime(2023, 12, 21, 10, 0, 0, 0, DateTimeKind.Utc), "Create responsive homepage design with product listings", new DateTime(2024, 1, 5, 10, 0, 0, 0, DateTimeKind.Utc), 2, 2, "Design Homepage", 2 },
                    { 2, null, 1, new DateTime(2023, 12, 26, 10, 0, 0, 0, DateTimeKind.Utc), "Develop shopping cart functionality with add/remove items", new DateTime(2024, 1, 30, 10, 0, 0, 0, DateTimeKind.Utc), 2, 1, "Implement Shopping Cart", 3 },
                    { 3, null, 1, new DateTime(2023, 12, 31, 10, 0, 0, 0, DateTimeKind.Utc), "Implement login/register system with JWT tokens", new DateTime(2024, 2, 9, 10, 0, 0, 0, DateTimeKind.Utc), 1, 0, "User Authentication", 2 }
                });

            migrationBuilder.InsertData(
                table: "userProjects",
                columns: new[] { "userProjectId", "projectId", "userId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "comments",
                columns: new[] { "commentId", "commentCreatedAt", "commentMessage", "fileId", "taskId", "userId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 7, 10, 0, 0, 0, DateTimeKind.Utc), "Great work on the homepage design! The layout looks clean and modern.", 1, 1, 1 },
                    { 2, new DateTime(2024, 1, 8, 10, 0, 0, 0, DateTimeKind.Utc), "Thanks! I'll start working on the mobile responsive version next.", null, 1, 2 },
                    { 3, new DateTime(2024, 1, 10, 10, 0, 0, 0, DateTimeKind.Utc), "Having some issues with the cart state management. Need to review the approach.", null, 2, 3 },
                    { 4, new DateTime(2024, 1, 11, 10, 0, 0, 0, DateTimeKind.Utc), "Check the updated specifications document for the cart requirements.", 2, 2, 1 }
                });
        }
    }
}
