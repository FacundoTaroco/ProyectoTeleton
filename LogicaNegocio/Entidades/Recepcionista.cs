using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Recepcionista:Usuario,IValidar
    {
        //aca tendria chats y otras cosas 

        public Recepcionista() { }
        public Recepcionista(string nombre ,string nombreUsr, string contrasenia):base(nombreUsr,contrasenia,nombre)
        {
          
        }

        public void Validar()
        {
            //IMPLEMENTAR
        }
    }
}
