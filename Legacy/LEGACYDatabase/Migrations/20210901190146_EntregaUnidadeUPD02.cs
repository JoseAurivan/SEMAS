using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class EntregaUnidadeUPD02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unidade",
                table: "entrega");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Unidade",
                table: "entrega",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
