using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.DTO
{
    public class AvisoMedicoDTO
    {
        public string Paciente { get; set; }
        public string Estado { get; set; }

        public DateTime Fecha { get; set; }


        public AvisoMedicoDTO() { } 

        public AvisoMedicoDTO(string paciente,string estado, DateTime fecha)
        {
            Paciente = paciente;
            Estado = estado;
            Fecha = fecha;
        }
    }
}
