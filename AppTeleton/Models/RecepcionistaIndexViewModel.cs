using LogicaNegocio.DTO;

namespace AppTeleton.Models
{
    public class RecepcionistaIndexViewModel
    {
        public IEnumerable<CitaMedicaDTO> citas { get; set; } = new List<CitaMedicaDTO>();  

        public RecepcionistaIndexViewModel(IEnumerable<CitaMedicaDTO> citas)
        {
            this.citas = citas;
        }
    }
}
