using LogicaAplicacion.CasosUso.AccesoTotemCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.CasosUso.TotemCU;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.EncuestaCU
{
    public class EnviarEncuestas
    {
        private GetPacientes _getPacietes;
        private AccesoCU _accesoCU;
        private GetTotems _totems;
        private ABMPacientes _abmPacientes;
        public EnviarEncuestas(GetPacientes getPacientes, AccesoCU acceso, ABMPacientes aBMPacientes, GetTotems totems) { 
            _getPacietes = getPacientes;
            _accesoCU = acceso; 
            _abmPacientes = aBMPacientes;
            _totems = totems;
        }

        public void EnviarSolicitudEncuestas() {
            DateTime _fecha = DateTime.UtcNow;
            TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            DateTime fechaHoy = TimeZoneInfo.ConvertTimeFromUtc(_fecha, zonaHoraria);
            Totem totem = _totems.GetTotemPorUsr("totemMVD");
            IEnumerable<AccesoTotem> Accesos = _accesoCU.GetAccesos(totem.Id);
            IEnumerable<AccesoTotem> AccesosDeHoy = Accesos.Where(a => a.FechaHora.Year == fechaHoy.Year && a.FechaHora.Month == fechaHoy.Month && a.FechaHora.Day == fechaHoy.Day);
            foreach (AccesoTotem a in AccesosDeHoy) {
                Paciente paciente = _getPacietes.GetPacientePorCedula(a.CedulaPaciente);
                paciente.ParaEncuestar = true;
                _abmPacientes.ModificarPaciente(paciente);
            }
        }
    }
}
