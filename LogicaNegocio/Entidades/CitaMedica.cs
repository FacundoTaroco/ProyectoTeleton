using LogicaNegocio.Excepciones;
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
        public string Estado { get; set; }
        public string Detalles { get; set; }
        public CitaMedica() { }

        public CitaMedica(int id,int pkAgenda, string cedula, string nombreCompleto, string servicio, DateTime fecha, int horaInicio, string tratamiento, string estado, string detalles)
        {
            Id = id;
            PkAgenda = pkAgenda;
            Cedula = cedula;
            NombreCompleto = nombreCompleto;
            Servicio = servicio;
            Fecha = fecha;
            HoraInicio = horaInicio;
            Tratamiento = tratamiento;
            Estado = estado;
            Detalles = detalles;
        }


        public void Validar()
        {
            try
            {
                if (String.IsNullOrEmpty(Cedula) || String.IsNullOrEmpty(NombreCompleto)|| 
                    String.IsNullOrEmpty(Servicio) || Fecha == DateTime.MinValue || HoraInicio == 0
                    || String.IsNullOrEmpty(Detalles) || String.IsNullOrEmpty(Estado) || String.IsNullOrEmpty(Tratamiento))
                {
                    throw new PreguntaFrecException("Ingrese todos los campos");
                }
            }
            catch (PreguntaFrecException)
            {

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
