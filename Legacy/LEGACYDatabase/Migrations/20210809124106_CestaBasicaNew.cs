using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class CestaBasicaNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrxEntrega",
                table: "cestaBasica");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "cestaBasica");

            migrationBuilder.CreateTable(
                name: "Entrega",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataEntrega = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntregaStatus = table.Column<bool>(type: "bit", nullable: false),
                    CestaBasicaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrega", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entrega_cestaBasica_CestaBasicaId",
                        column: x => x.CestaBasicaId,
                        principalTable: "cestaBasica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entrega_CestaBasicaId",
                table: "Entrega",
                column: "CestaBasicaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entrega");

            migrationBuilder.AddColumn<string>(
                name: "PrxEntrega",
                table: "cestaBasica",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "cestaBasica",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
