using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.DTO
{
    public class ComentarioEncuestaDTO
    {

        public string Comentario { get; set; }
        public int SatisfaccionGeneral { get; set; }   
        public DateTime Fecha { get; set; }
    }
}
