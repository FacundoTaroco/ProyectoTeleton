using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    public partial class versiactualizo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotemId",
                table: "AccesosTotem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AccesosTotem_TotemId",
                table: "AccesosTotem",
                column: "TotemId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccesosTotem_Usuarios_TotemId",
                table: "AccesosTotem",
                column: "TotemId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccesosTotem_Usuarios_TotemId",
                table: "AccesosTotem");

            migrationBuilder.DropIndex(
                name: "IX_AccesosTotem_TotemId",
                table: "AccesosTotem");

            migrationBuilder.DropColumn(
                name: "TotemId",
                table: "AccesosTotem");
        }
    }
}
