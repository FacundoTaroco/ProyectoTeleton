using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesWit
{
    public class ContextMap
    {
        public List<Context> context { get; set; }
        public ContextMap(List<Context> context)
        {
            this.context = context;
        }
        public ContextMap() { }
    }

}
