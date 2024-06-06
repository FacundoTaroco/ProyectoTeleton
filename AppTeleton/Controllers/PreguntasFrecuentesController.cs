using AppTeleton.Models;
using AppTeleton.Models.Filtros;
using LogicaAplicacion.CasosUso.MedicoCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.CasosUso.PreguntasFrecCU;
using LogicaAplicacion.CasosUso.RecepcionistaCU;
using LogicaAplicacion.CasosUso.TotemCU;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Controllers
{
    public class PreguntasFrecuentesController : Controller
    {
        private readonly ABMPreguntasFrec _abmPreguntasFrec;
        private readonly GetPreguntasFrec _getPreguntasFrec;

        public PreguntasFrecuentesController(GetPreguntasFrec getPreguntasFrec, ABMPreguntasFrec abmPreguntasFrec)
        {
            _getPreguntasFrec = getPreguntasFrec;
            _abmPreguntasFrec = abmPreguntasFrec;
        }
        public IActionResult PreguntasFrecuentes()
        {
            var modelo = ObtenerModeloPreguntasFrec();
            ViewBag.IsAdminOrRecepcionista = User.IsInRole("Admin") || User.IsInRole("Recepcionista");
            return View(modelo);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _abmPreguntasFrec.BajaPreguntaFrec(id);
                return RedirectToAction("PreguntasFrecuentes", new { mensaje = "Pregunta frecuente eliminada temporalmente con éxito, se cargará del servidor central nuevamente en el próximo llamado ", tipoMensaje = "EXITO" });
            }
            catch (Exception)
            {
                return RedirectToAction("PreguntasFrecuentes", new { mensaje = "Error al eliminar pregunta frecuente", tipoMensaje = "ERROR" });
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PreguntaFrec preguntaFrec)
        {
            try
            {
                _abmPreguntasFrec.AltaPreguntaFrec(preguntaFrec);
                ViewBag.TipoMensaje = "EXITO";
                ViewBag.Mensaje = "Pregunta frecuente agregada con éxito";
                return RedirectToAction("PreguntasFrecuentes");
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var preguntaFrec = _getPreguntasFrec.GetPreguntaFrecPorId(id);
            if (preguntaFrec == null)
            {
                return NotFound();
            }
            return View(preguntaFrec);
        }

        [HttpPost]
        public IActionResult Edit(PreguntaFrec preguntaFrec)
        {
            try
            {
                _abmPreguntasFrec.ModificarPreguntaFrec(preguntaFrec);
                ViewBag.TipoMensaje = "EXITO";
                ViewBag.Mensaje = "Pregunta frecuente editada con éxito";
                return RedirectToAction("PreguntasFrecuentes");
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View(preguntaFrec);
            }
        }

        private PreguntasFrecViewModel ObtenerModeloPreguntasFrec()
        {
            IEnumerable<PreguntaFrec> preguntasFrec = _getPreguntasFrec.GetAll();
            PreguntasFrecViewModel modeloIndex = new PreguntasFrecViewModel(preguntasFrec);
            return modeloIndex;
        }
    }
}
