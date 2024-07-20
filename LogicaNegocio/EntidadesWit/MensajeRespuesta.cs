using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesWit
{
    public class MensajeRespuesta
    {
        public string Text { get; set; }
        public List<Intent> Intents { get; set; }
        public Dictionary<string, List<Entity>> Entities { get; set; }
    }
}
