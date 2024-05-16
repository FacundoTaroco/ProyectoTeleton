using AppTeleton.Models;
using LogicaAplicacion.CasosUso.AdministradorCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.CasosUso.RecepcionistaCU;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Controllers
{
    public class AdministradorController : Controller
    {

        public GetAdministradores _getAdministradores;
        public GetRecepcionistas _getRecepcionistas;
        public GetPacientes _getPacientes;
        public ABMAdministradores _ABMAdministradores;
        public ABMRecepcionistas _ABMRecepcionistas;
        public ActualizarPacientes _actualizarPacientes;
        public AdministradorController(GetAdministradores listaAdmins, GetRecepcionistas listaRecepcionistas, GetPacientes listaPacientes, ABMAdministradores abmAdministradores, ABMRecepcionistas abmRecepcionistas, ActualizarPacientes actualizarPacientes)
        {
            _getAdministradores = listaAdmins;
            _getRecepcionistas = listaRecepcionistas;
            _getPacientes = listaPacientes;
            _ABMAdministradores = abmAdministradores;
            _ABMRecepcionistas = abmRecepcionistas;
            _actualizarPacientes = actualizarPacientes;
        }
        public IActionResult Index()
        {
            
            return View(ObtenerModeloUsuarios());
        }


        [HttpGet]
        public IActionResult AgregarAdmin() { 
        return View();
        }

        [HttpPost]
        public IActionResult AgregarAdmin(Administrador admin) {

            try
            {
                _ABMAdministradores.AltaAdmin(admin);
                ViewBag.TipoMensaje = "EXITO";
                ViewBag.Mensaje = "Administrador agregado con exito";
                return View("Index", ObtenerModeloUsuarios());


            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View();
            }

        }


        [HttpGet]
        public IActionResult AgregarRecepcionista()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AgregarRecepcionista(Recepcionista recepcionista)
        {

            try
            {
                _ABMRecepcionistas.AltaRecepcionista(recepcionista);
                ViewBag.TipoMensaje = "EXITO";
                ViewBag.Mensaje = "Recepcionista agregado con exito";
                return View("Index", ObtenerModeloUsuarios());
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View();
            }

           
        }

        [HttpPost] 
        public IActionResult ActualizarPacientes() {
            try
            {

                _actualizarPacientes.Actualizar();
                ViewBag.TipoMensaje = "EXITO";
                ViewBag.Mensaje ="Usuarios Actualizados con exito";
                return View("Index", ObtenerModeloUsuarios());
            }
            catch (Exception e)
            {

                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View("Index", ObtenerModeloUsuarios());
            }
        
        }

        private UsuariosViewModel ObtenerModeloUsuarios() {
            IEnumerable<Paciente> pacientes = _getPacientes.GetAll();
            IEnumerable<Recepcionista> recepcionistas = _getRecepcionistas.GetAll();
            IEnumerable<Administrador> admins = _getAdministradores.GetAll();
            UsuariosViewModel modeloIndex = new UsuariosViewModel(pacientes, admins, recepcionistas);
            return modeloIndex;
        }

    }
}
