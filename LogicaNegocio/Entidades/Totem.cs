using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Totem: Usuario
    {
        private static Totem instance = null;
        private static readonly object padlock = new object();

        public List<AccesoTotem> Accesos { get; set; } = new List<AccesoTotem>();
       

        // Constructor privado para patrón singleton
        private Totem()
        {

            //this.Nombre = "Totem Principal";

            this.Nombre = "Totem Montevideo";
            this.NombreUsuario = "totemMVD";
            this.Contrasenia = "totem123";
        }

        // Constructor público requerido por Entity Framework
        public Totem(bool forEF = false)
        {
        }
        public static Totem Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Totem();
                    }
                    return instance;
                }
            }
        }
    }
}
