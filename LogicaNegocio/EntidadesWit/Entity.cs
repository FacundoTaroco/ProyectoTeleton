using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesWit
{
    public class Entity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public string Body { get; set; }
        public string Value { get; set; }
        public double Confidence { get; set; }
        public Dictionary<string, object> Entities { get; set; }
        public string Type { get; set; }
        public FromTo From { get; set; }
        public FromTo To { get; set; }
        public List<FromTo> Values { get; set; }
    }
}
