using AppTeleton.Models;
using AppTeleton.Models.Filtros;
using LogicaAplicacion.CasosUso.EncuestaCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace AppTeleton.Controllers
{
    //controller que gestiona las acciones de las encuestas
    public class EncuestaController : Controller
    {
        private AgregarEncuesta _agregarEncuesta;
        private GetPacientes _getPacientes;
        private ABMPacientes _abmPacientes;
        private GetEncuestas _getEncuestas;

        public EncuestaController(GetEncuestas getEncuestas,  AgregarEncuesta agregarEncuesta,GetPacientes getPacientes, ABMPacientes aBMPacientes) {
            _agregarEncuesta = agregarEncuesta;
            _getPacientes = getPacientes;
            _abmPacientes= aBMPacientes;
            _getEncuestas  = getEncuestas;
        } 
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }
        //Crear una encuesta
        [HttpPost]
        public IActionResult Create(Encuesta encuesta, string nombreUsuario) {

            try
            {
                if (String.IsNullOrEmpty(encuesta.Comentarios)) {
                    encuesta.Comentarios = "Sin comentarios";
                }
                encuesta.agregarFecha();
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

        //Visualizar los datos de encuestas
        [HttpGet]
        [RecepcionistaAdminLogueado]
        public IActionResult VisualizarDatosEncuestas() {
            try
            {
                DatosEncuestasViewModel model = new DatosEncuestasViewModel();
                List<Encuesta> encuestas = _getEncuestas.GetAll().OrderByDescending(e => e.Fecha).ToList();
                model.Encuestas = encuestas;
                model.PromedioSatisfaccionGeneral = Math.Round(Encuesta.GetPromedioSatisfaccionGeneral(encuestas), 1); 
                model.PromedioSatisfaccionAplicacion = Math.Round(Encuesta.GetPromedioSatisfaccionAplicacion(encuestas), 1);
                model.PromedioSatisfaccionRecepcion = Math.Round(Encuesta.GetPromedioSatisfaccionRecepcion(encuestas), 1); 
                model.PromedioSatisfaccionEstadoCentro = Math.Round(Encuesta.GetPromedioSatisfaccionEstadoDelCentro(encuestas), 1);

                return View(model);

            }
            catch (Exception e)
            {
                DatosEncuestasViewModel model = new DatosEncuestasViewModel();

                ViewBag.Mensaje = e.Message;
                ViewBag.TipoMensaje = "ERROR";
                return View(model);
            }
      
        }


        [HttpPost]
        [RecepcionistaAdminLogueado]
        //Filtro para visualizar los datos de encuestas entre dos fechas
        public IActionResult VisualizarDatosEncuestasFechaFiltro(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                

                if (fechaFin.Equals(DateTime.MinValue)) { 
                    fechaFin = DateTime.MaxValue;
                }

                DatosEncuestasViewModel model = new DatosEncuestasViewModel();
                List<Encuesta> encuestas = _getEncuestas.GetAll().Where(e=> DateOnly.FromDateTime(e.Fecha)>= DateOnly.FromDateTime(fechaInicio) && DateOnly.FromDateTime(e.Fecha) <= DateOnly.FromDateTime(fechaFin)).OrderByDescending(e => e.Fecha).ToList();
                model.Encuestas = encuestas;
                model.PromedioSatisfaccionGeneral = Math.Round(Encuesta.GetPromedioSatisfaccionGeneral(encuestas), 1);
                model.PromedioSatisfaccionAplicacion = Math.Round(Encuesta.GetPromedioSatisfaccionAplicacion(encuestas), 1);
                model.PromedioSatisfaccionRecepcion = Math.Round(Encuesta.GetPromedioSatisfaccionRecepcion(encuestas), 1);
                model.PromedioSatisfaccionEstadoCentro = Math.Round(Encuesta.GetPromedioSatisfaccionEstadoDelCentro(encuestas), 1);

                return View("VisualizarDatosEncuestas",model);

            }
            catch (Exception e)
            {
                DatosEncuestasViewModel model = new DatosEncuestasViewModel();
                ViewBag.Mensaje = e.Message;
                ViewBag.TipoMensaje = "ERROR";
                return View("VisualizarDatosEncuestas",model);
            }

        }



        [HttpGet]
        [RecepcionistaAdminLogueado]
        //detalles de una encuesta
        public IActionResult Detalles(int id) {
            try
            {
                Encuesta encuesta = _getEncuestas.GetPorId(id);

                return View("Details", encuesta);


            }
            catch (Exception)
            {

                throw;
            }
        
        }
    }
}
