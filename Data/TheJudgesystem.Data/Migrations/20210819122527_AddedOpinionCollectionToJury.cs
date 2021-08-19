using Microsoft.EntityFrameworkCore.Migrations;

namespace TheJudgesystem.Data.Migrations
{
    public partial class AddedOpinionCollectionToJury : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JuryId",
                table: "Opinions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Opinions_JuryId",
                table: "Opinions",
                column: "JuryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Opinions_Juries_JuryId",
                table: "Opinions",
                column: "JuryId",
                principalTable: "Juries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opinions_Juries_JuryId",
                table: "Opinions");

            migrationBuilder.DropIndex(
                name: "IX_Opinions_JuryId",
                table: "Opinions");

            migrationBuilder.DropColumn(
                name: "JuryId",
                table: "Opinions");
        }
    }
}
