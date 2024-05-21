using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;

namespace AppTeleton.Models
{
    public class AccesoTotemViewModel
    {

        public IEnumerable<CitaMedicaDTO> Citas {  get; set; } = new List<CitaMedicaDTO>(); 
        public Paciente Paciente { get; set; }

        public AccesoTotemViewModel() { }   
        public AccesoTotemViewModel(IEnumerable<CitaMedicaDTO> citas, Paciente paciente)
        {
            Citas = citas;
            Paciente = paciente;
        }
        public AccesoTotemViewModel(Paciente paciente)
        {
            Paciente = paciente;
        }
    }
}
