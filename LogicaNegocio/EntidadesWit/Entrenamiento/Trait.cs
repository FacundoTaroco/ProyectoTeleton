using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesWit.Entrenamiento
{
    public class Trait
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<string> values { get; set; }

        public Trait(string id, string name, List<string> values)
        {
            this.id = id;
            this.name = name;
            this.values = values;
        }
        public Trait() { }
    }
}
