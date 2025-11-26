using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project___Task_Management_Backend.Migrations
{
    /// <inheritdoc />
    public partial class fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_projects_projectId",
                table: "tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_userProjects_projects_projectId",
                table: "userProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_userProjects_users_userId",
                table: "userProjects");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_projects_projectId",
                table: "tasks",
                column: "projectId",
                principalTable: "projects",
                principalColumn: "projectId");

            migrationBuilder.AddForeignKey(
                name: "FK_userProjects_projects_projectId",
                table: "userProjects",
                column: "projectId",
                principalTable: "projects",
                principalColumn: "projectId");

            migrationBuilder.AddForeignKey(
                name: "FK_userProjects_users_userId",
                table: "userProjects",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_projects_projectId",
                table: "tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_userProjects_projects_projectId",
                table: "userProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_userProjects_users_userId",
                table: "userProjects");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_projects_projectId",
                table: "tasks",
                column: "projectId",
                principalTable: "projects",
                principalColumn: "projectId",
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
