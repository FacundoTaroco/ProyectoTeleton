using AppTeleton.Models;
using AppTeleton.Models.Filtros;
using LogicaAplicacion.CasosUso.DispositivoUsuarioCU;
using LogicaAplicacion.CasosUso.NotificacionCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Controllers
{

    public class PacienteController : Controller
    {

        ABMPacientes _abmPacientes { get;}
        GetPacientes _getPacientes { get;  }
        GetNotificacion _getNotificaciones { get; }

        public PacienteController(ABMPacientes abmPacientes, GetPacientes getPacientes,GetNotificacion getNotificacion) { 
        
            _abmPacientes = abmPacientes;   
            _getPacientes = getPacientes;
            _getNotificaciones = getNotificacion;


        }
        
        [AdminLogueado]
        public IActionResult Delete(int id) {
            try
            {
                _abmPacientes.BajaPaciente(id);
                return RedirectToAction("Index", "Administrador", new { tipoUsuario = "PACIENTE", mensaje = "Paciente eliminado temporalmente con exito, se cargara del servidor central nuevamente en el proximo llamado ", tipoMensaje = "EXITO" });
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Administrador", new { tipoUsuario = "PACIENTE", mensaje = "Error al eliminar al paciente", tipoMensaje = "ERROR" });
            }

        }
        [PacienteLogueado]
        [HttpGet]
        public IActionResult NotificacionesPaciente() {
            string usuarioPaciente = HttpContext.Session.GetString("USR");
            Paciente pacienteLogueado = _getPacientes.GetPacientePorUsuario(usuarioPaciente);
            IEnumerable<Notificacion> notificaciones = _getNotificaciones.GetPorUsuario(pacienteLogueado.Id);
            notificaciones = notificaciones.OrderByDescending(n => n.fecha).ToList();
            return View(notificaciones);
        }

      

    }
}
