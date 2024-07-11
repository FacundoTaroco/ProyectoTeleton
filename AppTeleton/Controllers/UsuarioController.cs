using LogicaAccesoDatos.EF.Excepciones;
using LogicaAplicacion.CasosUso.TotemCU;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using NuGet.Protocol.Plugins;

namespace AppTeleton.Controllers
{
    public class UsuarioController : Controller
    {

        public ILogin _login;
        public GetTotems _getTotems;
        public IRepositorioUsuario _repositorioUsuario;


        public UsuarioController(ILogin login, GetTotems getTotems, IRepositorioUsuario repositorioUsuario)
        {
            _login = login;
            _getTotems = getTotems;
            _repositorioUsuario = repositorioUsuario;

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
                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(contrasenia))
                {
                    throw new Exception("Ingrese todos los campos");
                }

                TipoUsuario tipoUsuario = _login.LoginCaso(nombre, contrasenia);
                var usuario = _repositorioUsuario.GetUsuarioPorNombre(nombre); // Obtenemos el usuario por nombre

                HttpContext.Session.SetString("USR", nombre);
                HttpContext.Session.SetString("Id", usuario.Id.ToString()); // Guardamos el Id en la sesión
                ViewBag.TipoMensaje = "EXITO";
                ViewBag.Mensaje = "Sesión iniciada correctamente";

                switch (tipoUsuario)
                {
                    case TipoUsuario.Totem:
                        HttpContext.Session.SetString("TIPO", "TOTEM");
                        return RedirectToAction("Index", "Totem");
                    case TipoUsuario.Recepcionista:
                        HttpContext.Session.SetString("TIPO", "RECEPCIONISTA");
                        return RedirectToAction("Index", "Recepcionista");
                    case TipoUsuario.Admin:
                        HttpContext.Session.SetString("TIPO", "ADMIN");
                        return RedirectToAction("Index", "Administrador");
                    case TipoUsuario.Paciente:
                        HttpContext.Session.SetString("TIPO", "PACIENTE");
                        return RedirectToAction("Index", "Paciente");
                    case TipoUsuario.Medico:
                        HttpContext.Session.SetString("TIPO", "MEDICO");
                        return RedirectToAction("Index", "Medico");
                    default:
                        throw new Exception("No se recibió el tipo de usuario");
                }
            }
            catch (UsuarioException e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View("Login");
            }
            catch (NotFoundException)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "El usuario ingresado no existe";
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
