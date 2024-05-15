using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    public partial class AddTotemEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AccesosTotem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaAcceso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccesosTotem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccesosTotem_Usuarios_TotemId",
                        column: x => x.TotemId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SesionesTotem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InicioSesion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinSesion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionesTotem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SesionesTotem_Usuarios_TotemId",
                        column: x => x.TotemId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Contrasenia", "Discriminator", "Nombre", "NombreUsuario", "Rol" },
                values: new object[] { 1, "totem123", "Totem", "Totem Principal", "totem", "Totem" });

            migrationBuilder.CreateIndex(
                name: "IX_AccesosTotem_TotemId",
                table: "AccesosTotem",
                column: "TotemId");

            migrationBuilder.CreateIndex(
                name: "IX_SesionesTotem_TotemId",
                table: "SesionesTotem",
                column: "TotemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccesosTotem");

            migrationBuilder.DropTable(
                name: "SesionesTotem");

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Rol",
                table: "Usuarios");
        }
    }
}
