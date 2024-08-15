using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;

namespace AppTeleton.Models
{
    public class DatosEncuestasViewModel
    {

        public double PromedioSatisfaccionGeneral { get; set; }
        public double PromedioSatisfaccionRecepcion { get; set; }
        public double PromedioSatisfaccionAplicacion { get; set; }
        public double PromedioSatisfaccionEstadoCentro { get; set; }
        public IEnumerable<Encuesta> Encuestas { get; set; }
    }
}
