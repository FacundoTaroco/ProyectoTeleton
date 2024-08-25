using LogicaNegocio.DTO;

namespace AppTeleton.Models
{
    public class MedicoViewModel
    {

        public IEnumerable<CitaMedicaDTO> Citas { get; set; }

        public MedicoViewModel() { 
        
        }
        public MedicoViewModel(IEnumerable<CitaMedicaDTO> citas)
        {
            Citas = citas;
        }
    }
}
