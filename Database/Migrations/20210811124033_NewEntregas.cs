using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class NewEntregas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeAgente",
                table: "entrega",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unidade",
                table: "entrega",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeAgente",
                table: "entrega");

            migrationBuilder.DropColumn(
                name: "Unidade",
                table: "entrega");
        }
    }
}
