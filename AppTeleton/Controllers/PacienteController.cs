using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Controllers
{
    public class PacienteController : Controller
    {

        ABMPacientes _abmPacientes { get; set; }
        GetPacientes _getPacientes { get; set; }

        public PacienteController(ABMPacientes abmPacientes, GetPacientes getPacientes) { 
        
            _abmPacientes = abmPacientes;   
            _getPacientes = getPacientes;
        
        }
        public IActionResult Index()
        {
            return View();
        }
  
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
    }
}
