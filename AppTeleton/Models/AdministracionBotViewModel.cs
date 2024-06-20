using LogicaNegocio.EntidadesWit.Entrenamiento;

namespace AppTeleton.Models
{
    public class AdministracionBotViewModel
    {

        public IEnumerable<Intent> intents { get; set; } = new List<Intent>();  
        public IEnumerable<RespuestaEquivocada> respuestas { get; set; } = new List<RespuestaEquivocada>();


    }
}
