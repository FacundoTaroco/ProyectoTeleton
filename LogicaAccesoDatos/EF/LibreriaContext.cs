using LogicaAccesoDatos.EF.Config;
using LogicaNegocio.Entidades;
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


        //tabla para la gestion de roles de usuarios 
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Recepcionista> Recepcionistas { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Totem> Totems { get; set; }
        public DbSet<Medico> Medicos{ get; set; }


        //tabla para el manejo de los accesos al totem
        public DbSet<AccesoTotem> AccesosTotem { get; set; }

        //tablas para el manejo de las notificaciones
        public DbSet<DispositivoNotificacion> Dispositivos { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<ParametrosNotificaciones> ParametrosRecordatorios { get; set; }
        
  
        //tablas para el entrenamiento y manejo del chatbot
        
        public DbSet<Chat> Chats { get; set; }
        public DbSet<RespuestaEquivocada> RespuestasEquivocadas { get; set; }
        public DbSet<PreguntaFrec> PreguntasFrec { get; set; }
        public DbSet<CategoriaPregunta> CategoriasPregunta { get; set; }



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

            modelBuilder.Entity<CategoriaPregunta>().HasData(
            new CategoriaPregunta
            {
                Id = 1,
                Respuesta = "Para la prueba de ingreso debes llevar la cédula.",
                Categoria = "prueba_ingreso"
            }, 
            new CategoriaPregunta
            {
                Id = 2,
                Respuesta = "El niño/adolescente que va a ser atendido, debe concurrir obligatoriamente con uno de sus tutores legales a cargo, o con la persona que ese tutor autorice en la entrevista de recepción que realizamos cuando ingresó al Centro. \r\nEn casos específicos de adolescentes podría evaluarse, en ese caso debería consultar con Coordinación de Agenda.\r\nEn caso de querer asistir con un acompañante mas, se permite (por ejemplo, hermanos).\r\nMientras el niño/adolescente se este atendiendo, el tutor debe permanecer en el centro, aunque no siempre ingrese a las terapias.  Las atenciones pueden ir desde 30, 45, 60, 90 o 120 minutos dependiendo de la actividad que tengas coordinada (para saber cuanto dura su tratamiento, escriba el nombre del mismo en el chat, por ejemplo, fisitría).",
                Categoria = "acompaniante"
            },
            new CategoriaPregunta
            {
                Id = 3,
                Respuesta = "Disponemos de una cafetería, aquí podrá comprar comida, o traer la suya y comerla aquí. Tenemos microondas donde podrá calentarla. En caso de cualquier consulta, los voluntarios presentes en el centro, podrán ayudarle.",
                Categoria = "comida"
            },
            new CategoriaPregunta
            {
                Id = 4,
                Respuesta = "El centro de la fundación Teletón ubicado en Montevideo, se encuentra en Carlos Brussa 2854, en el Barrio Prado. Y el centro Teletón de la ciudad de Fray Bentos, se encuentra en la dirección Zorrilla de San Martín 1484.",
                Categoria = "ubicacion"
            },
            new CategoriaPregunta
            {
                Id = 5,
                Respuesta = "En caso de donaciones, o devolver algún equipamiento, primero deberá comunicarse con el número de coordinación: 09*******. Si estas en el interior del país, puede enviarlo por las distintas agencias de transporte (DAC, Correo Uruguayo, etc), y por el tema del costo del envío, se charla con la coordinación y se evalúa. Y en caso de estar en Montevideo, y no tener medio de transporte, también se charla con coordinación.",
                Categoria = "donacion"
            },
            new CategoriaPregunta
            {
                Id = 6,
                Respuesta = "Los materiales que deben llevar el niño/adolescente varían según su tratamiento del día, para mas información escriba el nombre de su tratamiento y le enviaremos mas información.",
                Categoria = "materiales_generales"
            },
            new CategoriaPregunta
            {
                Id = 7,
                Respuesta = "Las alcancías se comienzan a entregar aproximadamente un mes antes del comienzo del Programa Teletón. Todos los usuarios tienen derecho a llevar 1 alcancía, presentando la cédula en el área de voluntariado. ubicada en el Centro Teletón. Si necesitas más de 1 alcancía, en el área de voluntariado le podrán dar más información para gestionarla.",
                Categoria = "alcancias"
            },
            new CategoriaPregunta
            {
                Id = 8,
                Respuesta = "Enseguida le enviamos su historia clinica",
                Categoria = "historia_clinica"
            },
            new CategoriaPregunta
            {
                Id = 9,
                Respuesta = "Enseguida le enviamos indicaciones",
                Categoria = "transporte"
            },
            new CategoriaPregunta
            {
                Id = 10,
                Respuesta = "Enseguida le enviaremos la información sobre sus cita",
                Categoria = "cita"
            },
            new CategoriaPregunta
            {
                Id = 11,
                Respuesta = "Enseguida le enviaremos la información de la solicitud de traslado",
                Categoria = "solicitud_traslado"
            },
            new CategoriaPregunta
            {
                Id = 12,
                Respuesta = "Enseguida le enviaremos la información de la solicitud del tratamiento",
                Categoria = "tratamiento_info"
            }
            );

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




/* modelBuilder.Entity<PreguntaFrec>().HasData(
            new PreguntaFrec
            {
                Id = 1,
                Pregunta = "¿Cual es el protocolo de ingreso?",
                Respuesta = "Para la prueba de ingreso debes llevar la cédula.",
                Intent = "prueba_ingreso"
            },
            new PreguntaFrec
            {
                Id = 2,
                Pregunta = "¿Quienes deben acompañar al niño el día de la evaluación?",
                Respuesta = "El niño/adolescente que va a ser atendido, debe concurrir obligatoriamente con uno de sus tutores legales a cargo, o con la persona que ese tutor autorice en la entrevista de recepción que realizamos cuando ingresó al Centro. \r\nEn casos específicos de adolescentes podría evaluarse, en ese caso debería consultar con Coordinación de Agenda.\r\nEn caso de querer asistir con un acompañante mas, se permite (por ejemplo, hermanos).\r\nMientras el niño/adolescente se este atendiendo, el tutor debe permanecer en el centro, aunque no siempre ingrese a las terapias.  Las atenciones pueden ir desde 30, 45, 60, 90 o 120 minutos dependiendo de la actividad que tengas coordinada (para saber cuanto dura su tratamiento, escriba el nombre del mismo en el chat, por ejemplo, fisitría).",
                Intent = "acompaniante"
            },
            new PreguntaFrec
            {
                Id = 3,
                Pregunta = "¿Dónde puedo comprar comida?",
                Respuesta = "Disponemos de una cafetería, aquí podrá comprar comida, o traer la suya y comerla aquí. Tenemos microondas donde podrá calentarla. En caso de cualquier consulta, los voluntarios presentes en el centro, podrán ayudarle.",
                Intent = "comida"
            },
            new PreguntaFrec
            {
                Id = 4,
                Pregunta = "¿Dónde queda el Centro Teleton?",
                Respuesta = "El centro de la fundación Teletón ubicado en Montevideo, se encuentra en Carlos Brussa 2854, en el Barrio Prado. Y el centro Teletón de la ciudad de Fray Bentos, se encuentra en la dirección Zorrilla de San Martín 1484.",
                Intent = "ubicacion"
            },
            new PreguntaFrec
            {
                Id = 5,
                Pregunta = "¿Como puedo hacer llegar un equipamiento que no uso?",
                Respuesta = "En caso de donaciones, o devolver algún equipamiento, primero deberá comunicarse con el número de coordinación: 09*******. Si estas en el interior del país, puede enviarlo por las distintas agencias de transporte (DAC, Correo Uruguayo, etc), y por el tema del costo del envío, se charla con la coordinación y se evalúa. Y en caso de estar en Montevideo, y no tener medio de transporte, también se charla con coordinación.",
                Intent = "donacion"
            },
            new PreguntaFrec
            {
                Id = 6,
                Pregunta = "¿cuales son los materiales que debe llevar un paciente?",
                Respuesta = "Los materiales que deben llevar el niño/adolescente varían según su tratamiento del día, para mas información escriba el nombre de su tratamiento y le enviaremos mas información.",
                Intent = "materiales_generales"
            },
            new PreguntaFrec
            {
                Id = 7,
                Pregunta = "¿cual es el protocolo de las alcancias??",
                Respuesta = "Las alcancías se comienzan a entregar aproximadamente un mes antes del comienzo del Programa Teletón. Todos los usuarios tienen derecho a llevar 1 alcancía, presentando la cédula en el área de voluntariado. ubicada en el Centro Teletón. Si necesitas más de 1 alcancía, en el área de voluntariado le podrán dar más información para gestionarla.",
                Intent = "alcancias"
            },
            new PreguntaFrec
            {
                Id = 8,
                Pregunta = "¿Como puedo hacer llegar un equipamiento que no uso?",
                Respuesta = "En caso de donaciones, o devolver algún equipamiento, primero deberá comunicarse con el número de coordinación: 09*******. Si estas en el interior del país, puede enviarlo por las distintas agencias de transporte (DAC, Correo Uruguayo, etc), y por el tema del costo del envío, se charla con la coordinación y se evalúa. Y en caso de estar en Montevideo, y no tener medio de transporte, también se charla con coordinación.",
                Intent = "donacion"
            },
            new PreguntaFrec
            {
                Id = 9,
                Pregunta = "¿Puedo pedirles la historia clinica?",
                Respuesta = "Enseguida le enviamos su historia clinica",
                Intent = "historia_clinica"
            },
            new PreguntaFrec
            {
                Id = 10,
                Pregunta = "¿Como llego al centro Teleton?",
                Respuesta = "Enseguida le enviamos indicaciones",
                Intent = "transporte"
            },
             new PreguntaFrec
             {
                 Id = 11,
                 Pregunta = "¿Cual es mi proxima cita?",
                 Respuesta = "Enseguida le enviaremos la información sobre sus cita",
                 Intent = "transporte"
             },
             new PreguntaFrec
             {
                 Id = 12,
                 Pregunta = "¿Ustedes envían la solicitud de traslado?",
                 Respuesta = "Enseguida le enviaremos la información de la solicitud de traslado",
                 Intent = "solicitud_traslado"
             },
             new PreguntaFrec
             {
                 Id = 13,
                 Pregunta = "Informacion de tratamientos",
                 Respuesta = "Enseguida le enviaremos la información de la solicitud del tratamiento",
                 Intent = "tratamiento_info"
             }*/