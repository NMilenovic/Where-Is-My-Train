using Microsoft.EntityFrameworkCore.Migrations;

namespace Aplikacija.Migrations
{
    public partial class v62 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voznje_Zahtevi_ZahtevID",
                table: "Voznje");

            migrationBuilder.DropIndex(
                name: "IX_Voznje_ZahtevID",
                table: "Voznje");

            migrationBuilder.DropColumn(
                name: "ZahtevID",
                table: "Voznje");

            migrationBuilder.AddColumn<int>(
                name: "ListaVoznjiID",
                table: "Zahtevi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Zahtevi_ListaVoznjiID",
                table: "Zahtevi",
                column: "ListaVoznjiID");

            migrationBuilder.AddForeignKey(
                name: "FK_Zahtevi_Voznje_ListaVoznjiID",
                table: "Zahtevi",
                column: "ListaVoznjiID",
                principalTable: "Voznje",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zahtevi_Voznje_ListaVoznjiID",
                table: "Zahtevi");

            migrationBuilder.DropIndex(
                name: "IX_Zahtevi_ListaVoznjiID",
                table: "Zahtevi");

            migrationBuilder.DropColumn(
                name: "ListaVoznjiID",
                table: "Zahtevi");

            migrationBuilder.AddColumn<int>(
                name: "ZahtevID",
                table: "Voznje",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voznje_ZahtevID",
                table: "Voznje",
                column: "ZahtevID");

            migrationBuilder.AddForeignKey(
                name: "FK_Voznje_Zahtevi_ZahtevID",
                table: "Voznje",
                column: "ZahtevID",
                principalTable: "Zahtevi",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
