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
        public Totem _Totem { get; set; }
        public int IdTotem { get; set; }


        public AccesoTotem() { }

        public AccesoTotem(string cedula, Totem totem) {
            _Totem = totem;
            IdTotem = totem.Id;
            DateTime _fecha = DateTime.UtcNow;
            TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            FechaHora = TimeZoneInfo.ConvertTimeFromUtc(_fecha, zonaHoraria);
            CedulaPaciente = cedula;
        
        }

        public void Validar()
        {
            if(IdTotem == 0)
            {
                throw new AccesoTotemException("No se encontro el totem");
            }
            if(String.IsNullOrEmpty(CedulaPaciente)) { throw new AccesoTotemException("No se recibio cedula para el acceso al totem"); }

            if(FechaHora == DateTime.MinValue)
            {
                throw new AccesoTotemException("No se inicializo correctamente la fecha del acceso");
            }
        }
    }
}
