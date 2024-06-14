using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Mensaje
    {
        public int Id { get; set; }


        public bool EsDePaciente { get; set; }
        public string contenido { get; set; }
        public DateTime fecha { get; set; }
        public string nombreUsuario { get; set; }   

        public Chat _Chat { get; set; }
        public int IdChat { get; set; }

        public Mensaje() { }    

        public Mensaje(string contenido, DateTime fecha, string nombreUsuario)
        {
            this.contenido = contenido;
            this.fecha = fecha;
            this.nombreUsuario = nombreUsuario;
         
        }
    }
}
