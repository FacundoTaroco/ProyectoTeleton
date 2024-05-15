using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class LibreriaContext:DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Totem> Totems { get; set; }
        public DbSet<AccesoTotem> AccesosTotem { get; set; }
        public DbSet<SesionTotem> SesionesTotem { get; set; }

        public LibreriaContext(DbContextOptions<LibreriaContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de relaciones
            modelBuilder.Entity<AccesoTotem>()
                .HasOne(a => a.Totem)
                .WithMany()
                .HasForeignKey(a => a.TotemId);

            modelBuilder.Entity<SesionTotem>()
                .HasOne(s => s.Totem)
                .WithMany()
                .HasForeignKey(s => s.TotemId);

            // Creación de datos iniciales utilizando método adicional
            ConfigureTotemData(modelBuilder);
        }

        private void ConfigureTotemData(ModelBuilder modelBuilder)
        {
            // Instanciación del objeto singleton
            var totemInstance = Totem.Instance;

            modelBuilder.Entity<Totem>().HasData(new Totem
            {
                Id = 1,
                Nombre = totemInstance.Nombre,
                NombreUsuario = totemInstance.NombreUsuario,
                Contrasenia = totemInstance.Contrasenia,
                Rol = totemInstance.Rol
            });
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
            @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog=libreriaProyecto; Integrated Security=True;"
            );
        }

    }
}
