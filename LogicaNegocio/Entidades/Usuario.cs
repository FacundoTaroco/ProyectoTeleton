using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Usuario { 
    
        public int Id { get; set; }
        public string Nombre { get; set;}
        public string NombreUsuario { get; set;}
        public string Contrasenia { get; set;}

        public Usuario(string nombreUsr, string contrasenia)
        {
            NombreUsuario = nombreUsr;
            Contrasenia = contrasenia;
        }
        public Usuario(string nombreUsr, string contrasenia, string nombre)
        {
            Nombre = nombre;
            NombreUsuario = nombreUsr;
            Contrasenia = contrasenia;
        }

        //constructor vacio
        //hola
        public Usuario() { }

    }
}
