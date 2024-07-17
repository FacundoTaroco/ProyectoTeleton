using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;

namespace AppTeleton.Models
{
    public class CitasViewModel
    {
        public IEnumerable<CitaMedicaDTO> citas { get; set; } = new List<CitaMedicaDTO>();
        public Notificacion? Notificacion { get; set; } = null;
        public CitasViewModel() { }
        public CitasViewModel(IEnumerable<CitaMedicaDTO> citas)
        {
            this.citas = citas;
        }
        public CitasViewModel(IEnumerable<CitaMedicaDTO> citas, Notificacion notificacion)
        {
            this.citas = citas;
            this.Notificacion = notificacion;
        }
    }
}
