using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class AccesosFallidos
    {
        public static List<AccesoTotem> accesosFallidos = new List<AccesoTotem>();
        public static bool servicioDeReintentoActivado = false;
    }
}
