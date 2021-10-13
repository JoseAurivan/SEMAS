using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class EntregaUnidadeUPD03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnidadeEntrega",
                table: "entrega",
                newName: "Unidade");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Unidade",
                table: "entrega",
                newName: "UnidadeEntrega");
        }
    }
}
