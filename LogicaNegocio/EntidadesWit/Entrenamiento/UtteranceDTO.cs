using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesWit.Entrenamiento
{
    public class UtteranceDTO
    {
        public string text { get; set; }
        public string intent { get; set; }
        public List<UtteranceEntity> entities { get; set; }
        public List<UtteranceTrait> traits { get; set; }

        public UtteranceDTO(string text, string intent, List<UtteranceEntity> entities, List<UtteranceTrait> traits)
        {
            this.text = text;
            this.intent = intent;
            this.entities = entities;
            this.traits = traits;

        }
        public UtteranceDTO() { }
    }
}
