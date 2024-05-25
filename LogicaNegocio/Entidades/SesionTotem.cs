using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class SesionTotem:IValidar
    {
        public int Id { get; set; }
        public DateTime InicioSesion { get; set; }

        public bool SesionAbierta { get; set; }
        public int TotemId { get; set; }
        public Totem _Totem { get; set; }

        public List<AccesoTotem> Accesos { get; set; } = new List<AccesoTotem>();

        public SesionTotem() { }
    
        public SesionTotem(Totem totem)
        {
            InicioSesion = DateTime.Now;
            SesionAbierta = true;
            _Totem = totem;
            TotemId = totem.Id;
        }

        public void Validar()
        {
            if(TotemId == 0)
            {
                throw new SesionTotemException("La sesion tiene que estar asociada a un totem");
            }
        }
    }
}