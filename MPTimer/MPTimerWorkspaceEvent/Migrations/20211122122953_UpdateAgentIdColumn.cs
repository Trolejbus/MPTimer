using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MPTimerWorkspaceEvent.Migrations
{
    public partial class UpdateAgentIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "AgentId",
                table: "WorkspaceEvents",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AgentId",
                table: "WorkspaceEvents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
