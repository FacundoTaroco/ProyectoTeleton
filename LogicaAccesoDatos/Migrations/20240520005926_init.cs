using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Contrasenia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contacto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SesionesTotem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InicioSesion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SesionAbierta = table.Column<bool>(type: "bit", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "AccesosTotem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CedulaPaciente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Accion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotemId = table.Column<int>(type: "int", nullable: false),
                    SesionTotemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccesosTotem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccesosTotem_SesionesTotem_SesionTotemId",
                        column: x => x.SesionTotemId,
                        principalTable: "SesionesTotem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccesosTotem_Usuarios_TotemId",
                        column: x => x.TotemId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Contrasenia", "Discriminator", "Nombre", "NombreUsuario" },
                values: new object[] { 1, "totem123", "Totem", "Totem Montevideo", "totem" });

            migrationBuilder.CreateIndex(
                name: "IX_AccesosTotem_SesionTotemId",
                table: "AccesosTotem",
                column: "SesionTotemId");

            migrationBuilder.CreateIndex(
                name: "IX_AccesosTotem_TotemId",
                table: "AccesosTotem",
                column: "TotemId");

            migrationBuilder.CreateIndex(
                name: "IX_SesionesTotem_TotemId",
                table: "SesionesTotem",
                column: "TotemId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_NombreUsuario",
                table: "Usuarios",
                column: "NombreUsuario",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccesosTotem");

            migrationBuilder.DropTable(
                name: "SesionesTotem");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
