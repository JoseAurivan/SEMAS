using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class newCesta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Demandas",
                table: "cestaBasica",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Demandas",
                table: "cestaBasica");
        }
    }
}
