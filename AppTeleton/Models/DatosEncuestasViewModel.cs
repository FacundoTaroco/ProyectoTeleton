using LogicaNegocio.DTO;

namespace AppTeleton.Models
{
    public class DatosEncuestasViewModel
    {

        public double PromedioSatisfaccionGeneral { get; set; }
        public double PromedioSatisfaccionRecepcion { get; set; }
        public double PromedioSatisfaccionAplicacion { get; set; }
        public double PromedioSatisfaccionEstadoCentro { get; set; }
        public IEnumerable<ComentarioEncuestaDTO> ComentariosEncuestas { get; set; }
    }
}
