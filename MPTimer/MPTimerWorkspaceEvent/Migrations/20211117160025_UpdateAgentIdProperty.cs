using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MPTimerWorkspaceEvent.Migrations
{
    public partial class UpdateAgentIdProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InstanceId",
                table: "WorkspaceEvents",
                newName: "AgentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AgentId",
                table: "WorkspaceEvents",
                newName: "InstanceId");
        }
    }
}
