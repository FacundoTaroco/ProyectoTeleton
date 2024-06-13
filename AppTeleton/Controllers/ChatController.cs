using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using AppTeleton.Models.Filtros;

namespace AppTeleton.Controllers
{
    [PacienteRecepcionistaLogueado]
    public class ChatController : Controller
    {
        public IActionResult Chat()
        {
            ViewBag.Usuario = HttpContext.Session.GetString("USR");
            return View();
        }
    }
}
