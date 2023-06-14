using Microsoft.EntityFrameworkCore.Migrations;

namespace Aplikacija.Migrations
{
    public partial class v50 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Greske_Linije_LinijaID",
                table: "Greske");

            migrationBuilder.DropForeignKey(
                name: "FK_Greske_Stanice_StanicaID",
                table: "Greske");

            migrationBuilder.DropForeignKey(
                name: "FK_Greske_Usluge_UslugaID",
                table: "Greske");

            migrationBuilder.DropForeignKey(
                name: "FK_Greske_Voznje_VoznjaID",
                table: "Greske");

            migrationBuilder.DropForeignKey(
                name: "FK_Greske_Vozovi_VozID",
                table: "Greske");

            migrationBuilder.DropForeignKey(
                name: "FK_Stanice_Linije_LinijaID",
                table: "Stanice");

            migrationBuilder.DropForeignKey(
                name: "FK_Stanice_Mesta_MestoID",
                table: "Stanice");

            migrationBuilder.DropForeignKey(
                name: "FK_Voznje_Administratori_AdministratorID",
                table: "Voznje");

            migrationBuilder.DropForeignKey(
                name: "FK_Voznje_Stanice_StanicaDoID",
                table: "Voznje");

            migrationBuilder.DropForeignKey(
                name: "FK_Voznje_Stanice_StanicaOdID",
                table: "Voznje");

            migrationBuilder.DropForeignKey(
                name: "FK_Voznje_Vozovi_VozID",
                table: "Voznje");

            migrationBuilder.DropTable(
                name: "Karte");

            migrationBuilder.DropTable(
                name: "Linije");

            migrationBuilder.DropTable(
                name: "Mesta");

            migrationBuilder.DropTable(
                name: "Recenzije");

            migrationBuilder.DropTable(
                name: "Usluge");

            migrationBuilder.DropTable(
                name: "Vozovi");

            migrationBuilder.DropTable(
                name: "Proizvodjaci");

            migrationBuilder.DropIndex(
                name: "IX_Stanice_LinijaID",
                table: "Stanice");

            migrationBuilder.DropIndex(
                name: "IX_Stanice_MestoID",
                table: "Stanice");

            migrationBuilder.DropIndex(
                name: "IX_Greske_LinijaID",
                table: "Greske");

            migrationBuilder.DropIndex(
                name: "IX_Greske_StanicaID",
                table: "Greske");

            migrationBuilder.DropIndex(
                name: "IX_Greske_UslugaID",
                table: "Greske");

            migrationBuilder.DropIndex(
                name: "IX_Greske_VozID",
                table: "Greske");

            migrationBuilder.DropIndex(
                name: "IX_Greske_VoznjaID",
                table: "Greske");

            migrationBuilder.DropColumn(
                name: "LinijaID",
                table: "Stanice");

            migrationBuilder.DropColumn(
                name: "MestoID",
                table: "Stanice");

            migrationBuilder.DropColumn(
                name: "LinijaID",
                table: "Greske");

            migrationBuilder.DropColumn(
                name: "StanicaID",
                table: "Greske");

            migrationBuilder.DropColumn(
                name: "UslugaID",
                table: "Greske");

            migrationBuilder.DropColumn(
                name: "VozID",
                table: "Greske");

            migrationBuilder.DropColumn(
                name: "VoznjaID",
                table: "Greske");

            migrationBuilder.RenameColumn(
                name: "VozID",
                table: "Voznje",
                newName: "PrevoznikID");

            migrationBuilder.RenameColumn(
                name: "StanicaOdID",
                table: "Voznje",
                newName: "PocetnaStanicaID");

            migrationBuilder.RenameColumn(
                name: "StanicaDoID",
                table: "Voznje",
                newName: "KrajnaStanicaID");

            migrationBuilder.RenameColumn(
                name: "AdministratorID",
                table: "Voznje",
                newName: "KorisnikID");

            migrationBuilder.RenameIndex(
                name: "IX_Voznje_VozID",
                table: "Voznje",
                newName: "IX_Voznje_PrevoznikID");

            migrationBuilder.RenameIndex(
                name: "IX_Voznje_StanicaOdID",
                table: "Voznje",
                newName: "IX_Voznje_PocetnaStanicaID");

            migrationBuilder.RenameIndex(
                name: "IX_Voznje_StanicaDoID",
                table: "Voznje",
                newName: "IX_Voznje_KrajnaStanicaID");

            migrationBuilder.RenameIndex(
                name: "IX_Voznje_AdministratorID",
                table: "Voznje",
                newName: "IX_Voznje_KorisnikID");

            migrationBuilder.RenameColumn(
                name: "Naziv",
                table: "Prevoznici",
                newName: "Username");

            migrationBuilder.AddColumn<string>(
                name: "Mesto",
                table: "Stanice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RecenzijaPrevoznika",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrevoznikID = table.Column<int>(type: "int", nullable: true),
                    Sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecenzijaPrevoznika", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RecenzijaPrevoznika_Korisnici_AutorID",
                        column: x => x.AutorID,
                        principalTable: "Korisnici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecenzijaPrevoznika_Prevoznici_PrevoznikID",
                        column: x => x.PrevoznikID,
                        principalTable: "Prevoznici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecenzijaStanice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StanicaID = table.Column<int>(type: "int", nullable: true),
                    Sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecenzijaStanice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RecenzijaStanice_Korisnici_AutorID",
                        column: x => x.AutorID,
                        principalTable: "Korisnici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecenzijaStanice_Stanice_StanicaID",
                        column: x => x.StanicaID,
                        principalTable: "Stanice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecenzijaPrevoznika_AutorID",
                table: "RecenzijaPrevoznika",
                column: "AutorID");

            migrationBuilder.CreateIndex(
                name: "IX_RecenzijaPrevoznika_PrevoznikID",
                table: "RecenzijaPrevoznika",
                column: "PrevoznikID");

            migrationBuilder.CreateIndex(
                name: "IX_RecenzijaStanice_AutorID",
                table: "RecenzijaStanice",
                column: "AutorID");

            migrationBuilder.CreateIndex(
                name: "IX_RecenzijaStanice_StanicaID",
                table: "RecenzijaStanice",
                column: "StanicaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Voznje_Korisnici_KorisnikID",
                table: "Voznje",
                column: "KorisnikID",
                principalTable: "Korisnici",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voznje_Prevoznici_PrevoznikID",
                table: "Voznje",
                column: "PrevoznikID",
                principalTable: "Prevoznici",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voznje_Stanice_KrajnaStanicaID",
                table: "Voznje",
                column: "KrajnaStanicaID",
                principalTable: "Stanice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voznje_Stanice_PocetnaStanicaID",
                table: "Voznje",
                column: "PocetnaStanicaID",
                principalTable: "Stanice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voznje_Korisnici_KorisnikID",
                table: "Voznje");

            migrationBuilder.DropForeignKey(
                name: "FK_Voznje_Prevoznici_PrevoznikID",
                table: "Voznje");

            migrationBuilder.DropForeignKey(
                name: "FK_Voznje_Stanice_KrajnaStanicaID",
                table: "Voznje");

            migrationBuilder.DropForeignKey(
                name: "FK_Voznje_Stanice_PocetnaStanicaID",
                table: "Voznje");

            migrationBuilder.DropTable(
                name: "RecenzijaPrevoznika");

            migrationBuilder.DropTable(
                name: "RecenzijaStanice");

            migrationBuilder.DropColumn(
                name: "Mesto",
                table: "Stanice");

            migrationBuilder.RenameColumn(
                name: "PrevoznikID",
                table: "Voznje",
                newName: "VozID");

            migrationBuilder.RenameColumn(
                name: "PocetnaStanicaID",
                table: "Voznje",
                newName: "StanicaOdID");

            migrationBuilder.RenameColumn(
                name: "KrajnaStanicaID",
                table: "Voznje",
                newName: "StanicaDoID");

            migrationBuilder.RenameColumn(
                name: "KorisnikID",
                table: "Voznje",
                newName: "AdministratorID");

            migrationBuilder.RenameIndex(
                name: "IX_Voznje_PrevoznikID",
                table: "Voznje",
                newName: "IX_Voznje_VozID");

            migrationBuilder.RenameIndex(
                name: "IX_Voznje_PocetnaStanicaID",
                table: "Voznje",
                newName: "IX_Voznje_StanicaOdID");

            migrationBuilder.RenameIndex(
                name: "IX_Voznje_KrajnaStanicaID",
                table: "Voznje",
                newName: "IX_Voznje_StanicaDoID");

            migrationBuilder.RenameIndex(
                name: "IX_Voznje_KorisnikID",
                table: "Voznje",
                newName: "IX_Voznje_AdministratorID");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Prevoznici",
                newName: "Naziv");

            migrationBuilder.AddColumn<int>(
                name: "LinijaID",
                table: "Stanice",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MestoID",
                table: "Stanice",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LinijaID",
                table: "Greske",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StanicaID",
                table: "Greske",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UslugaID",
                table: "Greske",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VozID",
                table: "Greske",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VoznjaID",
                table: "Greske",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Karte",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Cena = table.Column<double>(type: "float", nullable: false),
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
                        name: "FK_Karte_Voznje_ID",
                        column: x => x.ID,
                        principalTable: "Voznje",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Linije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StanciaDoID = table.Column<int>(type: "int", nullable: true),
                    StanicaOdID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linije", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Linije_Stanice_StanciaDoID",
                        column: x => x.StanciaDoID,
                        principalTable: "Stanice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Linije_Stanice_StanicaOdID",
                        column: x => x.StanicaOdID,
                        principalTable: "Stanice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mesta",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesta", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Proizvodjaci",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvodjaci", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Usluge",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StanicaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usluge", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Usluge_Stanice_StanicaID",
                        column: x => x.StanicaID,
                        principalTable: "Stanice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vozovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PrevoznikID = table.Column<int>(type: "int", nullable: true),
                    ProizvodjacID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozovi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vozovi_Prevoznici_PrevoznikID",
                        column: x => x.PrevoznikID,
                        principalTable: "Prevoznici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vozovi_Proizvodjaci_ProizvodjacID",
                        column: x => x.ProizvodjacID,
                        principalTable: "Proizvodjaci",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recenzije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutorID = table.Column<int>(type: "int", nullable: true),
                    BrojDislike = table.Column<int>(type: "int", nullable: false),
                    BrojLike = table.Column<int>(type: "int", nullable: false),
                    Sadrzaj = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StanicaID = table.Column<int>(type: "int", nullable: true),
                    UslugaID = table.Column<int>(type: "int", nullable: true),
                    VozID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzije", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Recenzije_Korisnici_AutorID",
                        column: x => x.AutorID,
                        principalTable: "Korisnici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recenzije_Stanice_StanicaID",
                        column: x => x.StanicaID,
                        principalTable: "Stanice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recenzije_Usluge_UslugaID",
                        column: x => x.UslugaID,
                        principalTable: "Usluge",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recenzije_Vozovi_VozID",
                        column: x => x.VozID,
                        principalTable: "Vozovi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stanice_LinijaID",
                table: "Stanice",
                column: "LinijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Stanice_MestoID",
                table: "Stanice",
                column: "MestoID");

            migrationBuilder.CreateIndex(
                name: "IX_Greske_LinijaID",
                table: "Greske",
                column: "LinijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Greske_StanicaID",
                table: "Greske",
                column: "StanicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Greske_UslugaID",
                table: "Greske",
                column: "UslugaID");

            migrationBuilder.CreateIndex(
                name: "IX_Greske_VozID",
                table: "Greske",
                column: "VozID");

            migrationBuilder.CreateIndex(
                name: "IX_Greske_VoznjaID",
                table: "Greske",
                column: "VoznjaID");

            migrationBuilder.CreateIndex(
                name: "IX_Karte_KorisnikID",
                table: "Karte",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Linije_StanciaDoID",
                table: "Linije",
                column: "StanciaDoID");

            migrationBuilder.CreateIndex(
                name: "IX_Linije_StanicaOdID",
                table: "Linije",
                column: "StanicaOdID");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_AutorID",
                table: "Recenzije",
                column: "AutorID");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_StanicaID",
                table: "Recenzije",
                column: "StanicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_UslugaID",
                table: "Recenzije",
                column: "UslugaID");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_VozID",
                table: "Recenzije",
                column: "VozID");

            migrationBuilder.CreateIndex(
                name: "IX_Usluge_StanicaID",
                table: "Usluge",
                column: "StanicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Vozovi_PrevoznikID",
                table: "Vozovi",
                column: "PrevoznikID");

            migrationBuilder.CreateIndex(
                name: "IX_Vozovi_ProizvodjacID",
                table: "Vozovi",
                column: "ProizvodjacID");

            migrationBuilder.AddForeignKey(
                name: "FK_Greske_Linije_LinijaID",
                table: "Greske",
                column: "LinijaID",
                principalTable: "Linije",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Greske_Stanice_StanicaID",
                table: "Greske",
                column: "StanicaID",
                principalTable: "Stanice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Greske_Usluge_UslugaID",
                table: "Greske",
                column: "UslugaID",
                principalTable: "Usluge",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Greske_Voznje_VoznjaID",
                table: "Greske",
                column: "VoznjaID",
                principalTable: "Voznje",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Greske_Vozovi_VozID",
                table: "Greske",
                column: "VozID",
                principalTable: "Vozovi",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stanice_Linije_LinijaID",
                table: "Stanice",
                column: "LinijaID",
                principalTable: "Linije",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stanice_Mesta_MestoID",
                table: "Stanice",
                column: "MestoID",
                principalTable: "Mesta",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voznje_Administratori_AdministratorID",
                table: "Voznje",
                column: "AdministratorID",
                principalTable: "Administratori",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voznje_Stanice_StanicaDoID",
                table: "Voznje",
                column: "StanicaDoID",
                principalTable: "Stanice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voznje_Stanice_StanicaOdID",
                table: "Voznje",
                column: "StanicaOdID",
                principalTable: "Stanice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voznje_Vozovi_VozID",
                table: "Voznje",
                column: "VozID",
                principalTable: "Vozovi",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
