using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesWit
{
    public class ContextBody
    {
        public string body { get; set; }
        public decimal confidence { get; set; }
        public int end { get; set; }
        public List<Entitie> entities { get; set; }
        public string id { get; set; }

        public string name { get; set; }
        public string role { get; set; }
        public int start { get; set; }
        public string type { get; set; }
        public string value { get; set; }
        public ContextBody(string body, decimal confidence, int end, List<Entitie> entities, string id, string name, string role, int start, string type, string value)
        {
            this.body = body;
            this.confidence = confidence;
            this.end = end;
            this.entities = entities;
            this.id = id;
            this.name = name;
            this.role = role;
            this.start = start;
            this.type = type;
            this.value = value;
        }
        public ContextBody() { }
    }
}
