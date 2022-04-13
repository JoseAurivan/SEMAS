using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class NovasCestas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_enderecos_cestaBasica_CestaId",
                table: "enderecos");

            migrationBuilder.DropIndex(
                name: "IX_enderecos_CestaId",
                table: "enderecos");

            migrationBuilder.DropColumn(
                name: "CestaId",
                table: "enderecos");

            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                table: "cestaBasica",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cestaBasica_EnderecoId",
                table: "cestaBasica",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_cestaBasica_enderecos_EnderecoId",
                table: "cestaBasica",
                column: "EnderecoId",
                principalTable: "enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cestaBasica_enderecos_EnderecoId",
                table: "cestaBasica");

            migrationBuilder.DropIndex(
                name: "IX_cestaBasica_EnderecoId",
                table: "cestaBasica");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "cestaBasica");

            migrationBuilder.AddColumn<int>(
                name: "CestaId",
                table: "enderecos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_enderecos_CestaId",
                table: "enderecos",
                column: "CestaId");

            migrationBuilder.AddForeignKey(
                name: "FK_enderecos_cestaBasica_CestaId",
                table: "enderecos",
                column: "CestaId",
                principalTable: "cestaBasica",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
