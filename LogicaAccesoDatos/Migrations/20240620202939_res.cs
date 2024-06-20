using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    public partial class res : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Intent",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intent", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RespuestasEquivocadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Input = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntentAsignadoid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestasEquivocadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespuestasEquivocadas_Intent_IntentAsignadoid",
                        column: x => x.IntentAsignadoid,
                        principalTable: "Intent",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasEquivocadas_IntentAsignadoid",
                table: "RespuestasEquivocadas",
                column: "IntentAsignadoid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RespuestasEquivocadas");

            migrationBuilder.DropTable(
                name: "Intent");
        }
    }
}
