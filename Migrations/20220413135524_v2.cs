using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aplikacija.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administratori",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administratori", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Ban = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.ID);
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
                name: "Vozovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProizvodjacID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozovi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vozovi_Proizvodjaci_ProizvodjacID",
                        column: x => x.ProizvodjacID,
                        principalTable: "Proizvodjaci",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Voznje",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StanicaOdID = table.Column<int>(type: "int", nullable: true),
                    StanicaDoID = table.Column<int>(type: "int", nullable: true),
                    Termin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VozID = table.Column<int>(type: "int", nullable: true),
                    AdministratorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voznje", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Voznje_Administratori_AdministratorID",
                        column: x => x.AdministratorID,
                        principalTable: "Administratori",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Voznje_Vozovi_VozID",
                        column: x => x.VozID,
                        principalTable: "Vozovi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "Recenzije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutorID = table.Column<int>(type: "int", nullable: true),
                    Sadrzaj = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BrojLike = table.Column<int>(type: "int", nullable: false),
                    BrojDislike = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Recenzije_Vozovi_VozID",
                        column: x => x.VozID,
                        principalTable: "Vozovi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Greske",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Opis = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LinijaID = table.Column<int>(type: "int", nullable: true),
                    StanicaID = table.Column<int>(type: "int", nullable: true),
                    UslugaID = table.Column<int>(type: "int", nullable: true),
                    VozID = table.Column<int>(type: "int", nullable: true),
                    VoznjaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Greske", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Greske_Voznje_VoznjaID",
                        column: x => x.VoznjaID,
                        principalTable: "Voznje",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Greske_Vozovi_VozID",
                        column: x => x.VozID,
                        principalTable: "Vozovi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stanice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MestoID = table.Column<int>(type: "int", nullable: true),
                    LinijaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stanice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Stanice_Mesta_MestoID",
                        column: x => x.MestoID,
                        principalTable: "Mesta",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Linije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StanicaOdID = table.Column<int>(type: "int", nullable: true),
                    StanciaDoID = table.Column<int>(type: "int", nullable: true)
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
                name: "IX_Stanice_LinijaID",
                table: "Stanice",
                column: "LinijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Stanice_MestoID",
                table: "Stanice",
                column: "MestoID");

            migrationBuilder.CreateIndex(
                name: "IX_Usluge_StanicaID",
                table: "Usluge",
                column: "StanicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Voznje_AdministratorID",
                table: "Voznje",
                column: "AdministratorID");

            migrationBuilder.CreateIndex(
                name: "IX_Voznje_StanicaDoID",
                table: "Voznje",
                column: "StanicaDoID");

            migrationBuilder.CreateIndex(
                name: "IX_Voznje_StanicaOdID",
                table: "Voznje",
                column: "StanicaOdID");

            migrationBuilder.CreateIndex(
                name: "IX_Voznje_VozID",
                table: "Voznje",
                column: "VozID");

            migrationBuilder.CreateIndex(
                name: "IX_Vozovi_ProizvodjacID",
                table: "Vozovi",
                column: "ProizvodjacID");

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
                name: "FK_Recenzije_Stanice_StanicaID",
                table: "Recenzije",
                column: "StanicaID",
                principalTable: "Stanice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recenzije_Usluge_UslugaID",
                table: "Recenzije",
                column: "UslugaID",
                principalTable: "Usluge",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Stanice_Linije_LinijaID",
                table: "Stanice",
                column: "LinijaID",
                principalTable: "Linije",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stanice_Linije_LinijaID",
                table: "Stanice");

            migrationBuilder.DropTable(
                name: "Greske");

            migrationBuilder.DropTable(
                name: "Karte");

            migrationBuilder.DropTable(
                name: "Recenzije");

            migrationBuilder.DropTable(
                name: "Voznje");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Usluge");

            migrationBuilder.DropTable(
                name: "Administratori");

            migrationBuilder.DropTable(
                name: "Vozovi");

            migrationBuilder.DropTable(
                name: "Proizvodjaci");

            migrationBuilder.DropTable(
                name: "Linije");

            migrationBuilder.DropTable(
                name: "Stanice");

            migrationBuilder.DropTable(
                name: "Mesta");
        }
    }
}
