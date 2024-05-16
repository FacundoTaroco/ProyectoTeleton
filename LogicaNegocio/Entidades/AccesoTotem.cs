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
        public int TotemId { get; set; }
        public DateTime FechaHora { get; set; }
        public string Accion { get; set; }

        public virtual Totem Totem { get; set; }
    }
}
