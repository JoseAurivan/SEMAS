using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class DeterminacaoRecomendacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DeterminacaoJuridica",
                table: "cestaBasica",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RecomendacaoTecnica",
                table: "cestaBasica",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeterminacaoJuridica",
                table: "cestaBasica");

            migrationBuilder.DropColumn(
                name: "RecomendacaoTecnica",
                table: "cestaBasica");
        }
    }
}
