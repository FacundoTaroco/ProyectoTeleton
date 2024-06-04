using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Notificacion : IValidar
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public Usuario Usuario { get; set; }
        public int IdUsuario {get;set;}

        public Notificacion() { 
        
        
        }

        public Notificacion(string titulo, string mensaje, Usuario usuario)
        {
            Titulo = titulo;
            Mensaje = mensaje;
            Usuario = usuario;
            IdUsuario = usuario.Id;
        }

        public void Validar()
        {
           //IMPLEMENTAR VALIDAR NOTIFICACIONES
        }
    }
}
