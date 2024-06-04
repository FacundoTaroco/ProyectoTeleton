using AppTeleton.Models.Filtros;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Controllers
{
    [MedicoLogueado]
    public class MedicoController : Controller
    {
    
        public IActionResult Index()
        {
            return View();
        }
    }
}
