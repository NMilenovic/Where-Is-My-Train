using Microsoft.EntityFrameworkCore.Migrations;

namespace Aplikacija.Migrations
{
    public partial class konacna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voznje_Korisnici_KorisnikID",
                table: "Voznje");

            migrationBuilder.DropIndex(
                name: "IX_Voznje_KorisnikID",
                table: "Voznje");

            migrationBuilder.DropColumn(
                name: "KorisnikID",
                table: "Voznje");

            migrationBuilder.CreateTable(
                name: "Karte",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cena = table.Column<double>(type: "float", nullable: false),
                    VoznjaID = table.Column<int>(type: "int", nullable: true),
                    KorisnikID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karte", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Karte_Korisnici_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Karte_Voznje_VoznjaID",
                        column: x => x.VoznjaID,
                        principalTable: "Voznje",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Karte_KorisnikID",
                table: "Karte",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Karte_VoznjaID",
                table: "Karte",
                column: "VoznjaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Karte");

            migrationBuilder.AddColumn<int>(
                name: "KorisnikID",
                table: "Voznje",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voznje_KorisnikID",
                table: "Voznje",
                column: "KorisnikID");

            migrationBuilder.AddForeignKey(
                name: "FK_Voznje_Korisnici_KorisnikID",
                table: "Voznje",
                column: "KorisnikID",
                principalTable: "Korisnici",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
