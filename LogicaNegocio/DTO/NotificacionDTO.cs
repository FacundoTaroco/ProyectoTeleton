using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.DTO
{
    public class NotificacionDTO
    {
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public string Link { get; set; }

        public NotificacionDTO()
        {


        }

        public NotificacionDTO(string titulo, string mensaje)
        {
            Titulo = titulo;
            Mensaje = mensaje;
        }

        public NotificacionDTO(string titulo, string mensaje, string link)
        {
            Titulo = titulo;
            Mensaje = mensaje;
            Link = link;
        }
    }
}
