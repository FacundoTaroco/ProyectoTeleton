using LogicaAccesoDatos.EF.Config;
using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class LibreriaContext : DbContext
    {

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Recepcionista> Recepcionistas { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Totem> Totems { get; set; }
        public DbSet<AccesoTotem> AccesosTotem { get; set; }
        public DbSet<Medico> Medicos{ get; set; }
        public DbSet<DispositivoNotificacion> Dispositivos { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var totemInstance = Totem.Instance;
            modelBuilder.Entity<Totem>().HasData(new Totem
            {
                Id = 1,
                Nombre = totemInstance.Nombre,
                NombreUsuario = totemInstance.NombreUsuario,
                Contrasenia = totemInstance.Contrasenia
            });
            var medicoInstance = Medico.Instance;
            modelBuilder.Entity<Medico>().HasData(new Medico
            {
                Id = 2,
                Nombre = medicoInstance.Nombre,
                NombreUsuario = medicoInstance.NombreUsuario,
                Contrasenia = medicoInstance.Contrasenia
            });
            modelBuilder.Entity<Administrador>().HasData(new Administrador
            {
                Id = 3,
                Nombre = "Octavio",
                NombreUsuario = "Admin1",
                Contrasenia = "Admin123"
            });




            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());//aca valido que no se repitan nombres de usuario
            base.OnModelCreating(modelBuilder);
           
         
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
