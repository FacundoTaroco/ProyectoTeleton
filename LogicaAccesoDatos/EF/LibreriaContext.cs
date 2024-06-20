using LogicaAccesoDatos.EF.Config;
using LogicaNegocio.Entidades;
using LogicaNegocio.EntidadesWit.Entrenamiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class LibreriaContext : DbContext
    {
        private IConfiguration _config; 
        public LibreriaContext(IConfiguration config) { 
            _config = config;
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Recepcionista> Recepcionistas { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Totem> Totems { get; set; }
        public DbSet<AccesoTotem> AccesosTotem { get; set; }
        public DbSet<Medico> Medicos{ get; set; }
        public DbSet<DispositivoNotificacion> Dispositivos { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<PreguntaFrec> PreguntasFrec { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ParametrosNotificaciones> ParametrosRecordatorios { get; set; }

        public DbSet<RespuestaEquivocada> RespuestasEquivocadas { get; set; }



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

            var parametrosInstance = ParametrosNotificaciones.GetInstancia();
            modelBuilder.Entity<ParametrosNotificaciones>().HasData(new ParametrosNotificaciones
            {
                Id = 1,
                RecordatoriosEncendidos = parametrosInstance.RecordatoriosEncendidos,
                CadaCuantoEnviarRecordatorio = parametrosInstance.CadaCuantoEnviarRecordatorio
            });



            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());//aca valido que no se repitan nombres de usuario
            base.OnModelCreating(modelBuilder);
           
         
        }
        //a
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
            _config["ConnectionStrings:LocalBD"]
            );
        }


    }
}
