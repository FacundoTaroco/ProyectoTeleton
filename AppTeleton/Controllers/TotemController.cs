using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Controllers
{
    public class TotemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
