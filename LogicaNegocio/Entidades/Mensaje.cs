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

        public Mensaje(string contenido,string nombreUsuario)
        {
            this.contenido = contenido;
            DateTime _fecha = DateTime.UtcNow;
            TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            fecha = TimeZoneInfo.ConvertTimeFromUtc(_fecha, zonaHoraria);
            this.nombreUsuario = nombreUsuario;
         
        }
    }
}
