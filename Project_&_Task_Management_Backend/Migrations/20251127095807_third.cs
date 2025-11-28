using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project___Task_Management_Backend.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userProjects_projects_projectId",
                table: "userProjects");

            migrationBuilder.AddForeignKey(
                name: "FK_userProjects_projects_projectId",
                table: "userProjects",
                column: "projectId",
                principalTable: "projects",
                principalColumn: "projectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userProjects_projects_projectId",
                table: "userProjects");

            migrationBuilder.AddForeignKey(
                name: "FK_userProjects_projects_projectId",
                table: "userProjects",
                column: "projectId",
                principalTable: "projects",
                principalColumn: "projectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
