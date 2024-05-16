using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
        public class SesionTotem
        {
            public int Id { get; set; }
            public DateTime InicioSesion { get; set; }
            public DateTime? FinSesion { get; set; }
            public int TotemId { get; set; }
            public Totem Totem { get; set; }
        }
    }

