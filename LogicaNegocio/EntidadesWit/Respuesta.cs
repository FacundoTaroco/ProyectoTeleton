using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesWit
{
    public class Respuesta
    {
        public string text { get; set; }
        public Respuesta(string text)
        {
            this.text = text;
        }
        public Respuesta() { }
    }

}
