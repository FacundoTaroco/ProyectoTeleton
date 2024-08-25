using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class LLamado
    {


        public string NombrePaciente { get; set; }
        public string Cedula { get; set; }
        public string Consultorio { get; set; }

        public DateTime FechaHoraLLamada { get; set; }
        public LLamado() { }

        public LLamado(string nombrePaciente, string cedula, string consultorio)
        {
            DateTime _fecha = DateTime.UtcNow;
            TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            FechaHoraLLamada = TimeZoneInfo.ConvertTimeFromUtc(_fecha, zonaHoraria);

            NombrePaciente = nombrePaciente;
            Cedula = cedula;
            Consultorio = consultorio;
        }
    }
}
