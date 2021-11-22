using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MPTimerWorkspaceEvent.Migrations
{
    public partial class ChangeSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "WorkspaceEvent");

            migrationBuilder.RenameTable(
                name: "WorkspaceEvents",
                newName: "WorkspaceEvents",
                newSchema: "WorkspaceEvent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "WorkspaceEvents",
                schema: "WorkspaceEvent",
                newName: "WorkspaceEvents");
        }
    }
}
