using AppTeleton.Models;
using LogicaAccesoDatos.EF.Excepciones;
using LogicaAplicacion.CasosUso.AdministradorCU;
using LogicaAplicacion.CasosUso.MedicoCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.CasosUso.RecepcionistaCU;
using LogicaAplicacion.CasosUso.TotemCU;
using LogicaAplicacion.CasosUso.UsuarioCU;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesDominio;
using Microsoft.AspNetCore.Mvc;
using AppTeleton.Models.Filtros;
using Microsoft.AspNetCore.Server.IIS.Core;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Http;

namespace AppTeleton.Controllers
{
    //Controller que gestiona las diferentes acciones de usuarios
    public class UsuarioController : Controller
    {

        private ILogin _login;
        private GetTotems _getTotems;
        private GetUsuarios _getUsuarios;
        private CambiarContrasenia _cambiarContrasenia;
        private GetAdministradores _getAdministradores;
        private GetRecepcionistas _getRecepcionistas;
        private GetPacientes _getPacientes;
        private GetMedicos _getMedicos;
        private ABMAdministradores _ABMAdministradores;
        private ABMRecepcionistas _ABMRecepcionistas;
        private ActualizarPacientes _actualizarPacientes;
        


        public UsuarioController(GetUsuarios getUsuarios, ILogin login, GetTotems getTotems, CambiarContrasenia cambiarContrasenia, GetAdministradores listaAdmins, GetRecepcionistas listaRecepcionistas, GetPacientes listaPacientes, GetMedicos listaMedicos, ABMAdministradores abmAdministradores, ABMRecepcionistas abmRecepcionistas, ActualizarPacientes actualizarPacientes)
        {
            _login = login;
            _getTotems = getTotems;
            _getUsuarios = getUsuarios;
            _cambiarContrasenia = cambiarContrasenia;
            _getAdministradores = listaAdmins;
            _getRecepcionistas = listaRecepcionistas;
            _getPacientes = listaPacientes;
            _getMedicos = listaMedicos;
            _ABMAdministradores = abmAdministradores;
            _ABMRecepcionistas = abmRecepcionistas;
            _actualizarPacientes = actualizarPacientes;
        }

        //Carga el login
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
        //ejecuta  el login
        [HttpPost]
        public IActionResult Login(string nombre, string contrasenia)
        {
            try
            {
                if(String.IsNullOrEmpty(nombre) ||String.IsNullOrEmpty(contrasenia))
                {
                    throw new Exception("Ingrese todos los campos");
                }

                //Limpiamos el nombre
                nombre = nombre.Replace(" ", "");

                TipoUsuario tipoUsuario = _login.LoginCaso(nombre, contrasenia);

                Usuario usuario = _getUsuarios.GetUsuarioNombre(nombre);
                string idUsuario = usuario.Id.ToString();
                
                HttpContext.Session.SetString("USR", nombre);
                HttpContext.Session.SetString("Id", idUsuario);
                ViewBag.TipoMensaje = "EXITO";
                ViewBag.Mensaje = "Sesion iniciada correctamente";
                if (tipoUsuario == TipoUsuario.Totem)
                {
                    HttpContext.Session.SetString("TIPO", "TOTEM");
                    Totem totem = _getTotems.GetTotemPorUsr(nombre);
                    return RedirectToAction("Index", "Totem");
                }
                else if (tipoUsuario == TipoUsuario.Recepcionista)
                {
                    HttpContext.Session.SetString("TIPO", "RECEPCIONISTA");
                    return RedirectToAction("Index", "Citas");
                }
                else if (tipoUsuario == TipoUsuario.Admin)
                {
                    HttpContext.Session.SetString("TIPO", "ADMIN");
                    return RedirectToAction("Index", "Citas");
                    /*return RedirectToAction("ListadoUsuarios", "Usuario", new { tipoUsuario = TipoUsuario.Recepcionista });*/

                }
                else if (tipoUsuario == TipoUsuario.Paciente)
                {
                    HttpContext.Session.SetString("TIPO", "PACIENTE");
                    return RedirectToAction("Index", "Citas");
                    /*return RedirectToAction("Index", "Paciente");*/
                }
                else if (tipoUsuario == TipoUsuario.Medico)
                {
                    HttpContext.Session.SetString("TIPO", "MEDICO");
                    return RedirectToAction("Index", "Medico");
                }
                else {
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
            ViewBag.Mensaje = "Se cerró la sesión";
            return View("Login");
        }

        //carga la vista de perfil
        [UsuarioLogueado]
        public IActionResult Perfil(int idUsuario) {
            try
            {
                if (idUsuario == 0) {
                    idUsuario = Int32.Parse(HttpContext.Session.GetString("Id"));
                }
                if (HttpContext.Session.GetString("TIPO").Equals("PACIENTE") && idUsuario != Int32.Parse(HttpContext.Session.GetString("Id"))) {

                    throw new Exception("Se cerro su sesión");
                }

                    Usuario usuario = _getUsuarios.GetUsuario(idUsuario);
                ViewBag.IdUsuario = idUsuario;
                return View(usuario);
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                
                return View("Login");
            }
        }

        //Cambia la contraseña de un usuario
        [UsuarioLogueado]
        [HttpPost]
        public IActionResult CambiarContrasenia(int idUsuario,string contrasenia, string contraseniaRepeticion)
        {
            try
            {
                ViewBag.IdUsuario = idUsuario;
                if (String.IsNullOrEmpty(contrasenia) || String.IsNullOrEmpty(contraseniaRepeticion)) {
                    throw new Exception("Ingrese ambos campos de contraseña");
                }


                if (!contrasenia.Equals(contraseniaRepeticion)) {
                    throw new Exception("Las contraseñas ingresadas no coinciden");
                }

                Usuario usuario = _getUsuarios.GetUsuario(idUsuario);
                usuario.Contrasenia = contrasenia;
                _cambiarContrasenia.Cambiar(usuario);
                ViewBag.TipoMensaje = "EXITO";
                ViewBag.Mensaje = "Contraseña actualizada con exito";
                return View("Perfil",usuario);
            }
            catch (Exception e)
            {
                ViewBag.IdUsuario = idUsuario;
                Usuario usuario = _getUsuarios.GetUsuario(idUsuario);
                ViewBag.Mensaje = e.Message;
                ViewBag.TipoMensaje = "ERROR";
                return View("Perfil",usuario);

            }
        }
        //Carga el listado de usuarios por tipo

        [RecepcionistaAdminLogueado]
        [HttpGet]
        public IActionResult ListadoUsuarios(TipoUsuario tipoUsuario, string tipoMensaje, string mensaje)
        {
           
            ViewBag.UsuarioLogueado = HttpContext.Session.GetString("TIPO");

            if (!String.IsNullOrEmpty(tipoMensaje) && !String.IsNullOrEmpty(mensaje))
            {
                ViewBag.TipoMensaje = tipoMensaje;
                ViewBag.Mensaje = mensaje;
            }

            ViewBag.TipoUsuario = tipoUsuario;
            if (tipoUsuario == TipoUsuario.NoLogueado)
            {
                ViewBag.TipoUsuario = TipoUsuario.Paciente;
            }
            return View(ObtenerModeloUsuarios());
        }
        [RecepcionistaAdminLogueado]
        [HttpGet]
        public IActionResult VerTipoUsuario(TipoUsuario opcion)
        {
            ViewBag.UsuarioLogueado = HttpContext.Session.GetString("TIPO");

            if (opcion == TipoUsuario.Paciente)
            {
                ViewBag.TipoUsuario = TipoUsuario.Paciente;
            }
            else if (opcion == TipoUsuario.Recepcionista)
            {
                ViewBag.TipoUsuario = TipoUsuario.Recepcionista;
            }
            else if (opcion == TipoUsuario.Admin)
            {
                ViewBag.TipoUsuario = TipoUsuario.Admin;
            }
            else if (opcion == TipoUsuario.Medico)
            {
                ViewBag.TipoUsuario = TipoUsuario.Medico;
            }
            else if (opcion == TipoUsuario.Totem)
            {
                ViewBag.TipoUsuario = TipoUsuario.Totem;
            }

            return View("ListadoUsuarios", ObtenerModeloUsuarios());

        }
        [RecepcionistaAdminLogueado]
        [HttpPost]

        //Actualiza los pacientes fijandose si se agregaron nuevos pacientes al servidor central de Teleton
        public async Task<IActionResult> ActualizarPacientes()
        {
            try
            {
                ViewBag.UsuarioLogueado = HttpContext.Session.GetString("TIPO");

                await _actualizarPacientes.Actualizar();
                ViewBag.TipoMensaje = "EXITO";
                ViewBag.Mensaje = "Usuarios Actualizados con exito";
                ViewBag.TipoUsuario = TipoUsuario.Paciente;
                return View("ListadoUsuarios", ObtenerModeloUsuarios());
            }
            catch (Exception e)
            {

                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                ViewBag.TipoUsuario = TipoUsuario.Paciente;
                return View("ListadoUsuarios", ObtenerModeloUsuarios());
            }
        }

        private UsuariosViewModel ObtenerModeloUsuarios()
        {
            IEnumerable<Paciente> pacientes = _getPacientes.GetAll();
            IEnumerable<Recepcionista> recepcionistas = _getRecepcionistas.GetAll();
            IEnumerable<Administrador> admins = _getAdministradores.GetAll();
            IEnumerable<Medico> medicos = _getMedicos.GetAll();
            IEnumerable<Totem> totems = _getTotems.GetAll();
            UsuariosViewModel modeloIndex = new UsuariosViewModel(pacientes, admins, recepcionistas, medicos, totems);
            return modeloIndex;
        }
    }
}
