using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class PessoaRelationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cadastroCMAS_pessoa_PessoaId",
                table: "cadastroCMAS");

            migrationBuilder.DropIndex(
                name: "IX_cadastroCMAS_PessoaId",
                table: "cadastroCMAS");

            migrationBuilder.DropColumn(
                name: "PessoaId",
                table: "cadastroCMAS");

            migrationBuilder.AddColumn<int>(
                name: "CadastroCmasId",
                table: "pessoa",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_pessoa_CadastroCmasId",
                table: "pessoa",
                column: "CadastroCmasId");

            migrationBuilder.AddForeignKey(
                name: "FK_pessoa_cadastroCMAS_CadastroCmasId",
                table: "pessoa",
                column: "CadastroCmasId",
                principalTable: "cadastroCMAS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pessoa_cadastroCMAS_CadastroCmasId",
                table: "pessoa");

            migrationBuilder.DropIndex(
                name: "IX_pessoa_CadastroCmasId",
                table: "pessoa");

            migrationBuilder.DropColumn(
                name: "CadastroCmasId",
                table: "pessoa");

            migrationBuilder.AddColumn<int>(
                name: "PessoaId",
                table: "cadastroCMAS",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cadastroCMAS_PessoaId",
                table: "cadastroCMAS",
                column: "PessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_cadastroCMAS_pessoa_PessoaId",
                table: "cadastroCMAS",
                column: "PessoaId",
                principalTable: "pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
