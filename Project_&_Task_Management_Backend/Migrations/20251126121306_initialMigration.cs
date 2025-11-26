using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project___Task_Management_Backend.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "docs",
                columns: table => new
                {
                    fileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fileURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_docs", x => x.fileId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    projectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    projectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    projectDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    projectStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    projectEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    projectCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fileId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.projectId);
                    table.ForeignKey(
                        name: "FK_projects_docs_fileId",
                        column: x => x.fileId,
                        principalTable: "docs",
                        principalColumn: "fileId");
                });

            migrationBuilder.CreateTable(
                name: "activities",
                columns: table => new
                {
                    activityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    activityDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    activityEntityType = table.Column<int>(type: "int", nullable: false),
                    activityEntityId = table.Column<int>(type: "int", nullable: false),
                    activityCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activities", x => x.activityId);
                    table.ForeignKey(
                        name: "FK_activities_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    taskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    projectId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    fileId = table.Column<int>(type: "int", nullable: true),
                    taskTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    taskDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    taskPriority = table.Column<int>(type: "int", nullable: false),
                    taskStatus = table.Column<int>(type: "int", nullable: false),
                    taskDueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    taskCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => x.taskId);
                    table.ForeignKey(
                        name: "FK_tasks_docs_fileId",
                        column: x => x.fileId,
                        principalTable: "docs",
                        principalColumn: "fileId");
                    table.ForeignKey(
                        name: "FK_tasks_projects_projectId",
                        column: x => x.projectId,
                        principalTable: "projects",
                        principalColumn: "projectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tasks_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userProjects",
                columns: table => new
                {
                    userProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    projectId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userProjects", x => x.userProjectId);
                    table.ForeignKey(
                        name: "FK_userProjects_projects_projectId",
                        column: x => x.projectId,
                        principalTable: "projects",
                        principalColumn: "projectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userProjects_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    commentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    taskId = table.Column<int>(type: "int", nullable: false),
                    fileId = table.Column<int>(type: "int", nullable: true),
                    userId = table.Column<int>(type: "int", nullable: false),
                    commentMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    commentCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.commentId);
                    table.ForeignKey(
                        name: "FK_comments_docs_fileId",
                        column: x => x.fileId,
                        principalTable: "docs",
                        principalColumn: "fileId");
                    table.ForeignKey(
                        name: "FK_comments_tasks_taskId",
                        column: x => x.taskId,
                        principalTable: "tasks",
                        principalColumn: "taskId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comments_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_activities_userId",
                table: "activities",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_fileId",
                table: "comments",
                column: "fileId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_taskId",
                table: "comments",
                column: "taskId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_userId",
                table: "comments",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_projects_fileId",
                table: "projects",
                column: "fileId");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_fileId",
                table: "tasks",
                column: "fileId");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_projectId",
                table: "tasks",
                column: "projectId");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_userId",
                table: "tasks",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_userProjects_projectId",
                table: "userProjects",
                column: "projectId");

            migrationBuilder.CreateIndex(
                name: "IX_userProjects_userId",
                table: "userProjects",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activities");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "userProjects");

            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "docs");
        }
    }
}
