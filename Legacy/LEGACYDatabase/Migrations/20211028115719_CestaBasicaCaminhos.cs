using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class CestaBasicaCaminhos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Caminhos",
                table: "cestaBasica",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caminhos",
                table: "cestaBasica");
        }
    }
}
