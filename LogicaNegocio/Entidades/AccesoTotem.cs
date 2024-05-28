using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class AccesoTotem : IValidar
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

        public void Validar()
        {
            if(IdSesionTotem == 0)
            {
                throw new AccesoTotemException("No se encontro la sesion del totem");
            }
            if(String.IsNullOrEmpty(CedulaPaciente)) { throw new AccesoTotemException("No se recibio cedula para el acceso al totem"); }

            if(FechaHora == DateTime.MinValue)
            {
                throw new AccesoTotemException("No se inicializo correctamente la fecha del acceso");
            }
        }
    }
}
