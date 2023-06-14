using Microsoft.EntityFrameworkCore.Migrations;

namespace Aplikacija.Migrations
{
    public partial class v51 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecenzijaPrevoznika_Korisnici_AutorID",
                table: "RecenzijaPrevoznika");

            migrationBuilder.DropForeignKey(
                name: "FK_RecenzijaPrevoznika_Prevoznici_PrevoznikID",
                table: "RecenzijaPrevoznika");

            migrationBuilder.DropForeignKey(
                name: "FK_RecenzijaStanice_Korisnici_AutorID",
                table: "RecenzijaStanice");

            migrationBuilder.DropForeignKey(
                name: "FK_RecenzijaStanice_Stanice_StanicaID",
                table: "RecenzijaStanice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecenzijaStanice",
                table: "RecenzijaStanice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecenzijaPrevoznika",
                table: "RecenzijaPrevoznika");

            migrationBuilder.RenameTable(
                name: "RecenzijaStanice",
                newName: "RecenzijeStanice");

            migrationBuilder.RenameTable(
                name: "RecenzijaPrevoznika",
                newName: "RecenzijePrevoznika");

            migrationBuilder.RenameIndex(
                name: "IX_RecenzijaStanice_StanicaID",
                table: "RecenzijeStanice",
                newName: "IX_RecenzijeStanice_StanicaID");

            migrationBuilder.RenameIndex(
                name: "IX_RecenzijaStanice_AutorID",
                table: "RecenzijeStanice",
                newName: "IX_RecenzijeStanice_AutorID");

            migrationBuilder.RenameIndex(
                name: "IX_RecenzijaPrevoznika_PrevoznikID",
                table: "RecenzijePrevoznika",
                newName: "IX_RecenzijePrevoznika_PrevoznikID");

            migrationBuilder.RenameIndex(
                name: "IX_RecenzijaPrevoznika_AutorID",
                table: "RecenzijePrevoznika",
                newName: "IX_RecenzijePrevoznika_AutorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecenzijeStanice",
                table: "RecenzijeStanice",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecenzijePrevoznika",
                table: "RecenzijePrevoznika",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RecenzijePrevoznika_Korisnici_AutorID",
                table: "RecenzijePrevoznika",
                column: "AutorID",
                principalTable: "Korisnici",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecenzijePrevoznika_Prevoznici_PrevoznikID",
                table: "RecenzijePrevoznika",
                column: "PrevoznikID",
                principalTable: "Prevoznici",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecenzijeStanice_Korisnici_AutorID",
                table: "RecenzijeStanice",
                column: "AutorID",
                principalTable: "Korisnici",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecenzijeStanice_Stanice_StanicaID",
                table: "RecenzijeStanice",
                column: "StanicaID",
                principalTable: "Stanice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecenzijePrevoznika_Korisnici_AutorID",
                table: "RecenzijePrevoznika");

            migrationBuilder.DropForeignKey(
                name: "FK_RecenzijePrevoznika_Prevoznici_PrevoznikID",
                table: "RecenzijePrevoznika");

            migrationBuilder.DropForeignKey(
                name: "FK_RecenzijeStanice_Korisnici_AutorID",
                table: "RecenzijeStanice");

            migrationBuilder.DropForeignKey(
                name: "FK_RecenzijeStanice_Stanice_StanicaID",
                table: "RecenzijeStanice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecenzijeStanice",
                table: "RecenzijeStanice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecenzijePrevoznika",
                table: "RecenzijePrevoznika");

            migrationBuilder.RenameTable(
                name: "RecenzijeStanice",
                newName: "RecenzijaStanice");

            migrationBuilder.RenameTable(
                name: "RecenzijePrevoznika",
                newName: "RecenzijaPrevoznika");

            migrationBuilder.RenameIndex(
                name: "IX_RecenzijeStanice_StanicaID",
                table: "RecenzijaStanice",
                newName: "IX_RecenzijaStanice_StanicaID");

            migrationBuilder.RenameIndex(
                name: "IX_RecenzijeStanice_AutorID",
                table: "RecenzijaStanice",
                newName: "IX_RecenzijaStanice_AutorID");

            migrationBuilder.RenameIndex(
                name: "IX_RecenzijePrevoznika_PrevoznikID",
                table: "RecenzijaPrevoznika",
                newName: "IX_RecenzijaPrevoznika_PrevoznikID");

            migrationBuilder.RenameIndex(
                name: "IX_RecenzijePrevoznika_AutorID",
                table: "RecenzijaPrevoznika",
                newName: "IX_RecenzijaPrevoznika_AutorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecenzijaStanice",
                table: "RecenzijaStanice",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecenzijaPrevoznika",
                table: "RecenzijaPrevoznika",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RecenzijaPrevoznika_Korisnici_AutorID",
                table: "RecenzijaPrevoznika",
                column: "AutorID",
                principalTable: "Korisnici",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecenzijaPrevoznika_Prevoznici_PrevoznikID",
                table: "RecenzijaPrevoznika",
                column: "PrevoznikID",
                principalTable: "Prevoznici",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecenzijaStanice_Korisnici_AutorID",
                table: "RecenzijaStanice",
                column: "AutorID",
                principalTable: "Korisnici",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecenzijaStanice_Stanice_StanicaID",
                table: "RecenzijaStanice",
                column: "StanicaID",
                principalTable: "Stanice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
