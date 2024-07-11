using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.EntidadesWit.GetContext;

namespace LogicaNegocio.EntidadesWit
{
    public class Evento
    {
        public string action { get; set; }
        public ContextMap context_map { get; set; }
        public bool expects_input { get; set; }
        public bool is_final { get; set; }
        public Respuesta response { get; set; }
        public string type { get; set; }

        public Evento(ContextMap context_map, Respuesta response, string action, bool expects_input, bool is_final, string type)
        {
            this.context_map = context_map;
            this.response = response;
            this.action = action;
            this.expects_input = expects_input;
            this.is_final = is_final;
            this.type = type;
        }
        public Evento() { }
    }
}
