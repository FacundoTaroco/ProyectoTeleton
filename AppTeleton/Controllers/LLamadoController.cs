using AppTeleton.Models.Filtros;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Controllers
{
    public class LLamadoController : Controller
    {

     
        public IActionResult Index()
        {
            return View("MostrarLLamado");
        }


    }
}
