using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MPSourceControl.Migrations
{
    public partial class SourceControlStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Status",
                schema: "SourceControl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceControlId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Status_SourceControl_SourceControlId",
                        column: x => x.SourceControlId,
                        principalSchema: "SourceControl",
                        principalTable: "SourceControl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Status_SourceControlId",
                schema: "SourceControl",
                table: "Status",
                column: "SourceControlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Status",
                schema: "SourceControl");
        }
    }
}
