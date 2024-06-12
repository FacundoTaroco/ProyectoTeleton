using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.Servicios;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.NotificacionCU
{
    public class NotificacionesAutomaticas
    {
        private SolicitarCitasService _solicitarCitas;
        private EnviarNotificacionService _enviarNotificaciones;
        private GetPacientes _getPacientes;
        public NotificacionesAutomaticas(SolicitarCitasService solicitarCitas, EnviarNotificacionService enviarNotificaciones, GetPacientes getPacientes ) { 
            _solicitarCitas = solicitarCitas;
            _enviarNotificaciones = enviarNotificaciones;
            _getPacientes = getPacientes;
        }

        public async Task<bool> EnviarRecordatorioCitaMasTemprana() {

            try
            {
            IEnumerable<Paciente> pacientes = _getPacientes.GetAll();
            foreach (Paciente p in pacientes) {

                IEnumerable<CitaMedicaDTO> citasDePaciente = await _solicitarCitas.ObtenerCitasPorCedula(p.Cedula);
                if (citasDePaciente.Count() > 0) {
                citasDePaciente= citasDePaciente.OrderBy(p => p.Fecha);
                citasDePaciente= citasDePaciente.OrderBy(p => p.HoraInicio);

                 CitaMedicaDTO citaMasReciente = citasDePaciente.First();
                    string tituloNotificacion = "RECORDATORIO: Su proxima cita medica es el " + citaMasReciente.Fecha.ToShortDateString() + " a las " + citaMasReciente.HoraInicio + " hs";
                    string mensajeNotificacion = "Tiene agendado para " + citaMasReciente.Servicio;

                    _enviarNotificaciones.Enviar(tituloNotificacion, mensajeNotificacion, p.Id);
                        
                }
            }
                return true;
            }
            catch (Exception)
            {
               
                throw;
            }
            
           
        }


    }
}
