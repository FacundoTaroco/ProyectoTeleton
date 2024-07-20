using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Recepcionista:Usuario
    {
   

        public Recepcionista() { }
        public Recepcionista(string nombre ,string nombreUsr, string contrasenia):base(nombreUsr,contrasenia,nombre)
        {
          
        }

      
    }
}
