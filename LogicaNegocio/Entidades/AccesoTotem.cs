using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class AccesoTotem
    {
        public int Id { get; set; }
        //Saque totemId ya que el acceso esta guardado en una sesion, y la sesion guarda al totem 
        /*public int TotemId { get; set; }*/

        //guardamos quien ingreso con cada acceso
        public string CedulaPaciente { get; set; }
        public DateTime FechaHora { get; set; }
        public string Accion { get; set; }

        public virtual Totem Totem { get; set; }


        /*public virtual Totem Totem { get; set; } Lo mismo por lo que saque el id del totem*/

        //constructores

        public AccesoTotem() { }

        public AccesoTotem(DateTime fecha, string cedula) {
            FechaHora = fecha;
            CedulaPaciente = cedula;
        }

    }
}
