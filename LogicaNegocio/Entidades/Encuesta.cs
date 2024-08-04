using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Encuesta : IValidar
    {   //que se pida la encuesta al final de día a las 17:00
        //que en el totem diga que se puede hacer una encuesta en la página web y el acompañante va a realizarla en el correr dél día
        //Las encuestas tienen valores del 1 al 5 y 0 en caso de que se seleccione "No contesta"
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int SatisfaccionGeneral { get; set; } // Puntuación de 1 a 5
        public int SatisfaccionRecepcion { get; set; }
        public int SatisfaccionEstadoDelCentro { get; set; }
        public int SatisfaccionAplicacion { get; set; }

        public string Comentarios { get; set; }//text area

        public Encuesta() { }

        public Encuesta(int id, int satisfaccionGeneral, string comentarios)
        {
            Id = id;
            SatisfaccionGeneral = satisfaccionGeneral;
            Comentarios = comentarios;
            DateTime _fecha = DateTime.UtcNow;
            TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            Fecha = TimeZoneInfo.ConvertTimeFromUtc(_fecha, zonaHoraria);
        }

        public void Validar()
        {
           //HACEEER
        }
    }
}
