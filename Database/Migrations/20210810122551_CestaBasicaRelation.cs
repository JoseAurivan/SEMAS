using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class CestaBasicaRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entrega_cestaBasica_CestaBasicaId",
                table: "Entrega");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entrega",
                table: "Entrega");

            migrationBuilder.RenameTable(
                name: "Entrega",
                newName: "entrega");

            migrationBuilder.RenameIndex(
                name: "IX_Entrega_CestaBasicaId",
                table: "entrega",
                newName: "IX_entrega_CestaBasicaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_entrega",
                table: "entrega",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_entrega_cestaBasica_CestaBasicaId",
                table: "entrega",
                column: "CestaBasicaId",
                principalTable: "cestaBasica",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_entrega_cestaBasica_CestaBasicaId",
                table: "entrega");

            migrationBuilder.DropPrimaryKey(
                name: "PK_entrega",
                table: "entrega");

            migrationBuilder.RenameTable(
                name: "entrega",
                newName: "Entrega");

            migrationBuilder.RenameIndex(
                name: "IX_entrega_CestaBasicaId",
                table: "Entrega",
                newName: "IX_Entrega_CestaBasicaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entrega",
                table: "Entrega",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Entrega_cestaBasica_CestaBasicaId",
                table: "Entrega",
                column: "CestaBasicaId",
                principalTable: "cestaBasica",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
