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
       
        public string CedulaPaciente { get; set; }
        public DateTime FechaHora { get; set; }


        public SesionTotem _SesionTotem { get; set; }
        public int IdSesionTotem { get; set; }


        public AccesoTotem() { }

        public AccesoTotem(string cedula, SesionTotem sesion) {
            _SesionTotem = sesion;
            IdSesionTotem = sesion.Id;
            FechaHora = DateTime.Now;
            CedulaPaciente = cedula;
        
        }

    }
}
