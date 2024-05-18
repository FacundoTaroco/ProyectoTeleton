using LogicaAccesoDatos.EF.Excepciones;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesDominio;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace AppTeleton.Controllers
{
    public class UsuarioController : Controller
    {

        ILogin _login;

        public UsuarioController(ILogin login)
        {
            _login = login;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Usuario Usr)
        {
            return View();

        }

        public IActionResult Login()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("USR")))
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "Se cerro su sesion";
                HttpContext.Session.Clear();
            }

            return View();
        }
        [HttpPost]
        public IActionResult Login(string nombre, string Contrasenia)
        {
            try
            {
                bool UsrValido = _login.LoginCaso(nombre, Contrasenia);


                HttpContext.Session.SetString("USR", nombre);
                ViewBag.TipoMensaje = "EXITO";
                ViewBag.Mensaje = "Sesion iniciada correctamente";
                return RedirectToAction("Index", "Home");

            }
            catch (UsuarioException e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View("Login");
            }
            catch (DomainException e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "Contraseña incorrecta";
                return View("Login");
            }
            catch (NotFoundException)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "No se encontro al usuario";
                return View("Login");
            }
            catch (ServerErrorException)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "Error del servidor al realizar el login";
                return View("Login");
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View("Login");
            }



        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            ViewBag.TipoMensaje = "ERROR";
            ViewBag.Mensaje = "Se cerro la sesion";
            return View("Login");
        }

    }
}
