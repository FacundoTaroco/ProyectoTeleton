using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Chat : IValidar
    {
        
        public int Id { get; set; }
        public Paciente _Paciente { get; set; }

        public bool AsistenciaAutomatica { get; set; }
        public bool Abierto { get; set; }

        public int IndiceReintento { get; set; } = 0;

        public Recepcionista? _Recepcionista { get; set; }

        public List<Mensaje> Mensajes { get; set; } = new List<Mensaje>();

        public DateTime FechaApertura { get; set; }



        public Chat() {
            Abierto = true;
        }

        public Chat(Paciente paciente)
        {
            IndiceReintento = 0;
            _Paciente = paciente;
            Abierto = true;
            AsistenciaAutomatica = true;
            Mensaje msgBienvenida = new Mensaje("¡Hola! Bienvenido, ¿En qué puedo asistirte?", "CHATBOT");
            Mensajes.Add(msgBienvenida);
            DateTime _fecha = DateTime.UtcNow;
            TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            FechaApertura = TimeZoneInfo.ConvertTimeFromUtc(_fecha, zonaHoraria);
        }
      

        public void AgregarMensajePaciente(Mensaje mensaje)
        {
            mensaje.EsDePaciente = true;
            Mensajes.Add(mensaje);   

        }
        public void AgregarMensajeBotRecepcion(Mensaje mensaje)
        {
            mensaje.EsDePaciente = false;
            Mensajes.Add(mensaje);
        }
        public void Validar()
        {
            
        }
    }
}
