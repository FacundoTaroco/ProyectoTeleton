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
        //LUCAS: Saque fin de sesion ya que no nos interesa ese dato, agrege SesionAbierta para saber si esa sesion esta abierta
            public bool SesionAbierta { get; set; }
            public int TotemId { get; set; }
            public Totem _Totem { get; set; }
        //LUCAS: Lista para guardar los accesos que se van haciendo al totem
            public List<AccesoTotem> Accesos { get; set; } = new List<AccesoTotem>();   

        //constructores para poder hacer las sesiones
        public SesionTotem() { 
            _Totem = Totem.Instance;
            TotemId = _Totem.Id;
        }
        public SesionTotem(DateTime inicioSesion) {
            _Totem = Totem.Instance;
            TotemId = _Totem.Id;
            InicioSesion = inicioSesion;
            SesionAbierta = true;
        }

        
        }
    }

