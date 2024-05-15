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
        public DateTime FechaAcceso { get; set; }
        public int TotemId { get; set; }
        public Totem Totem { get; set; }
    }
}
