using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MPTimerAgent.Migrations
{
    public partial class ChangeSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Agent");

            migrationBuilder.RenameTable(
                name: "AgentRuntime",
                newName: "AgentRuntime",
                newSchema: "Agent");

            migrationBuilder.RenameTable(
                name: "Agent",
                newName: "Agent",
                newSchema: "Agent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "AgentRuntime",
                schema: "Agent",
                newName: "AgentRuntime");

            migrationBuilder.RenameTable(
                name: "Agent",
                schema: "Agent",
                newName: "Agent");
        }
    }
}
