using LogicaNegocio.InterfacesDominio;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Controllers
{
    public class TotemController : Controller
    {

        ILogin _login;

        public TotemController(ILogin login)
        {
            _login = login;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CerrarSesion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CerrarSesion(string NombreUsuario, string Contrasenia)
        {
            try
            {
                // Validar las credenciales del usuario
                string tipoUsuario = _login.LoginCaso(NombreUsuario, Contrasenia);

                // Si la validación es correcta y el usuario es un totem, redirige al Logout de UsuarioController
                if (tipoUsuario == "TOTEM")
                {
                    return RedirectToAction("Logout", "Usuario", new { isTotem = true });
                }

                // Si no es un usuario totem, mostrar mensaje de error
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "Usuario o contraseña incorrectos para cerrar sesión del totem.";
                return View();
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View();
            }
        }
    }
}
