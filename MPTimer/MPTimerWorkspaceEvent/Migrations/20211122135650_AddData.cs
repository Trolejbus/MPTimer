using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MPTimerWorkspaceEvent.Migrations
{
    public partial class AddData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "WorkspaceEvents",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "WorkspaceEvents");
        }
    }
}
