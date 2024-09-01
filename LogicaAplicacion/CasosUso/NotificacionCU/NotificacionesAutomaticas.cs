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
        private GetNotificacion _getNotificacion;
        public NotificacionesAutomaticas(GetNotificacion getNotificacion, SolicitarCitasService solicitarCitas, EnviarNotificacionService enviarNotificaciones, GetPacientes getPacientes ) { 
            _solicitarCitas = solicitarCitas;
            _enviarNotificaciones = enviarNotificaciones;
            _getPacientes = getPacientes;
            _getNotificacion = getNotificacion;
        }

        public async Task<bool> EnviarRecordatorioCitaMasTemprana() {

            try
            {

                DateTime _fecha = DateTime.UtcNow;
                TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
                DateTime fechaUruguay = TimeZoneInfo.ConvertTimeFromUtc(_fecha, zonaHoraria);

                IEnumerable<Paciente> pacientes = _getPacientes.GetAll();
                foreach (Paciente p in pacientes) {

                IEnumerable<CitaMedicaDTO> citasDePaciente = await _solicitarCitas.ObtenerCitasPorCedula(p.Cedula);
                citasDePaciente= citasDePaciente.OrderBy(p => p.Fecha).ThenBy(p => p.HoraInicio).Where(p => p.Estado == "RPA" && p.Fecha >= fechaUruguay).ToList();
                if (citasDePaciente.Count() > 0) {

                 CitaMedicaDTO citaMasReciente = citasDePaciente.First();
                        int diasQueFaltan = (citaMasReciente.Fecha - DateTime.Now).Days;
                        if (citaMasReciente != null && diasQueFaltan <= _getNotificacion.GetParametrosRecordatorios().CadaCuantoEnviarRecordatorio) { 
                        
                        string tituloNotificacion = "RECORDATORIO: Su próxima cita en Teletón";
                        string mensajeNotificacion = "El " + citaMasReciente.Fecha.ToShortDateString() + " a las " + citaMasReciente.HoraInicio + " hs Tiene agendado para " + citaMasReciente.Servicio;
                        _enviarNotificaciones.Enviar(tituloNotificacion, mensajeNotificacion, "https://appteletonrecepcion.azurewebsites.net/Paciente/NotificacionesPaciente", p.Id);

                        }                        
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
