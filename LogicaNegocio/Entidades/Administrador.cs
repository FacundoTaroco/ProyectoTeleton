using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Administrador:Usuario
    {

        public Administrador() { }


        public Administrador(string nombre, string nombreUsr, string contrasenia) : base(nombreUsr, contrasenia, nombre)
        {

        }

       
    }
}
