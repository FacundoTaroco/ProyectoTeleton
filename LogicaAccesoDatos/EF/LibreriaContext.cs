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
        public DbSet<Paciente> Pacientes { get; set; } 
        public DbSet<Recepcionista> Recepcionistas { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Totem> Totems { get; set; }
        public DbSet<AccesoTotem> AccesosTotem { get; set; }
        public DbSet<SesionTotem> SesionesTotem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AccesoTotem>()
                .HasOne(a => a.Totem)
                .WithMany()
                .HasForeignKey(a => a.TotemId);

            // Creación de datos iniciales utilizando constructor para EF
            var totemInstance = Totem.Instance;
            modelBuilder.Entity<Totem>().HasData(new Totem
            {
                Id = 1,
                Nombre = totemInstance.Nombre,
                NombreUsuario = totemInstance.NombreUsuario,
                Contrasenia = totemInstance.Contrasenia
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
            @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog=libreriaProyecto; Integrated Security=True;"
            );
        }





       /* protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CabaniaConfiguration());


           

            base.OnModelCreating(modelBuilder);
        }*/


    }
}
