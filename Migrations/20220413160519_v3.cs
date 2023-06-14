using Microsoft.EntityFrameworkCore.Migrations;

namespace Aplikacija.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrevoznikID",
                table: "Vozovi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Prevoznici",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prevoznici", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vozovi_PrevoznikID",
                table: "Vozovi",
                column: "PrevoznikID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vozovi_Prevoznici_PrevoznikID",
                table: "Vozovi",
                column: "PrevoznikID",
                principalTable: "Prevoznici",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vozovi_Prevoznici_PrevoznikID",
                table: "Vozovi");

            migrationBuilder.DropTable(
                name: "Prevoznici");

            migrationBuilder.DropIndex(
                name: "IX_Vozovi_PrevoznikID",
                table: "Vozovi");

            migrationBuilder.DropColumn(
                name: "PrevoznikID",
                table: "Vozovi");
        }
    }
}
