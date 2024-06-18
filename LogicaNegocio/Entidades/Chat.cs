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

        public Recepcionista? _Recepcionista { get; set; }

        public List<Mensaje> Mensajes { get; set; } = new List<Mensaje>();



        public Chat() { 
        }

        public Chat(Paciente paciente)
        {
            _Paciente = paciente;
            Abierto = true;
            AsistenciaAutomatica = true;
        }
        public Chat(Paciente paciente, Recepcionista? recepcionista)
        {
            _Paciente = paciente;
            _Recepcionista = recepcionista;
            Abierto = true;
            AsistenciaAutomatica = true;
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
            //Implementar
        }
    }
}
