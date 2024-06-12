﻿// <auto-generated />
using System;
using LogicaAccesoDatos.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    [DbContext(typeof(LibreriaContext))]
    partial class LibreriaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<int?>("PacienteId")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PacienteId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Notificaciones");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.PreguntaFrec", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Pregunta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Respuesta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PreguntasFrec");
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

                    b.HasDiscriminator().HasValue("Paciente");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Recepcionista", b =>
                {
                    b.HasBaseType("LogicaNegocio.Entidades.Usuario");

                    b.HasDiscriminator().HasValue("Recepcionista");
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

            modelBuilder.Entity("LogicaNegocio.Entidades.DispositivoNotificacion", b =>
                {
                    b.HasOne("LogicaNegocio.Entidades.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Notificacion", b =>
                {
                    b.HasOne("LogicaNegocio.Entidades.Paciente", null)
                        .WithMany("Notificaciones")
                        .HasForeignKey("PacienteId");

                    b.HasOne("LogicaNegocio.Entidades.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Paciente", b =>
                {
                    b.Navigation("Notificaciones");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Totem", b =>
                {
                    b.Navigation("Accesos");
                });
#pragma warning restore 612, 618
        }
    }
}
