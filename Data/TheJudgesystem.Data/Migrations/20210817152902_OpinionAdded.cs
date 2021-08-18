using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheJudgesystem.Data.Migrations
{
    public partial class OpinionAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Opinion",
                table: "Jurymembers");

            migrationBuilder.AddColumn<int>(
                name: "OpinionId",
                table: "Jurymembers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Opinions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Guiltiness = table.Column<int>(type: "int", nullable: false),
                    CaseId = table.Column<int>(type: "int", nullable: true),
                    JurymemberId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opinions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opinions_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opinions_Jurymembers_JurymemberId",
                        column: x => x.JurymemberId,
                        principalTable: "Jurymembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jurymembers_OpinionId",
                table: "Jurymembers",
                column: "OpinionId");

            migrationBuilder.CreateIndex(
                name: "IX_Opinions_CaseId",
                table: "Opinions",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Opinions_IsDeleted",
                table: "Opinions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Opinions_JurymemberId",
                table: "Opinions",
                column: "JurymemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jurymembers_Opinions_OpinionId",
                table: "Jurymembers",
                column: "OpinionId",
                principalTable: "Opinions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jurymembers_Opinions_OpinionId",
                table: "Jurymembers");

            migrationBuilder.DropTable(
                name: "Opinions");

            migrationBuilder.DropIndex(
                name: "IX_Jurymembers_OpinionId",
                table: "Jurymembers");

            migrationBuilder.DropColumn(
                name: "OpinionId",
                table: "Jurymembers");

            migrationBuilder.AddColumn<int>(
                name: "Opinion",
                table: "Jurymembers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
