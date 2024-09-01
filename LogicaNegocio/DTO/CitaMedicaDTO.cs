using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.DTO
{
    public class CitaMedicaDTO
    {
   
        public int PkAgenda { get; set; }
        public string Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string Servicio { get; set; }
        public DateTime Fecha { get; set; }
        public int HoraInicio { get; set; }
        public string Tratamiento { get; set; }
        public string Consultorio { get; set; }
        public string Estado { get; set; }
        public CitaMedicaDTO() { }

        public CitaMedicaDTO(int pkAgenda, string cedula, string nombreCompleto, string servicio, DateTime fecha, int horaInicio, string tratamiento, string consultorio,string estado)
        {
            PkAgenda = pkAgenda;
            Cedula = cedula;
            NombreCompleto = nombreCompleto;
            Servicio = servicio;
            TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            fecha = TimeZoneInfo.ConvertTimeFromUtc(fecha, zonaHoraria);
            Fecha = DateTime.ParseExact(fecha.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            HoraInicio = horaInicio;
            Tratamiento = tratamiento;
            Consultorio = consultorio;
            Estado = estado;
        }
    }
}
