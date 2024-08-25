using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesWit
{
    public class UtteranceTrait
    {
        public string trait { get; set; }
        public string value { get; set; }

        public UtteranceTrait(string trait, string value)
        {
            this.trait = trait;
            this.value = value;
        }
        public UtteranceTrait() { }
    }

}
