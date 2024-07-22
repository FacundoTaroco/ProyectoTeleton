using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Encuesta
    {   //que se pida la encuesta al final de día a las 19:00
        //que en el totem diga que se puede hacer una encuesta en la página web y el acompañante va a realizarla en el correr dél día
        public int Id { get; set; }
        public int SatisfaccionGeneral { get; set; } // Puntuación de 1 a 5
        public string Comentarios { get; set; }//text area

        public Encuesta() { }

        public Encuesta(int id,int satisfaccionGeneral, string comentarios)
        {
            Id = id;
            SatisfaccionGeneral = satisfaccionGeneral;
            Comentarios = comentarios;
        }
    }
}
