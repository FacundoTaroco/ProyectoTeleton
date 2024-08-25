using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesWit
{
    public class Intent
    {
        public string id { get; set; }
        public string? name { get; set; }

        public Intent(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
        public Intent() { }
    }
}
