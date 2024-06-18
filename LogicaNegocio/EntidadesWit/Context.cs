using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesWit
{
    public class Context
    {
        public List<ContextBody> list { get; set; }
        public Context(List<ContextBody> list)
        {
            this.list = list;
        }
        public Context() { }
    }
}
