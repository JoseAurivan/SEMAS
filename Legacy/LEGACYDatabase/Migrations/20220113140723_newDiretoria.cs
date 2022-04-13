using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class newDiretoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificado_Curriculo_CurriculoId",
                table: "Certificado");

            migrationBuilder.DropForeignKey(
                name: "FK_Experiencias_Curriculo_CurriculoId",
                table: "Experiencias");

            migrationBuilder.DropForeignKey(
                name: "FK_pessoa_Curriculo_CurriculoId",
                table: "pessoa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Curriculo",
                table: "Curriculo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Certificado",
                table: "Certificado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Experiencias",
                table: "Experiencias");

            migrationBuilder.RenameTable(
                name: "Curriculo",
                newName: "curriculo");

            migrationBuilder.RenameTable(
                name: "Certificado",
                newName: "certificado");

            migrationBuilder.RenameTable(
                name: "Experiencias",
                newName: "experiencia");

            migrationBuilder.RenameIndex(
                name: "IX_Certificado_CurriculoId",
                table: "certificado",
                newName: "IX_certificado_CurriculoId");

            migrationBuilder.RenameIndex(
                name: "IX_Experiencias_CurriculoId",
                table: "experiencia",
                newName: "IX_experiencia_CurriculoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_curriculo",
                table: "curriculo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_certificado",
                table: "certificado",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_experiencia",
                table: "experiencia",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_certificado_curriculo_CurriculoId",
                table: "certificado",
                column: "CurriculoId",
                principalTable: "curriculo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_experiencia_curriculo_CurriculoId",
                table: "experiencia",
                column: "CurriculoId",
                principalTable: "curriculo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_pessoa_curriculo_CurriculoId",
                table: "pessoa",
                column: "CurriculoId",
                principalTable: "curriculo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificado_curriculo_CurriculoId",
                table: "certificado");

            migrationBuilder.DropForeignKey(
                name: "FK_experiencia_curriculo_CurriculoId",
                table: "experiencia");

            migrationBuilder.DropForeignKey(
                name: "FK_pessoa_curriculo_CurriculoId",
                table: "pessoa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_curriculo",
                table: "curriculo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_certificado",
                table: "certificado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_experiencia",
                table: "experiencia");

            migrationBuilder.RenameTable(
                name: "curriculo",
                newName: "Curriculo");

            migrationBuilder.RenameTable(
                name: "certificado",
                newName: "Certificado");

            migrationBuilder.RenameTable(
                name: "experiencia",
                newName: "Experiencias");

            migrationBuilder.RenameIndex(
                name: "IX_certificado_CurriculoId",
                table: "Certificado",
                newName: "IX_Certificado_CurriculoId");

            migrationBuilder.RenameIndex(
                name: "IX_experiencia_CurriculoId",
                table: "Experiencias",
                newName: "IX_Experiencias_CurriculoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Curriculo",
                table: "Curriculo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Certificado",
                table: "Certificado",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Experiencias",
                table: "Experiencias",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificado_Curriculo_CurriculoId",
                table: "Certificado",
                column: "CurriculoId",
                principalTable: "Curriculo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiencias_Curriculo_CurriculoId",
                table: "Experiencias",
                column: "CurriculoId",
                principalTable: "Curriculo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_pessoa_Curriculo_CurriculoId",
                table: "pessoa",
                column: "CurriculoId",
                principalTable: "Curriculo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
