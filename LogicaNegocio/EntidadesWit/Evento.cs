using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LogicaNegocio.EntidadesWit
{
    public class Evento
    {
     
        public Respuesta response { get; set; }
        

        public Evento(Respuesta response)
        {
            this.response = response;
         
        }
        public Evento() { }
    }
}
