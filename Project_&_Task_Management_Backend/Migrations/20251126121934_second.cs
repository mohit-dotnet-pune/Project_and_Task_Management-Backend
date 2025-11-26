using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project___Task_Management_Backend.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_activities_users_userId",
                table: "activities");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_docs_fileId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_tasks_taskId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_users_userId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_projects_docs_fileId",
                table: "projects");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_docs_fileId",
                table: "tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_projects_projectId",
                table: "tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_users_userId",
                table: "tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_userProjects_projects_projectId",
                table: "userProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_userProjects_users_userId",
                table: "userProjects");

            migrationBuilder.AddForeignKey(
                name: "FK_activities_users_userId",
                table: "activities",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_docs_fileId",
                table: "comments",
                column: "fileId",
                principalTable: "docs",
                principalColumn: "fileId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_tasks_taskId",
                table: "comments",
                column: "taskId",
                principalTable: "tasks",
                principalColumn: "taskId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_users_userId",
                table: "comments",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_projects_docs_fileId",
                table: "projects",
                column: "fileId",
                principalTable: "docs",
                principalColumn: "fileId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_docs_fileId",
                table: "tasks",
                column: "fileId",
                principalTable: "docs",
                principalColumn: "fileId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_projects_projectId",
                table: "tasks",
                column: "projectId",
                principalTable: "projects",
                principalColumn: "projectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_users_userId",
                table: "tasks",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_userProjects_projects_projectId",
                table: "userProjects",
                column: "projectId",
                principalTable: "projects",
                principalColumn: "projectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_userProjects_users_userId",
                table: "userProjects",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_activities_users_userId",
                table: "activities");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_docs_fileId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_tasks_taskId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_users_userId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_projects_docs_fileId",
                table: "projects");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_docs_fileId",
                table: "tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_projects_projectId",
                table: "tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_users_userId",
                table: "tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_userProjects_projects_projectId",
                table: "userProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_userProjects_users_userId",
                table: "userProjects");

            migrationBuilder.AddForeignKey(
                name: "FK_activities_users_userId",
                table: "activities",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_docs_fileId",
                table: "comments",
                column: "fileId",
                principalTable: "docs",
                principalColumn: "fileId");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_tasks_taskId",
                table: "comments",
                column: "taskId",
                principalTable: "tasks",
                principalColumn: "taskId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_users_userId",
                table: "comments",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_projects_docs_fileId",
                table: "projects",
                column: "fileId",
                principalTable: "docs",
                principalColumn: "fileId");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_docs_fileId",
                table: "tasks",
                column: "fileId",
                principalTable: "docs",
                principalColumn: "fileId");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_projects_projectId",
                table: "tasks",
                column: "projectId",
                principalTable: "projects",
                principalColumn: "projectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_users_userId",
                table: "tasks",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userProjects_projects_projectId",
                table: "userProjects",
                column: "projectId",
                principalTable: "projects",
                principalColumn: "projectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userProjects_users_userId",
                table: "userProjects",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
