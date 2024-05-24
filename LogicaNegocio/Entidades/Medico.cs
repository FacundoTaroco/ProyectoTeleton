using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Medico : Usuario
    {

        private static Medico instance = null;
        private static readonly object padlock = new object();

     
        private Medico()
        {
            this.Nombre = "Medico Montevideo";
            this.NombreUsuario = "medicoMVD";
            this.Contrasenia = "medico123";
        }

        public Medico(bool forEF = false)
        {
        }

        // Constructor público requerido por Entity Framework
        public static Medico Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Medico();
                    }
                    return instance;
                }
            }
        }


    }
}
