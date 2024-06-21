using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;

namespace AppTeleton.Models
{
    public class RecepsionistaViewModel
    {
        public IEnumerable<CitaMedicaDTO> CitasMedicas { get; set; }

        public RecepsionistaViewModel() { }
        public RecepsionistaViewModel(IEnumerable<CitaMedicaDTO> citasMedicas)
        {
            CitasMedicas = citasMedicas;
        }
    }
}
