using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project___Task_Management_Backend.Migrations
{
    /// <inheritdoc />
    public partial class f : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "docs",
                columns: new[] { "fileId", "fileName", "fileURL" },
                values: new object[,]
                {
                    { 1, "project-1-spec.pdf", "https://example.com/files/project-1-spec.pdf" },
                    { 2, "project-2-spec.pdf", "https://example.com/files/project-2-spec.pdf" },
                    { 3, "project-3-spec.pdf", "https://example.com/files/project-3-spec.pdf" },
                    { 4, "project-4-spec.pdf", "https://example.com/files/project-4-spec.pdf" },
                    { 5, "project-5-spec.pdf", "https://example.com/files/project-5-spec.pdf" },
                    { 6, "project-6-spec.pdf", "https://example.com/files/project-6-spec.pdf" },
                    { 7, "task-1-attachment.png", "https://example.com/files/task-1-attachment.png" },
                    { 8, "task-2-attachment.png", "https://example.com/files/task-2-attachment.png" },
                    { 9, "task-3-attachment.png", "https://example.com/files/task-3-attachment.png" },
                    { 10, "task-4-attachment.png", "https://example.com/files/task-4-attachment.png" },
                    { 11, "task-5-attachment.png", "https://example.com/files/task-5-attachment.png" },
                    { 12, "task-6-attachment.png", "https://example.com/files/task-6-attachment.png" },
                    { 13, "task-7-attachment.png", "https://example.com/files/task-7-attachment.png" },
                    { 14, "task-8-attachment.png", "https://example.com/files/task-8-attachment.png" },
                    { 15, "task-9-attachment.png", "https://example.com/files/task-9-attachment.png" },
                    { 16, "task-10-attachment.png", "https://example.com/files/task-10-attachment.png" },
                    { 17, "task-11-attachment.png", "https://example.com/files/task-11-attachment.png" },
                    { 18, "task-12-attachment.png", "https://example.com/files/task-12-attachment.png" },
                    { 19, "task-13-attachment.png", "https://example.com/files/task-13-attachment.png" },
                    { 20, "task-14-attachment.png", "https://example.com/files/task-14-attachment.png" },
                    { 21, "task-15-attachment.png", "https://example.com/files/task-15-attachment.png" },
                    { 22, "task-16-attachment.png", "https://example.com/files/task-16-attachment.png" },
                    { 23, "task-17-attachment.png", "https://example.com/files/task-17-attachment.png" },
                    { 24, "task-18-attachment.png", "https://example.com/files/task-18-attachment.png" },
                    { 25, "task-19-attachment.png", "https://example.com/files/task-19-attachment.png" },
                    { 26, "task-20-attachment.png", "https://example.com/files/task-20-attachment.png" },
                    { 27, "task-21-attachment.png", "https://example.com/files/task-21-attachment.png" },
                    { 28, "task-22-attachment.png", "https://example.com/files/task-22-attachment.png" },
                    { 29, "task-23-attachment.png", "https://example.com/files/task-23-attachment.png" },
                    { 30, "task-24-attachment.png", "https://example.com/files/task-24-attachment.png" },
                    { 31, "task-25-attachment.png", "https://example.com/files/task-25-attachment.png" },
                    { 32, "task-26-attachment.png", "https://example.com/files/task-26-attachment.png" },
                    { 33, "task-27-attachment.png", "https://example.com/files/task-27-attachment.png" },
                    { 34, "task-28-attachment.png", "https://example.com/files/task-28-attachment.png" },
                    { 35, "task-29-attachment.png", "https://example.com/files/task-29-attachment.png" },
                    { 36, "task-30-attachment.png", "https://example.com/files/task-30-attachment.png" },
                    { 37, "comment-file-1.jpg", "https://example.com/files/comment-file-1.jpg" },
                    { 38, "comment-file-2.jpg", "https://example.com/files/comment-file-2.jpg" },
                    { 39, "comment-file-3.jpg", "https://example.com/files/comment-file-3.jpg" },
                    { 40, "comment-file-4.jpg", "https://example.com/files/comment-file-4.jpg" },
                    { 41, "comment-file-5.jpg", "https://example.com/files/comment-file-5.jpg" },
                    { 42, "comment-file-6.jpg", "https://example.com/files/comment-file-6.jpg" },
                    { 43, "comment-file-7.jpg", "https://example.com/files/comment-file-7.jpg" },
                    { 44, "comment-file-8.jpg", "https://example.com/files/comment-file-8.jpg" },
                    { 45, "comment-file-9.jpg", "https://example.com/files/comment-file-9.jpg" },
                    { 46, "comment-file-10.jpg", "https://example.com/files/comment-file-10.jpg" },
                    { 47, "comment-file-11.jpg", "https://example.com/files/comment-file-11.jpg" },
                    { 48, "comment-file-12.jpg", "https://example.com/files/comment-file-12.jpg" },
                    { 49, "comment-file-13.jpg", "https://example.com/files/comment-file-13.jpg" },
                    { 50, "comment-file-14.jpg", "https://example.com/files/comment-file-14.jpg" },
                    { 51, "comment-file-15.jpg", "https://example.com/files/comment-file-15.jpg" },
                    { 52, "comment-file-16.jpg", "https://example.com/files/comment-file-16.jpg" },
                    { 53, "comment-file-17.jpg", "https://example.com/files/comment-file-17.jpg" },
                    { 54, "comment-file-18.jpg", "https://example.com/files/comment-file-18.jpg" },
                    { 55, "comment-file-19.jpg", "https://example.com/files/comment-file-19.jpg" },
                    { 56, "comment-file-20.jpg", "https://example.com/files/comment-file-20.jpg" },
                    { 57, "comment-file-21.jpg", "https://example.com/files/comment-file-21.jpg" },
                    { 58, "comment-file-22.jpg", "https://example.com/files/comment-file-22.jpg" },
                    { 59, "comment-file-23.jpg", "https://example.com/files/comment-file-23.jpg" },
                    { 60, "comment-file-24.jpg", "https://example.com/files/comment-file-24.jpg" },
                    { 61, "comment-file-25.jpg", "https://example.com/files/comment-file-25.jpg" },
                    { 62, "comment-file-26.jpg", "https://example.com/files/comment-file-26.jpg" },
                    { 63, "comment-file-27.jpg", "https://example.com/files/comment-file-27.jpg" },
                    { 64, "comment-file-28.jpg", "https://example.com/files/comment-file-28.jpg" },
                    { 65, "comment-file-29.jpg", "https://example.com/files/comment-file-29.jpg" },
                    { 66, "comment-file-30.jpg", "https://example.com/files/comment-file-30.jpg" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "userId", "CreatedAt", "EmailConfirmed", "EmailOtp", "EmailOtpExpiry", "JwtToken", "JwtTokenExpiry", "PasswordResetOtp", "PasswordResetOtpExpiry", "RefreshToken", "RefreshTokenExpiry", "userEmail", "userName", "userPassword", "userRole" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 10, 8, 0, 0, 0, DateTimeKind.Utc), true, null, null, null, null, null, null, null, null, "manager1@example.com", "manager1", "hashed-password-1", 1 },
                    { 2, new DateTime(2024, 2, 11, 9, 0, 0, 0, DateTimeKind.Utc), true, null, null, null, null, null, null, null, null, "manager2@example.com", "manager2", "hashed-password-2", 1 },
                    { 3, new DateTime(2024, 3, 12, 10, 0, 0, 0, DateTimeKind.Utc), true, null, null, null, null, null, null, null, null, "manager3@example.com", "manager3", "hashed-password-3", 1 },
                    { 4, new DateTime(2024, 4, 13, 11, 0, 0, 0, DateTimeKind.Utc), true, null, null, null, null, null, null, null, null, "manager4@example.com", "manager4", "hashed-password-4", 1 },
                    { 5, new DateTime(2024, 5, 10, 8, 0, 0, 0, DateTimeKind.Utc), true, null, null, null, null, null, null, null, null, "emp1@example.com", "employee1", "hashed-emp-1", 0 },
                    { 6, new DateTime(2024, 5, 11, 8, 0, 0, 0, DateTimeKind.Utc), true, null, null, null, null, null, null, null, null, "emp2@example.com", "employee2", "hashed-emp-2", 0 },
                    { 7, new DateTime(2024, 5, 12, 8, 0, 0, 0, DateTimeKind.Utc), true, null, null, null, null, null, null, null, null, "emp3@example.com", "employee3", "hashed-emp-3", 0 },
                    { 8, new DateTime(2024, 5, 13, 8, 0, 0, 0, DateTimeKind.Utc), true, null, null, null, null, null, null, null, null, "emp4@example.com", "employee4", "hashed-emp-4", 0 },
                    { 9, new DateTime(2024, 5, 14, 8, 0, 0, 0, DateTimeKind.Utc), true, null, null, null, null, null, null, null, null, "emp5@example.com", "employee5", "hashed-emp-5", 0 },
                    { 10, new DateTime(2024, 5, 15, 8, 0, 0, 0, DateTimeKind.Utc), true, null, null, null, null, null, null, null, null, "emp6@example.com", "employee6", "hashed-emp-6", 0 },
                    { 11, new DateTime(2024, 5, 16, 8, 0, 0, 0, DateTimeKind.Utc), true, null, null, null, null, null, null, null, null, "emp7@example.com", "employee7", "hashed-emp-7", 0 },
                    { 12, new DateTime(2024, 5, 17, 8, 0, 0, 0, DateTimeKind.Utc), true, null, null, null, null, null, null, null, null, "emp8@example.com", "employee8", "hashed-emp-8", 0 }
                });

            migrationBuilder.InsertData(
                table: "projects",
                columns: new[] { "projectId", "fileId", "projectCreatedAt", "projectDescription", "projectEndDate", "projectName", "projectStartDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 7, 1, 9, 0, 0, 0, DateTimeKind.Utc), "Alpha project description", new DateTime(2024, 10, 1, 17, 0, 0, 0, DateTimeKind.Utc), "Project Alpha", new DateTime(2024, 7, 1, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 2, new DateTime(2024, 8, 1, 9, 0, 0, 0, DateTimeKind.Utc), "Beta project description", new DateTime(2024, 11, 1, 17, 0, 0, 0, DateTimeKind.Utc), "Project Beta", new DateTime(2024, 8, 1, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 3, new DateTime(2024, 9, 1, 9, 0, 0, 0, DateTimeKind.Utc), "Gamma project description", new DateTime(2024, 12, 1, 17, 0, 0, 0, DateTimeKind.Utc), "Project Gamma", new DateTime(2024, 9, 1, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 4, new DateTime(2024, 7, 15, 9, 0, 0, 0, DateTimeKind.Utc), "Delta project description", new DateTime(2024, 10, 15, 17, 0, 0, 0, DateTimeKind.Utc), "Project Delta", new DateTime(2024, 7, 15, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 5, new DateTime(2024, 8, 15, 9, 0, 0, 0, DateTimeKind.Utc), "Epsilon project description", new DateTime(2024, 11, 15, 17, 0, 0, 0, DateTimeKind.Utc), "Project Epsilon", new DateTime(2024, 8, 15, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 6, new DateTime(2024, 9, 15, 9, 0, 0, 0, DateTimeKind.Utc), "Zeta project description", new DateTime(2024, 12, 15, 17, 0, 0, 0, DateTimeKind.Utc), "Project Zeta", new DateTime(2024, 9, 15, 9, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "tasks",
                columns: new[] { "taskId", "fileId", "projectId", "taskCreatedAt", "taskDescription", "taskDueDate", "taskPriority", "taskStatus", "taskTitle", "userId" },
                values: new object[,]
                {
                    { 1, 7, 1, new DateTime(2024, 7, 1, 10, 0, 0, 0, DateTimeKind.Utc), "Set up initial database schema", new DateTime(2024, 7, 20, 17, 0, 0, 0, DateTimeKind.Utc), 2, 1, "Alpha - Setup DB", 5 },
                    { 2, 8, 1, new DateTime(2024, 7, 2, 10, 0, 0, 0, DateTimeKind.Utc), "Create API endpoints", new DateTime(2024, 7, 25, 17, 0, 0, 0, DateTimeKind.Utc), 1, 0, "Alpha - API skeleton", 6 },
                    { 3, 9, 1, new DateTime(2024, 7, 3, 10, 0, 0, 0, DateTimeKind.Utc), "Create basic UI", new DateTime(2024, 7, 28, 17, 0, 0, 0, DateTimeKind.Utc), 1, 0, "Alpha - Frontend", 7 },
                    { 4, 10, 1, new DateTime(2024, 7, 4, 10, 0, 0, 0, DateTimeKind.Utc), "Write unit tests", new DateTime(2024, 7, 30, 17, 0, 0, 0, DateTimeKind.Utc), 0, 0, "Alpha - Testing", 8 },
                    { 5, 11, 1, new DateTime(2024, 7, 5, 10, 0, 0, 0, DateTimeKind.Utc), "Deploy to staging", new DateTime(2024, 8, 2, 17, 0, 0, 0, DateTimeKind.Utc), 1, 0, "Alpha - Deploy", 9 },
                    { 6, 12, 2, new DateTime(2024, 8, 1, 10, 0, 0, 0, DateTimeKind.Utc), "Design the main screens", new DateTime(2024, 8, 20, 17, 0, 0, 0, DateTimeKind.Utc), 1, 1, "Beta - UI design", 5 },
                    { 7, 13, 2, new DateTime(2024, 8, 2, 10, 0, 0, 0, DateTimeKind.Utc), "Implement auth flows", new DateTime(2024, 8, 28, 17, 0, 0, 0, DateTimeKind.Utc), 2, 0, "Beta - Authentication", 6 },
                    { 8, 14, 2, new DateTime(2024, 8, 3, 10, 0, 0, 0, DateTimeKind.Utc), "Integrate third-party APIs", new DateTime(2024, 8, 30, 17, 0, 0, 0, DateTimeKind.Utc), 1, 0, "Beta - Integrations", 7 },
                    { 9, 15, 2, new DateTime(2024, 8, 4, 10, 0, 0, 0, DateTimeKind.Utc), "Improve accessibility", new DateTime(2024, 9, 5, 17, 0, 0, 0, DateTimeKind.Utc), 0, 0, "Beta - Accessibility", 8 },
                    { 10, 16, 2, new DateTime(2024, 8, 5, 10, 0, 0, 0, DateTimeKind.Utc), "Performance optimizations", new DateTime(2024, 9, 10, 17, 0, 0, 0, DateTimeKind.Utc), 2, 0, "Beta - Performance", 9 },
                    { 11, 17, 3, new DateTime(2024, 9, 1, 10, 0, 0, 0, DateTimeKind.Utc), "Import legacy data", new DateTime(2024, 9, 25, 17, 0, 0, 0, DateTimeKind.Utc), 2, 0, "Gamma - Data import", 10 },
                    { 12, 18, 3, new DateTime(2024, 9, 2, 10, 0, 0, 0, DateTimeKind.Utc), "Create reporting module", new DateTime(2024, 10, 5, 17, 0, 0, 0, DateTimeKind.Utc), 1, 1, "Gamma - Reporting", 11 },
                    { 13, 19, 3, new DateTime(2024, 9, 3, 10, 0, 0, 0, DateTimeKind.Utc), "Polish the UI", new DateTime(2024, 10, 10, 17, 0, 0, 0, DateTimeKind.Utc), 0, 0, "Gamma - UI polish", 12 },
                    { 14, 20, 3, new DateTime(2024, 9, 4, 10, 0, 0, 0, DateTimeKind.Utc), "Quality Assurance", new DateTime(2024, 10, 15, 17, 0, 0, 0, DateTimeKind.Utc), 1, 0, "Gamma - QA", 5 },
                    { 15, 21, 3, new DateTime(2024, 9, 5, 10, 0, 0, 0, DateTimeKind.Utc), "Create documentation", new DateTime(2024, 10, 20, 17, 0, 0, 0, DateTimeKind.Utc), 0, 0, "Gamma - Docs", 6 },
                    { 16, 22, 4, new DateTime(2024, 7, 16, 10, 0, 0, 0, DateTimeKind.Utc), "CI/CD pipeline", new DateTime(2024, 8, 30, 17, 0, 0, 0, DateTimeKind.Utc), 1, 0, "Delta - Setup CI", 7 },
                    { 17, 23, 4, new DateTime(2024, 7, 17, 10, 0, 0, 0, DateTimeKind.Utc), "Write integration tests", new DateTime(2024, 9, 15, 17, 0, 0, 0, DateTimeKind.Utc), 2, 0, "Delta - Integration tests", 8 },
                    { 18, 24, 4, new DateTime(2024, 7, 18, 10, 0, 0, 0, DateTimeKind.Utc), "Security review", new DateTime(2024, 9, 25, 17, 0, 0, 0, DateTimeKind.Utc), 2, 0, "Delta - Security", 9 },
                    { 19, 25, 4, new DateTime(2024, 7, 19, 10, 0, 0, 0, DateTimeKind.Utc), "Prepare release notes", new DateTime(2024, 9, 30, 17, 0, 0, 0, DateTimeKind.Utc), 0, 0, "Delta - Release prep", 10 },
                    { 20, 26, 4, new DateTime(2024, 7, 20, 10, 0, 0, 0, DateTimeKind.Utc), "Rollout to prod", new DateTime(2024, 10, 5, 17, 0, 0, 0, DateTimeKind.Utc), 2, 0, "Delta - Rollout", 11 },
                    { 21, 27, 5, new DateTime(2024, 8, 16, 10, 0, 0, 0, DateTimeKind.Utc), "Profile and optimize", new DateTime(2024, 9, 30, 17, 0, 0, 0, DateTimeKind.Utc), 2, 0, "Epsilon - Performance", 12 },
                    { 22, 28, 5, new DateTime(2024, 8, 17, 10, 0, 0, 0, DateTimeKind.Utc), "Security audit", new DateTime(2024, 10, 5, 17, 0, 0, 0, DateTimeKind.Utc), 1, 0, "Epsilon - Security review", 5 },
                    { 23, 29, 5, new DateTime(2024, 8, 18, 10, 0, 0, 0, DateTimeKind.Utc), "Add structured logging", new DateTime(2024, 10, 12, 17, 0, 0, 0, DateTimeKind.Utc), 0, 0, "Epsilon - Logging", 6 },
                    { 24, 30, 5, new DateTime(2024, 8, 19, 10, 0, 0, 0, DateTimeKind.Utc), "Set up backups", new DateTime(2024, 10, 20, 17, 0, 0, 0, DateTimeKind.Utc), 1, 0, "Epsilon - Backup", 7 },
                    { 25, 31, 5, new DateTime(2024, 8, 20, 10, 0, 0, 0, DateTimeKind.Utc), "Operational docs", new DateTime(2024, 10, 25, 17, 0, 0, 0, DateTimeKind.Utc), 0, 0, "Epsilon - Documentation", 8 },
                    { 26, 32, 6, new DateTime(2024, 9, 16, 10, 0, 0, 0, DateTimeKind.Utc), "Scale services", new DateTime(2024, 10, 20, 17, 0, 0, 0, DateTimeKind.Utc), 2, 0, "Zeta - Scalability", 9 },
                    { 27, 33, 6, new DateTime(2024, 9, 17, 10, 0, 0, 0, DateTimeKind.Utc), "Perform load tests", new DateTime(2024, 10, 25, 17, 0, 0, 0, DateTimeKind.Utc), 1, 0, "Zeta - Load testing", 10 },
                    { 28, 34, 6, new DateTime(2024, 9, 18, 10, 0, 0, 0, DateTimeKind.Utc), "Add autoscaling", new DateTime(2024, 11, 1, 17, 0, 0, 0, DateTimeKind.Utc), 2, 0, "Zeta - Auto-scaling", 11 },
                    { 29, 35, 6, new DateTime(2024, 9, 19, 10, 0, 0, 0, DateTimeKind.Utc), "Add monitoring & alerts", new DateTime(2024, 11, 5, 17, 0, 0, 0, DateTimeKind.Utc), 1, 0, "Zeta - Monitoring", 12 },
                    { 30, 36, 6, new DateTime(2024, 9, 20, 10, 0, 0, 0, DateTimeKind.Utc), "Technical debt cleanup", new DateTime(2024, 11, 10, 17, 0, 0, 0, DateTimeKind.Utc), 0, 0, "Zeta - Cleanup", 5 }
                });

            migrationBuilder.InsertData(
                table: "userProjects",
                columns: new[] { "userProjectId", "projectId", "userId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 2, 2 },
                    { 4, 3, 2 },
                    { 5, 3, 3 },
                    { 6, 4, 3 },
                    { 7, 4, 4 },
                    { 8, 5, 4 },
                    { 9, 6, 1 },
                    { 10, 1, 5 },
                    { 11, 1, 6 },
                    { 12, 2, 7 },
                    { 13, 2, 8 },
                    { 14, 3, 9 },
                    { 15, 3, 10 },
                    { 16, 4, 11 },
                    { 17, 4, 12 },
                    { 18, 5, 5 },
                    { 19, 5, 6 },
                    { 20, 6, 7 },
                    { 21, 6, 8 },
                    { 22, 5, 9 }
                });

            migrationBuilder.InsertData(
                table: "comments",
                columns: new[] { "commentId", "commentCreatedAt", "commentMessage", "fileId", "taskId", "userId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 2, 10, 0, 0, 0, DateTimeKind.Utc), "Initial DB comment", 37, 1, 5 },
                    { 2, new DateTime(2024, 7, 3, 10, 0, 0, 0, DateTimeKind.Utc), "API skeleton remarks", 38, 2, 6 },
                    { 3, new DateTime(2024, 7, 4, 10, 0, 0, 0, DateTimeKind.Utc), "Frontend notes", 39, 3, 7 },
                    { 4, new DateTime(2024, 7, 5, 10, 0, 0, 0, DateTimeKind.Utc), "Testing plan", 40, 4, 8 },
                    { 5, new DateTime(2024, 7, 6, 10, 0, 0, 0, DateTimeKind.Utc), "Deploy checklist", 41, 5, 9 },
                    { 6, new DateTime(2024, 8, 2, 10, 0, 0, 0, DateTimeKind.Utc), "UI design feedback", 42, 6, 5 },
                    { 7, new DateTime(2024, 8, 3, 10, 0, 0, 0, DateTimeKind.Utc), "Auth flow details", 43, 7, 6 },
                    { 8, new DateTime(2024, 8, 4, 10, 0, 0, 0, DateTimeKind.Utc), "Integration notes", 44, 8, 7 },
                    { 9, new DateTime(2024, 8, 5, 10, 0, 0, 0, DateTimeKind.Utc), "Accessibility fixes", 45, 9, 8 },
                    { 10, new DateTime(2024, 8, 6, 10, 0, 0, 0, DateTimeKind.Utc), "Performance tips", 46, 10, 9 },
                    { 11, new DateTime(2024, 9, 2, 10, 0, 0, 0, DateTimeKind.Utc), "Data import plan", 47, 11, 10 },
                    { 12, new DateTime(2024, 9, 3, 10, 0, 0, 0, DateTimeKind.Utc), "Reporting spec", 48, 12, 11 },
                    { 13, new DateTime(2024, 9, 4, 10, 0, 0, 0, DateTimeKind.Utc), "UI polish suggestions", 49, 13, 12 },
                    { 14, new DateTime(2024, 9, 5, 10, 0, 0, 0, DateTimeKind.Utc), "QA checklist", 50, 14, 5 },
                    { 15, new DateTime(2024, 9, 6, 10, 0, 0, 0, DateTimeKind.Utc), "Docs outline", 51, 15, 6 },
                    { 16, new DateTime(2024, 7, 18, 10, 0, 0, 0, DateTimeKind.Utc), "CI pipeline notes", 52, 16, 7 },
                    { 17, new DateTime(2024, 7, 19, 10, 0, 0, 0, DateTimeKind.Utc), "Integration test coverage", 53, 17, 8 },
                    { 18, new DateTime(2024, 7, 20, 10, 0, 0, 0, DateTimeKind.Utc), "Security review comments", 54, 18, 9 },
                    { 19, new DateTime(2024, 7, 21, 10, 0, 0, 0, DateTimeKind.Utc), "Release prep notes", 55, 19, 10 },
                    { 20, new DateTime(2024, 7, 22, 10, 0, 0, 0, DateTimeKind.Utc), "Rollout instructions", 56, 20, 11 },
                    { 21, new DateTime(2024, 8, 17, 10, 0, 0, 0, DateTimeKind.Utc), "Performance profiling", 57, 21, 12 },
                    { 22, new DateTime(2024, 8, 18, 10, 0, 0, 0, DateTimeKind.Utc), "Security audit notes", 58, 22, 5 },
                    { 23, new DateTime(2024, 8, 19, 10, 0, 0, 0, DateTimeKind.Utc), "Logging config", 59, 23, 6 },
                    { 24, new DateTime(2024, 8, 20, 10, 0, 0, 0, DateTimeKind.Utc), "Backup schedule", 60, 24, 7 },
                    { 25, new DateTime(2024, 8, 21, 10, 0, 0, 0, DateTimeKind.Utc), "Operational docs", 61, 25, 8 },
                    { 26, new DateTime(2024, 9, 18, 10, 0, 0, 0, DateTimeKind.Utc), "Scalability plan", 62, 26, 9 },
                    { 27, new DateTime(2024, 9, 19, 10, 0, 0, 0, DateTimeKind.Utc), "Load test results", 63, 27, 10 },
                    { 28, new DateTime(2024, 9, 20, 10, 0, 0, 0, DateTimeKind.Utc), "Autoscaling config", 64, 28, 11 },
                    { 29, new DateTime(2024, 9, 21, 10, 0, 0, 0, DateTimeKind.Utc), "Monitoring alerts", 65, 29, 12 },
                    { 30, new DateTime(2024, 9, 22, 10, 0, 0, 0, DateTimeKind.Utc), "Cleanup notes", 66, 30, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                table: "comments",
                keyColumn: "commentId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "commentId",
                keyValue: 30);

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
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "userProjects",
                keyColumn: "userProjectId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 66);

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
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "taskId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "userId",
                keyValue: 1);

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
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 36);

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
                table: "projects",
                keyColumn: "projectId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "projects",
                keyColumn: "projectId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "projects",
                keyColumn: "projectId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "userId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "userId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "userId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "userId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "userId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "userId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "userId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "userId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "docs",
                keyColumn: "fileId",
                keyValue: 6);
        }
    }
}
