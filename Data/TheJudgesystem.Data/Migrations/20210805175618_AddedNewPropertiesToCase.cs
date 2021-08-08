using Microsoft.EntityFrameworkCore.Migrations;

namespace TheJudgesystem.Data.Migrations
{
    public partial class AddedNewPropertiesToCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JudgeDecision",
                table: "Cases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LawyerDefence",
                table: "Cases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProsecutorDecision",
                table: "Cases",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JudgeDecision",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "LawyerDefence",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "ProsecutorDecision",
                table: "Cases");
        }
    }
}
