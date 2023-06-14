using Microsoft.EntityFrameworkCore.Migrations;

namespace Aplikacija.Migrations
{
    public partial class v61 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voznje_Zahtev_ZahtevID",
                table: "Voznje");

            migrationBuilder.DropForeignKey(
                name: "FK_Zahtev_Prevoznici_PrevoznikID",
                table: "Zahtev");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zahtev",
                table: "Zahtev");

            migrationBuilder.RenameTable(
                name: "Zahtev",
                newName: "Zahtevi");

            migrationBuilder.RenameIndex(
                name: "IX_Zahtev_PrevoznikID",
                table: "Zahtevi",
                newName: "IX_Zahtevi_PrevoznikID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zahtevi",
                table: "Zahtevi",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Voznje_Zahtevi_ZahtevID",
                table: "Voznje",
                column: "ZahtevID",
                principalTable: "Zahtevi",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zahtevi_Prevoznici_PrevoznikID",
                table: "Zahtevi",
                column: "PrevoznikID",
                principalTable: "Prevoznici",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voznje_Zahtevi_ZahtevID",
                table: "Voznje");

            migrationBuilder.DropForeignKey(
                name: "FK_Zahtevi_Prevoznici_PrevoznikID",
                table: "Zahtevi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zahtevi",
                table: "Zahtevi");

            migrationBuilder.RenameTable(
                name: "Zahtevi",
                newName: "Zahtev");

            migrationBuilder.RenameIndex(
                name: "IX_Zahtevi_PrevoznikID",
                table: "Zahtev",
                newName: "IX_Zahtev_PrevoznikID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zahtev",
                table: "Zahtev",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Voznje_Zahtev_ZahtevID",
                table: "Voznje",
                column: "ZahtevID",
                principalTable: "Zahtev",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zahtev_Prevoznici_PrevoznikID",
                table: "Zahtev",
                column: "PrevoznikID",
                principalTable: "Prevoznici",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
