using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class EntregaUnidadeUPD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnidadeEntrega",
                table: "entrega",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnidadeEntrega",
                table: "entrega");
        }
    }
}
