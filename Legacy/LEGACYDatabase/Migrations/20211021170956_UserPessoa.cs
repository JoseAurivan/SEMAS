using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class UserPessoa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Matricula",
                table: "user",
                newName: "Nome");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "user",
                newName: "Matricula");
        }
    }
}
