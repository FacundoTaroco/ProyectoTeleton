using LogicaNegocio.DTO;

namespace AppTeleton.Models
{
    public class CitasPorFechaViewModel
    {

        public DateOnly Fecha { get; set; }
        public List<CitaMedicaDTO> CitasDeFecha { get; set; } = new List<CitaMedicaDTO>();


        public CitasPorFechaViewModel() { }
    }
}
