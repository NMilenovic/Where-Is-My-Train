using Microsoft.EntityFrameworkCore.Migrations;

namespace Aplikacija.Migrations
{
    public partial class v53 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VoznjaID",
                table: "Greske",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Greske_VoznjaID",
                table: "Greske",
                column: "VoznjaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Greske_Voznje_VoznjaID",
                table: "Greske",
                column: "VoznjaID",
                principalTable: "Voznje",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Greske_Voznje_VoznjaID",
                table: "Greske");

            migrationBuilder.DropIndex(
                name: "IX_Greske_VoznjaID",
                table: "Greske");

            migrationBuilder.DropColumn(
                name: "VoznjaID",
                table: "Greske");
        }
    }
}
