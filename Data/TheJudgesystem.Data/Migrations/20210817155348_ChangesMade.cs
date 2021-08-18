using Microsoft.EntityFrameworkCore.Migrations;

namespace TheJudgesystem.Data.Migrations
{
    public partial class ChangesMade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CaseId",
                table: "Juries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Juries_CaseId",
                table: "Juries",
                column: "CaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Juries_Cases_CaseId",
                table: "Juries",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Juries_Cases_CaseId",
                table: "Juries");

            migrationBuilder.DropIndex(
                name: "IX_Juries_CaseId",
                table: "Juries");

            migrationBuilder.DropColumn(
                name: "CaseId",
                table: "Juries");
        }
    }
}
