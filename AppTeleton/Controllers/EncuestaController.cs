using LogicaAplicacion.CasosUso.EncuestaCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Controllers
{
    public class EncuestaController : Controller
    {
        private AgregarEncuesta _agregarEncuesta;
        private GetPacientes _getPacientes;
        private ABMPacientes _abmPacientes;

        public EncuestaController(AgregarEncuesta agregarEncuesta,GetPacientes getPacientes, ABMPacientes aBMPacientes) {
            _agregarEncuesta = agregarEncuesta;
            _getPacientes = getPacientes;
            _abmPacientes= aBMPacientes;
        } 
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Encuesta encuesta, string nombreUsuario) {

            try
            {
                _agregarEncuesta.Agregar(encuesta);
                Paciente paciente = _getPacientes.GetPacientePorUsuario(nombreUsuario);
                paciente.ParaEncuestar = false;
                _abmPacientes.ModificarPaciente(paciente);
                return RedirectToAction("Index","Citas");
            }
            catch (Exception e)
            {

                ViewBag.Mensaje = e.Message;
                ViewBag.TipoMensaje = "ERROR";
                return View();
            }
        }
        [HttpPost]
        public IActionResult NoCrear(string nombreUsuario) {
            Paciente paciente = _getPacientes.GetPacientePorUsuario(nombreUsuario);
            paciente.ParaEncuestar = false;
            _abmPacientes.ModificarPaciente(paciente);
            return RedirectToAction("Index", "Citas");
        }
    }
}
