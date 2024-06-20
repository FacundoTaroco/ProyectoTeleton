using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesWit
{
    public class Entitie
    {
        public string id { get; set; }
        public string? name { get; set; }
        public ArrayList roles { get; set; }
        public ArrayList lookups { get; set; }
        public List<string> keywords { get; set; }

        public Entitie(string id, string name, ArrayList roles, ArrayList lookups, List<String> keywords)
        {
            this.id = id;
            this.name = name;
            this.roles = roles;
            this.lookups = lookups;
            this.keywords = keywords;
        }
        public Entitie() { }
    }

}
