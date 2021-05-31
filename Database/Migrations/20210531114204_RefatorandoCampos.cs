using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class RefatorandoCampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PessoaEndereco_enderecos_EnderecoId",
                table: "PessoaEndereco");

            migrationBuilder.DropForeignKey(
                name: "FK_PessoaEndereco_pessoa_PessoaId",
                table: "PessoaEndereco");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PessoaEndereco",
                table: "PessoaEndereco");

            migrationBuilder.DropIndex(
                name: "IX_PessoaEndereco_PessoaId",
                table: "PessoaEndereco");

            migrationBuilder.DropColumn(
                name: "IdPessoa",
                table: "PessoaEndereco");

            migrationBuilder.DropColumn(
                name: "IdEndereco",
                table: "PessoaEndereco");

            migrationBuilder.AlterColumn<int>(
                name: "PessoaId",
                table: "PessoaEndereco",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EnderecoId",
                table: "PessoaEndereco",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PessoaEndereco",
                table: "PessoaEndereco",
                columns: new[] { "PessoaId", "EnderecoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaEndereco_enderecos_EnderecoId",
                table: "PessoaEndereco",
                column: "EnderecoId",
                principalTable: "enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaEndereco_pessoa_PessoaId",
                table: "PessoaEndereco",
                column: "PessoaId",
                principalTable: "pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PessoaEndereco_enderecos_EnderecoId",
                table: "PessoaEndereco");

            migrationBuilder.DropForeignKey(
                name: "FK_PessoaEndereco_pessoa_PessoaId",
                table: "PessoaEndereco");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PessoaEndereco",
                table: "PessoaEndereco");

            migrationBuilder.AlterColumn<int>(
                name: "EnderecoId",
                table: "PessoaEndereco",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PessoaId",
                table: "PessoaEndereco",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdPessoa",
                table: "PessoaEndereco",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdEndereco",
                table: "PessoaEndereco",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PessoaEndereco",
                table: "PessoaEndereco",
                columns: new[] { "IdPessoa", "IdEndereco" });

            migrationBuilder.CreateIndex(
                name: "IX_PessoaEndereco_PessoaId",
                table: "PessoaEndereco",
                column: "PessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaEndereco_enderecos_EnderecoId",
                table: "PessoaEndereco",
                column: "EnderecoId",
                principalTable: "enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaEndereco_pessoa_PessoaId",
                table: "PessoaEndereco",
                column: "PessoaId",
                principalTable: "pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
