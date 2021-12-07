using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class DiretoriaTrabalho : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurriculoId",
                table: "pessoa",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Curriculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Resumo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Competencias = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Certificado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CopiaPdf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurriculoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificado_Curriculo_CurriculoId",
                        column: x => x.CurriculoId,
                        principalTable: "Curriculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Experiencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Local = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurriculoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiencias_Curriculo_CurriculoId",
                        column: x => x.CurriculoId,
                        principalTable: "Curriculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pessoa_CurriculoId",
                table: "pessoa",
                column: "CurriculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificado_CurriculoId",
                table: "Certificado",
                column: "CurriculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiencias_CurriculoId",
                table: "Experiencias",
                column: "CurriculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_pessoa_Curriculo_CurriculoId",
                table: "pessoa",
                column: "CurriculoId",
                principalTable: "Curriculo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pessoa_Curriculo_CurriculoId",
                table: "pessoa");

            migrationBuilder.DropTable(
                name: "Certificado");

            migrationBuilder.DropTable(
                name: "Experiencias");

            migrationBuilder.DropTable(
                name: "Curriculo");

            migrationBuilder.DropIndex(
                name: "IX_pessoa_CurriculoId",
                table: "pessoa");

            migrationBuilder.DropColumn(
                name: "CurriculoId",
                table: "pessoa");
        }
    }
}
