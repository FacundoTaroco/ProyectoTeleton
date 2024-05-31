using AppTeleton.Models.Filtros;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Controllers
{
    
    public class PacienteController : Controller
    {

        ABMPacientes _abmPacientes { get; set; }
        GetPacientes _getPacientes { get; set; }
        GuardarDispositivoNotificacion _guardarDispositivo {  get; set; }

        public PacienteController(ABMPacientes abmPacientes, GetPacientes getPacientes, GuardarDispositivoNotificacion guardarDispositivo) { 
        
            _abmPacientes = abmPacientes;   
            _getPacientes = getPacientes;
            _guardarDispositivo = guardarDispositivo;   
        
        }
        [PacienteLogueado]
        public IActionResult Index()
        {
            return View();
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
        [HttpPost]
        public IActionResult GuardarDispositivoNotificacion(string pushEndpoint, string pushP256DH, string pushAuth) {

            try
            {
                string usuario = HttpContext.Session.GetString("USR");
                Paciente pacienteLogueado = _getPacientes.GetPacientePorUsuario(usuario);

                DispositivoNotificacion dispositivo = new DispositivoNotificacion();
                dispositivo.Auth = pushAuth;
                dispositivo.P256dh = pushP256DH;    
                dispositivo.Endpoint = pushEndpoint;
                dispositivo.Usuario = pacienteLogueado;
                dispositivo.IdUsuario = pacienteLogueado.Id;
                _guardarDispositivo.GuardarDispositivo(dispositivo);


                return View("Index");

            }
            catch (Exception)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "Algo salio mal al activar las notificaciones";
              return View("Index");
            }
       
        
        }

    }
}
