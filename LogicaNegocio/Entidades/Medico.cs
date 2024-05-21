using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Medico : Usuario, IValidar
    {

        private static Medico instance = null;
        private static readonly object padlock = new object();

        public List<SesionMedico> Sesiones { get; set; } = new List<SesionMedico>();

        private Medico()
        {
            this.Nombre = "Medico 1";
            this.NombreUsuario = "medico";
            this.Contrasenia = "medico123";
        }

        // Constructor público requerido por Entity Framework
        public Medico(bool forEF = false)
        {
        }
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
