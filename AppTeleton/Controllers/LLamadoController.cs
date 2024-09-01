using AppTeleton.Models.Filtros;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Controllers
{
    public class LLamadoController : Controller
    {

     //Muestra la pantalla de llamados medicos
        public IActionResult Index()
        {
            return View("MostrarLLamado");
        }


    }
}
