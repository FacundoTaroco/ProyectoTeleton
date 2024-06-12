using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;

namespace AppTeleton.Models
{
    public class PreguntasFrecViewModel
    {
        public IEnumerable<PreguntaFrec> PreguntasFrec { get; set; }

        public PreguntasFrecViewModel() { }
        public PreguntasFrecViewModel(IEnumerable<PreguntaFrec> preguntasFrec)
        {
            PreguntasFrec = preguntasFrec;
        }
    }
}