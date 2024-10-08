﻿// <auto-generated />
using System;
using LogicaAccesoDatos.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    [DbContext(typeof(LibreriaContext))]
    [Migration("20240830012313_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LogicaNegocio.Entidades.AccesoTotem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CedulaPaciente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdTotem")
                        .HasColumnType("int");

                    b.Property<int>("_TotemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("_TotemId");

                    b.ToTable("AccesosTotem");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.CategoriaPregunta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Respuesta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategoriasPregunta");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Categoria = "prueba_ingreso",
                            Descripcion = "Preguntas relacionadas al protocolo de ingreso",
                            Respuesta = "Para la prueba de ingreso debes llevar la cédula."
                        },
                        new
                        {
                            Id = 2,
                            Categoria = "acompaniante",
                            Descripcion = "Preguntas relacionadas a como/quien debe acompañar al paciente",
                            Respuesta = "El niño/adolescente que va a ser atendido, debe concurrir obligatoriamente con uno de sus tutores legales a cargo, o con la persona que ese tutor autorice en la entrevista de recepción que realizamos cuando ingresó al Centro. \r\nEn casos específicos de adolescentes podría evaluarse, en ese caso debería consultar con Coordinación de Agenda.\r\nEn caso de querer asistir con un acompañante mas, se permite (por ejemplo, hermanos).\r\nMientras el niño/adolescente se este atendiendo, el tutor debe permanecer en el centro, aunque no siempre ingrese a las terapias.  Las atenciones pueden ir desde 30, 45, 60, 90 o 120 minutos dependiendo de la actividad que tengas coordinada (para saber cuanto dura su tratamiento, escriba el nombre del mismo en el chat, por ejemplo, fisitría)."
                        },
                        new
                        {
                            Id = 3,
                            Categoria = "comida",
                            Descripcion = "Preguntas relacionadas a las diferentes opciones de alimentos a las que pueden acceder los pacientes y/o familias en el centro",
                            Respuesta = "Disponemos de una cafetería, aquí podrá comprar comida, o traer la suya y comerla aquí. Tenemos microondas donde podrá calentarla. En caso de cualquier consulta, los voluntarios presentes en el centro, podrán ayudarle."
                        },
                        new
                        {
                            Id = 4,
                            Categoria = "ubicacion",
                            Descripcion = "Preguntas que solicitan la dirección de la Teletón",
                            Respuesta = "El centro de la fundación Teletón ubicado en Montevideo, se encuentra en Carlos Brussa 2854, en el Barrio Prado. Y el centro Teletón de la ciudad de Fray Bentos, se encuentra en la dirección Zorrilla de San Martín 1484."
                        },
                        new
                        {
                            Id = 5,
                            Categoria = "donacion",
                            Descripcion = "Preguntas relacionadas al protocolo de donaciones",
                            Respuesta = "En caso de donaciones, o devolver algún equipamiento, primero deberá comunicarse con el número de coordinación: 09*******. Si estas en el interior del país, puede enviarlo por las distintas agencias de transporte (DAC, Correo Uruguayo, etc), y por el tema del costo del envío, se charla con la coordinación y se evalúa. Y en caso de estar en Montevideo, y no tener medio de transporte, también se charla con coordinación."
                        },
                        new
                        {
                            Id = 6,
                            Categoria = "materiales_generales",
                            Descripcion = "Preguntas relacionadas a con que materiales básicos deben contar a la hora de presentarse a cualquier cita medica",
                            Respuesta = "Los materiales que deben llevar el niño/adolescente varían según su tratamiento del día, para mas información escriba el nombre de su tratamiento y le enviaremos mas información."
                        },
                        new
                        {
                            Id = 7,
                            Categoria = "alcancias",
                            Descripcion = "Preguntas relacionadas a las alcancías",
                            Respuesta = "Las alcancías se comienzan a entregar aproximadamente un mes antes del comienzo del Programa Teletón. Todos los usuarios tienen derecho a llevar 1 alcancía, presentando la cédula en el área de voluntariado. ubicada en el Centro Teletón. Si necesitas más de 1 alcancía, en el área de voluntariado le podrán dar más información para gestionarla."
                        },
                        new
                        {
                            Id = 9,
                            Categoria = "transporte",
                            Descripcion = "Preguntas que solicitan direcciones para llegar al centro Teletón",
                            Respuesta = "Enseguida le enviamos indicaciones"
                        },
                        new
                        {
                            Id = 10,
                            Categoria = "cita",
                            Descripcion = "Preguntas que solicitan informacion de sus citas",
                            Respuesta = "Enseguida le enviaremos la información sobre sus cita"
                        },
                        new
                        {
                            Id = 11,
                            Categoria = "solicitud_traslado",
                            Descripcion = "Preguntas relacionadas al protocolo de de solicitudes de traslado",
                            Respuesta = "Enseguida le enviaremos la información de la solicitud de traslado"
                        },
                        new
                        {
                            Id = 12,
                            Categoria = "tratamiento_info",
                            Descripcion = "Preguntas que solicitan informacion de los diferentes tratamientos",
                            Respuesta = "Enseguida le enviaremos la información de la solicitud del tratamiento"
                        });
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Abierto")
                        .HasColumnType("bit");

                    b.Property<bool>("AsistenciaAutomatica")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaApertura")
                        .HasColumnType("datetime2");

                    b.Property<int>("IndiceReintento")
                        .HasColumnType("int");

                    b.Property<int>("_PacienteId")
                        .HasColumnType("int");

                    b.Property<int?>("_RecepcionistaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("_PacienteId");

                    b.HasIndex("_RecepcionistaId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.DispositivoNotificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Auth")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Endpoint")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("P256dh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Dispositivos");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Encuesta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Comentarios")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("SatisfaccionAplicacion")
                        .HasColumnType("int");

                    b.Property<int>("SatisfaccionEstadoDelCentro")
                        .HasColumnType("int");

                    b.Property<int>("SatisfaccionGeneral")
                        .HasColumnType("int");

                    b.Property<int>("SatisfaccionRecepcion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Encuestas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Comentarios = "Excelente atención, pero la aplicación podría ser más intuitiva.",
                            Fecha = new DateTime(2024, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SatisfaccionAplicacion = 4,
                            SatisfaccionEstadoDelCentro = 5,
                            SatisfaccionGeneral = 5,
                            SatisfaccionRecepcion = 4
                        },
                        new
                        {
                            Id = 2,
                            Comentarios = "Buen servicio, pero la recepción fue un poco lenta.",
                            Fecha = new DateTime(2024, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SatisfaccionAplicacion = 5,
                            SatisfaccionEstadoDelCentro = 4,
                            SatisfaccionGeneral = 3,
                            SatisfaccionRecepcion = 3
                        },
                        new
                        {
                            Id = 3,
                            Comentarios = "Todo bien, la atención fue muy rápida.",
                            Fecha = new DateTime(2024, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SatisfaccionAplicacion = 4,
                            SatisfaccionEstadoDelCentro = 4,
                            SatisfaccionGeneral = 4,
                            SatisfaccionRecepcion = 5
                        },
                        new
                        {
                            Id = 4,
                            Comentarios = "Podría mejorar la señalización dentro del centro.",
                            Fecha = new DateTime(2024, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SatisfaccionAplicacion = 4,
                            SatisfaccionEstadoDelCentro = 3,
                            SatisfaccionGeneral = 2,
                            SatisfaccionRecepcion = 2
                        },
                        new
                        {
                            Id = 5,
                            Comentarios = "Sin comentarios",
                            Fecha = new DateTime(2024, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SatisfaccionAplicacion = 5,
                            SatisfaccionEstadoDelCentro = 5,
                            SatisfaccionGeneral = 5,
                            SatisfaccionRecepcion = 5
                        },
                        new
                        {
                            Id = 6,
                            Comentarios = "Muy satisfecho con la aplicación y el servicio.",
                            Fecha = new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SatisfaccionAplicacion = 5,
                            SatisfaccionEstadoDelCentro = 4,
                            SatisfaccionGeneral = 4,
                            SatisfaccionRecepcion = 4
                        },
                        new
                        {
                            Id = 7,
                            Comentarios = "Sin comentarios",
                            Fecha = new DateTime(2024, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SatisfaccionAplicacion = 4,
                            SatisfaccionEstadoDelCentro = 3,
                            SatisfaccionGeneral = 3,
                            SatisfaccionRecepcion = 3
                        },
                        new
                        {
                            Id = 8,
                            Comentarios = "El personal fue muy amable y atento.",
                            Fecha = new DateTime(2024, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SatisfaccionAplicacion = 5,
                            SatisfaccionEstadoDelCentro = 5,
                            SatisfaccionGeneral = 4,
                            SatisfaccionRecepcion = 4
                        },
                        new
                        {
                            Id = 9,
                            Comentarios = "Sin comentarios",
                            Fecha = new DateTime(2024, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SatisfaccionAplicacion = 4,
                            SatisfaccionEstadoDelCentro = 5,
                            SatisfaccionGeneral = 5,
                            SatisfaccionRecepcion = 5
                        },
                        new
                        {
                            Id = 10,
                            Comentarios = "Sin comentarios",
                            Fecha = new DateTime(2024, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SatisfaccionAplicacion = 5,
                            SatisfaccionEstadoDelCentro = 4,
                            SatisfaccionGeneral = 4,
                            SatisfaccionRecepcion = 3
                        },
                        new
                        {
                            Id = 11,
                            Comentarios = "Todo estuvo perfecto, ¡gracias!",
                            Fecha = new DateTime(2024, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SatisfaccionAplicacion = 5,
                            SatisfaccionEstadoDelCentro = 5,
                            SatisfaccionGeneral = 5,
                            SatisfaccionRecepcion = 5
                        },
                        new
                        {
                            Id = 12,
                            Comentarios = "La aplicación funcionó bien, pero el estado del centro podría mejorar.",
                            Fecha = new DateTime(2024, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SatisfaccionAplicacion = 4,
                            SatisfaccionEstadoDelCentro = 3,
                            SatisfaccionGeneral = 3,
                            SatisfaccionRecepcion = 4
                        },
                        new
                        {
                            Id = 13,
                            Comentarios = "Sin comentarios",
                            Fecha = new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SatisfaccionAplicacion = 4,
                            SatisfaccionEstadoDelCentro = 4,
                            SatisfaccionGeneral = 4,
                            SatisfaccionRecepcion = 4
                        },
                        new
                        {
                            Id = 14,
                            Comentarios = "Una experiencia excelente en todos los aspectos.",
                            Fecha = new DateTime(2024, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SatisfaccionAplicacion = 5,
                            SatisfaccionEstadoDelCentro = 5,
                            SatisfaccionGeneral = 5,
                            SatisfaccionRecepcion = 5
                        },
                        new
                        {
                            Id = 15,
                            Comentarios = "Mejoraría la señalización del centro.",
                            Fecha = new DateTime(2024, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SatisfaccionAplicacion = 5,
                            SatisfaccionEstadoDelCentro = 4,
                            SatisfaccionGeneral = 4,
                            SatisfaccionRecepcion = 4
                        },
                        new
                        {
                            Id = 16,
                            Comentarios = "La aplicación fue útil, pero el tiempo de espera en recepción fue largo.",
                            Fecha = new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SatisfaccionAplicacion = 4,
                            SatisfaccionEstadoDelCentro = 4,
                            SatisfaccionGeneral = 3,
                            SatisfaccionRecepcion = 3
                        },
                        new
                        {
                            Id = 17,
                            Comentarios = "Una experiencia muy positiva, ¡gracias al equipo!",
                            Fecha = new DateTime(2024, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SatisfaccionAplicacion = 5,
                            SatisfaccionEstadoDelCentro = 5,
                            SatisfaccionGeneral = 5,
                            SatisfaccionRecepcion = 5
                        },
                        new
                        {
                            Id = 18,
                            Comentarios = "La atención fue buena, aunque el centro estaba un poco desordenado.",
                            Fecha = new DateTime(2024, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SatisfaccionAplicacion = 4,
                            SatisfaccionEstadoDelCentro = 3,
                            SatisfaccionGeneral = 4,
                            SatisfaccionRecepcion = 4
                        },
                        new
                        {
                            Id = 19,
                            Comentarios = "La aplicación funcionó perfectamente, pero la atención podría mejorar.",
                            Fecha = new DateTime(2024, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SatisfaccionAplicacion = 5,
                            SatisfaccionEstadoDelCentro = 4,
                            SatisfaccionGeneral = 3,
                            SatisfaccionRecepcion = 4
                        },
                        new
                        {
                            Id = 20,
                            Comentarios = "Todo fue excelente, la atención fue rápida y eficiente.",
                            Fecha = new DateTime(2024, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SatisfaccionAplicacion = 5,
                            SatisfaccionEstadoDelCentro = 5,
                            SatisfaccionGeneral = 5,
                            SatisfaccionRecepcion = 5
                        });
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Mensaje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("EsDePaciente")
                        .HasColumnType("bit");

                    b.Property<int>("IdChat")
                        .HasColumnType("int");

                    b.Property<int>("_ChatId")
                        .HasColumnType("int");

                    b.Property<string>("contenido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("nombreUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("_ChatId");

                    b.ToTable("Mensaje");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Notificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("Mensaje")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<DateTime>("fecha")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Notificaciones");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.ParametrosNotificaciones", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CadaCuantoEnviarRecordatorio")
                        .HasColumnType("int");

                    b.Property<bool>("RecordatoriosEncendidos")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("ParametrosRecordatorios");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CadaCuantoEnviarRecordatorio = 2,
                            RecordatoriosEncendidos = true
                        });
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.PreguntaFrec", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoriaPreguntaId")
                        .HasColumnType("int");

                    b.Property<bool>("MostrarEnTotem")
                        .HasColumnType("bit");

                    b.Property<string>("Pregunta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaPreguntaId");

                    b.ToTable("PreguntasFrec");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoriaPreguntaId = 2,
                            MostrarEnTotem = true,
                            Pregunta = "¿Quienes deben acompañar al niño el día de la evaluación?"
                        },
                        new
                        {
                            Id = 2,
                            CategoriaPreguntaId = 1,
                            MostrarEnTotem = true,
                            Pregunta = "¿Que es la evaluación de ingreso?"
                        },
                        new
                        {
                            Id = 3,
                            CategoriaPreguntaId = 3,
                            MostrarEnTotem = true,
                            Pregunta = "¿Es posible comer en el centro?"
                        },
                        new
                        {
                            Id = 4,
                            CategoriaPreguntaId = 4,
                            MostrarEnTotem = true,
                            Pregunta = "¿Dónde se ubica el centro Teletón?"
                        },
                        new
                        {
                            Id = 5,
                            CategoriaPreguntaId = 5,
                            MostrarEnTotem = true,
                            Pregunta = "¿Cómo hago para donar?"
                        },
                        new
                        {
                            Id = 6,
                            CategoriaPreguntaId = 6,
                            MostrarEnTotem = true,
                            Pregunta = "¿Qué cosas necesita llevar el responsable del niño a la cita?"
                        },
                        new
                        {
                            Id = 7,
                            CategoriaPreguntaId = 7,
                            MostrarEnTotem = true,
                            Pregunta = "¿Cuál es el protocolo para las alcancías?"
                        });
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.RespuestaEquivocada", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Input")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IntentAsignado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RespuestasEquivocadas");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Contrasenia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("NombreUsuario")
                        .IsUnique();

                    b.ToTable("Usuarios");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Usuario");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Administrador", b =>
                {
                    b.HasBaseType("LogicaNegocio.Entidades.Usuario");

                    b.HasDiscriminator().HasValue("Administrador");

                    b.HasData(
                        new
                        {
                            Id = 3,
                            Contrasenia = "Admin123",
                            Nombre = "Octavio",
                            NombreUsuario = "Admin1"
                        });
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Medico", b =>
                {
                    b.HasBaseType("LogicaNegocio.Entidades.Usuario");

                    b.HasDiscriminator().HasValue("Medico");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Contrasenia = "medico123",
                            Nombre = "Medico Montevideo",
                            NombreUsuario = "medicoMVD"
                        });
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Paciente", b =>
                {
                    b.HasBaseType("LogicaNegocio.Entidades.Usuario");

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ParaEncuestar")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("Paciente");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Recepcionista", b =>
                {
                    b.HasBaseType("LogicaNegocio.Entidades.Usuario");

                    b.HasDiscriminator().HasValue("Recepcionista");

                    b.HasData(
                        new
                        {
                            Id = 4,
                            Contrasenia = "Maria123",
                            Nombre = "Maria",
                            NombreUsuario = "Maria"
                        },
                        new
                        {
                            Id = 5,
                            Contrasenia = "Laura123",
                            Nombre = "Laura",
                            NombreUsuario = "Laura"
                        });
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Totem", b =>
                {
                    b.HasBaseType("LogicaNegocio.Entidades.Usuario");

                    b.HasDiscriminator().HasValue("Totem");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Contrasenia = "totem123",
                            Nombre = "Totem Montevideo",
                            NombreUsuario = "totemMVD"
                        });
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.AccesoTotem", b =>
                {
                    b.HasOne("LogicaNegocio.Entidades.Totem", "_Totem")
                        .WithMany("Accesos")
                        .HasForeignKey("_TotemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("_Totem");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Chat", b =>
                {
                    b.HasOne("LogicaNegocio.Entidades.Paciente", "_Paciente")
                        .WithMany()
                        .HasForeignKey("_PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Entidades.Recepcionista", "_Recepcionista")
                        .WithMany()
                        .HasForeignKey("_RecepcionistaId");

                    b.Navigation("_Paciente");

                    b.Navigation("_Recepcionista");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.DispositivoNotificacion", b =>
                {
                    b.HasOne("LogicaNegocio.Entidades.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Mensaje", b =>
                {
                    b.HasOne("LogicaNegocio.Entidades.Chat", "_Chat")
                        .WithMany("Mensajes")
                        .HasForeignKey("_ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("_Chat");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Notificacion", b =>
                {
                    b.HasOne("LogicaNegocio.Entidades.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.PreguntaFrec", b =>
                {
                    b.HasOne("LogicaNegocio.Entidades.CategoriaPregunta", "CategoriaPregunta")
                        .WithMany()
                        .HasForeignKey("CategoriaPreguntaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoriaPregunta");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Chat", b =>
                {
                    b.Navigation("Mensajes");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Totem", b =>
                {
                    b.Navigation("Accesos");
                });
#pragma warning restore 612, 618
        }
    }
}
