using LogicaAccesoDatos.EF.Excepciones;
using LogicaAplicacion.CasosUso.SesionTotemCU;
using LogicaAplicacion.CasosUso.TotemCU;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesDominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using NuGet.Protocol.Plugins;

namespace AppTeleton.Controllers
{
    public class UsuarioController : Controller
    {

        ILogin _login;
        GetTotems _getTotems;
        ABMSesionTotem _abmSesionTotem;
    
        public UsuarioController(ILogin login, ABMSesionTotem sesionTotem, GetTotems getTotems)
        {
            _login = login;
            _abmSesionTotem = sesionTotem;   
            _getTotems = getTotems;

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
        public IActionResult Login(string nombre, string contrasenia)
        {
            try
            {
                string tipoUsuario = _login.LoginCaso(nombre, contrasenia);

                HttpContext.Session.SetString("USR", nombre);
                HttpContext.Session.SetString("TIPO", tipoUsuario);
                ViewBag.TipoMensaje = "EXITO";
                ViewBag.Mensaje = "Sesion iniciada correctamente";




                if (tipoUsuario == "TOTEM") {
                

                    Totem totem = _getTotems.GetTotemPorUsr(nombre);
                    SesionTotem nuevaSesionTotem = new SesionTotem(totem);
                    SesionTotem sesionTot = _abmSesionTotem.AgregarSesion(nuevaSesionTotem);
                    HttpContext.Session.SetInt32("SESIONTOTEM", sesionTot.Id);
                    return RedirectToAction("Index", "Totem");


                }

                else if (tipoUsuario == "PACIENTE")
                {

                    return RedirectToAction("Index", "Paciente");

                }
                else if (tipoUsuario == "RECEPCIONISTA")
                {
                    return RedirectToAction("Index", "Recepcionista");

                }
                else if (tipoUsuario == "ADMIN")
                {
                    return RedirectToAction("Index", "Administrador");

                }
                else {
                    throw new Exception("No se recibio el tipo de usuario");
                }

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
            ViewBag.Mensaje = "Se cerró la sesion";
            return View("Login");
        }
    }
}
