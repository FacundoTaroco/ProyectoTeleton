using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class DispositivoNotificacion : IValidar
    {
        public int Id { get; set; }
        public Paciente Paciente { get; set; }
        public int IdPaciente { get; set; }
        public string Endpoint { get; set; }
        public string P256dh { get; set; }
        public string Auth { get; set; }

        public void Validar()
        {
            //IMPLEMENTAR
        }
    }
}
