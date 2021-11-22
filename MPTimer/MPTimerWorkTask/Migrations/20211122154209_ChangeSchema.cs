using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MPTimerWorkTask.Migrations
{
    public partial class ChangeSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "WorkTask");

            migrationBuilder.RenameTable(
                name: "WorkTasks",
                newName: "WorkTasks",
                newSchema: "WorkTask");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "WorkTasks",
                schema: "WorkTask",
                newName: "WorkTasks");
        }
    }
}
