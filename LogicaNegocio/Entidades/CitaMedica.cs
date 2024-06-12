using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class CitaMedica
    {
        public int Id { get; set; }
        public int PkAgenda { get; set; }
        public string Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string Servicio { get; set; }
        public DateTime Fecha { get; set; }
        public int HoraInicio { get; set; }
        public string Tratamiento { get; set; }
        public CitaMedica() { }

        public CitaMedica(int pkAgenda, string cedula, string nombreCompleto, string servicio, DateTime fecha, int horaInicio, string tratamiento)
        {
            PkAgenda = pkAgenda;
            Cedula = cedula;
            NombreCompleto = nombreCompleto;
            Servicio = servicio;
            Fecha = fecha;
            HoraInicio = horaInicio;
            Tratamiento = tratamiento;
        }

    }
}
