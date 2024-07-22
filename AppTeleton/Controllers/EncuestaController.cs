using AppTeleton.Models;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Controllers
{


    public class EncuestaController : Controller
    {
        public IRepositorioEncuesta _repositorioEncuesta;

        public EncuestaController(IRepositorioEncuesta repositorioEncuesta)
        {
            _repositorioEncuesta = repositorioEncuesta;
        }

        // Acción para mostrar la vista de creación de encuestas
        /*public IActionResult Add()
        {
            return View();
        }

        // Acción HTTP POST para procesar el formulario de creación de encuestas
        [HttpPost]
        public IActionResult Add(Encuesta encuesta)
        {
            if (ModelState.IsValid)
            {
                _repositorioEncuesta.Add(encuesta);
                return RedirectToAction("Index");
            }
            return View(encuesta);
        }*/

        // Otras acciones para actualizar, obtener, y eliminar encuestas según sea necesario
    }

}

