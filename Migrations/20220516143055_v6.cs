using Microsoft.EntityFrameworkCore.Migrations;

namespace Aplikacija.Migrations
{
    public partial class v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ZahtevID",
                table: "Voznje",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Zahtev",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrevoznikID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zahtev", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Zahtev_Prevoznici_PrevoznikID",
                        column: x => x.PrevoznikID,
                        principalTable: "Prevoznici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Voznje_ZahtevID",
                table: "Voznje",
                column: "ZahtevID");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtev_PrevoznikID",
                table: "Zahtev",
                column: "PrevoznikID");

            migrationBuilder.AddForeignKey(
                name: "FK_Voznje_Zahtev_ZahtevID",
                table: "Voznje",
                column: "ZahtevID",
                principalTable: "Zahtev",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voznje_Zahtev_ZahtevID",
                table: "Voznje");

            migrationBuilder.DropTable(
                name: "Zahtev");

            migrationBuilder.DropIndex(
                name: "IX_Voznje_ZahtevID",
                table: "Voznje");

            migrationBuilder.DropColumn(
                name: "ZahtevID",
                table: "Voznje");
        }
    }
}
