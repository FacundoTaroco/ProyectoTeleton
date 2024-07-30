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
                name: "CategoriasPregunta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Respuesta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasPregunta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Encuestas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SatisfaccionGeneral = table.Column<int>(type: "int", nullable: false),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encuestas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParametrosRecordatorios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CadaCuantoEnviarRecordatorio = table.Column<int>(type: "int", nullable: false),
                    RecordatoriosEncendidos = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametrosRecordatorios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RespuestasEquivocadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Input = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntentAsignado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestasEquivocadas", x => x.Id);
                });

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
                    ParaEncuestar = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreguntasFrec",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pregunta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoriaPreguntaId = table.Column<int>(type: "int", nullable: false),
                    MostrarEnTotem = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreguntasFrec", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreguntasFrec_CategoriasPregunta_CategoriaPreguntaId",
                        column: x => x.CategoriaPreguntaId,
                        principalTable: "CategoriasPregunta",
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
                    _TotemId = table.Column<int>(type: "int", nullable: false),
                    IdTotem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccesosTotem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccesosTotem_Usuarios__TotemId",
                        column: x => x._TotemId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    _PacienteId = table.Column<int>(type: "int", nullable: false),
                    AsistenciaAutomatica = table.Column<bool>(type: "bit", nullable: false),
                    Abierto = table.Column<bool>(type: "bit", nullable: false),
                    IndiceReintento = table.Column<int>(type: "int", nullable: false),
                    _RecepcionistaId = table.Column<int>(type: "int", nullable: true),
                    FechaApertura = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_Usuarios__PacienteId",
                        column: x => x._PacienteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chats_Usuarios__RecepcionistaId",
                        column: x => x._RecepcionistaId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Dispositivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Endpoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    P256dh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Auth = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispositivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dispositivos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mensaje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EsDePaciente = table.Column<bool>(type: "bit", nullable: false),
                    contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _ChatId = table.Column<int>(type: "int", nullable: false),
                    IdChat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensaje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mensaje_Chats__ChatId",
                        column: x => x._ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CategoriasPregunta",
                columns: new[] { "Id", "Categoria", "Descripcion", "Respuesta" },
                values: new object[,]
                {
                    { 1, "prueba_ingreso", "Preguntas relacionadas al protocolo de ingreso", "Para la prueba de ingreso debes llevar la cédula." },
                    { 2, "acompaniante", "Preguntas relacionadas a como/quien debe acompañar al paciente", "El niño/adolescente que va a ser atendido, debe concurrir obligatoriamente con uno de sus tutores legales a cargo, o con la persona que ese tutor autorice en la entrevista de recepción que realizamos cuando ingresó al Centro. \r\nEn casos específicos de adolescentes podría evaluarse, en ese caso debería consultar con Coordinación de Agenda.\r\nEn caso de querer asistir con un acompañante mas, se permite (por ejemplo, hermanos).\r\nMientras el niño/adolescente se este atendiendo, el tutor debe permanecer en el centro, aunque no siempre ingrese a las terapias.  Las atenciones pueden ir desde 30, 45, 60, 90 o 120 minutos dependiendo de la actividad que tengas coordinada (para saber cuanto dura su tratamiento, escriba el nombre del mismo en el chat, por ejemplo, fisitría)." },
                    { 3, "comida", "Preguntas relacionadas a las diferentes opciones de alimentos a las que pueden acceder los pacientes y/o familias en el centro", "Disponemos de una cafetería, aquí podrá comprar comida, o traer la suya y comerla aquí. Tenemos microondas donde podrá calentarla. En caso de cualquier consulta, los voluntarios presentes en el centro, podrán ayudarle." },
                    { 4, "ubicacion", "Preguntas que solicitan la direccion de la Teletón", "El centro de la fundación Teletón ubicado en Montevideo, se encuentra en Carlos Brussa 2854, en el Barrio Prado. Y el centro Teletón de la ciudad de Fray Bentos, se encuentra en la dirección Zorrilla de San Martín 1484." },
                    { 5, "donacion", "Preguntas relacionadas al protocolo de donaciones", "En caso de donaciones, o devolver algún equipamiento, primero deberá comunicarse con el número de coordinación: 09*******. Si estas en el interior del país, puede enviarlo por las distintas agencias de transporte (DAC, Correo Uruguayo, etc), y por el tema del costo del envío, se charla con la coordinación y se evalúa. Y en caso de estar en Montevideo, y no tener medio de transporte, también se charla con coordinación." },
                    { 6, "materiales_generales", "Preguntas relacionadas a con que materiales basicos deben contar a la hora de presentarse a cualquier cita medica", "Los materiales que deben llevar el niño/adolescente varían según su tratamiento del día, para mas información escriba el nombre de su tratamiento y le enviaremos mas información." },
                    { 7, "alcancias", "Preguntas relacionadas a las alcancias", "Las alcancías se comienzan a entregar aproximadamente un mes antes del comienzo del Programa Teletón. Todos los usuarios tienen derecho a llevar 1 alcancía, presentando la cédula en el área de voluntariado. ubicada en el Centro Teletón. Si necesitas más de 1 alcancía, en el área de voluntariado le podrán dar más información para gestionarla." },
                    { 8, "historia_clinica", "Preguntas relacionadas a la historia clinica de los pacientes", "Enseguida le enviamos su historia clinica" },
                    { 9, "transporte", "Preguntas que solicitan direcciones para llegar al centro Teletón", "Enseguida le enviamos indicaciones" },
                    { 10, "cita", "Preguntas que solicitan informacion de sus citas", "Enseguida le enviaremos la información sobre sus cita" },
                    { 11, "solicitud_traslado", "Preguntas relacionadas al protocolo de de solicitudes de traslado", "Enseguida le enviaremos la información de la solicitud de traslado" },
                    { 12, "tratamiento_info", "Preguntas que solicitan informacion de los diferentes tratamientosii", "Enseguida le enviaremos la información de la solicitud del tratamiento" }
                });

            migrationBuilder.InsertData(
                table: "ParametrosRecordatorios",
                columns: new[] { "Id", "CadaCuantoEnviarRecordatorio", "RecordatoriosEncendidos" },
                values: new object[] { 1, 2, true });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Contrasenia", "Discriminator", "Nombre", "NombreUsuario" },
                values: new object[,]
                {
                    { 3, "Admin123", "Administrador", "Octavio", "Admin1" },
                    { 2, "medico123", "Medico", "Medico Montevideo", "medicoMVD" },
                    { 1, "totem123", "Totem", "Totem Montevideo", "totemMVD" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccesosTotem__TotemId",
                table: "AccesosTotem",
                column: "_TotemId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats__PacienteId",
                table: "Chats",
                column: "_PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats__RecepcionistaId",
                table: "Chats",
                column: "_RecepcionistaId");

            migrationBuilder.CreateIndex(
                name: "IX_Dispositivos_UsuarioId",
                table: "Dispositivos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensaje__ChatId",
                table: "Mensaje",
                column: "_ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_UsuarioId",
                table: "Notificaciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PreguntasFrec_CategoriaPreguntaId",
                table: "PreguntasFrec",
                column: "CategoriaPreguntaId");

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
                name: "Dispositivos");

            migrationBuilder.DropTable(
                name: "Encuestas");

            migrationBuilder.DropTable(
                name: "Mensaje");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "ParametrosRecordatorios");

            migrationBuilder.DropTable(
                name: "PreguntasFrec");

            migrationBuilder.DropTable(
                name: "RespuestasEquivocadas");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "CategoriasPregunta");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
