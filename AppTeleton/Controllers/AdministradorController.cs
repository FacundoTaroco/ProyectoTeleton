using AppTeleton.Models;
using LogicaAplicacion.CasosUso.AdministradorCU;
using LogicaAplicacion.CasosUso.MedicoCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.CasosUso.RecepcionistaCU;
using LogicaAplicacion.CasosUso.TotemCU;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Controllers
{
    public class AdministradorController : Controller
    {

        public GetAdministradores _getAdministradores;
        public GetRecepcionistas _getRecepcionistas;
        public GetPacientes _getPacientes;
        public GetMedicos _getMedicos;
        public ABMAdministradores _ABMAdministradores;
        public ABMRecepcionistas _ABMRecepcionistas;
        public ActualizarPacientes _actualizarPacientes;
        
        public AdministradorController(GetAdministradores listaAdmins, GetRecepcionistas listaRecepcionistas, GetPacientes listaPacientes, GetMedicos listaMedicos, ABMAdministradores abmAdministradores, ABMRecepcionistas abmRecepcionistas, ActualizarPacientes actualizarPacientes)
        {
            _getAdministradores = listaAdmins;
            _getRecepcionistas = listaRecepcionistas;
            _getPacientes = listaPacientes;
            _getMedicos = listaMedicos;
            _ABMAdministradores = abmAdministradores;
            _ABMRecepcionistas = abmRecepcionistas;
            _actualizarPacientes = actualizarPacientes;
        }
      
        [HttpGet]
        public IActionResult Index(string tipoUsuario,string tipoMensaje, string mensaje)
        {
            if(!String.IsNullOrEmpty(tipoMensaje) && !String.IsNullOrEmpty(mensaje)) {
                ViewBag.TipoMensaje = tipoMensaje;
                ViewBag.Mensaje = mensaje;  
            
            }
            ViewBag.TipoUsuario = tipoUsuario;
            if (String.IsNullOrEmpty(tipoUsuario)) {
                ViewBag.TipoUsuario = "PACIENTE";
            }
            return View(ObtenerModeloUsuarios());
        }

        [HttpGet]
        public IActionResult VerTipoUsuario(string opcion) {

            if (opcion == "paciente")
            {
                ViewBag.TipoUsuario = "PACIENTE";
            }
            else if (opcion == "recepcionista") {
                ViewBag.TipoUsuario = "RECEPCIONISTA";
            }
            else if (opcion == "admin")
            {
                ViewBag.TipoUsuario = "ADMIN";
            }

            return View("Index", ObtenerModeloUsuarios());

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
                ViewBag.TipoUsuario = "ADMIN";
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
                ViewBag.TipoUsuario = "RECEPCIONISTA";
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
        public async Task<IActionResult> ActualizarPacientes() {
            try
            {

                await _actualizarPacientes.Actualizar();
                ViewBag.TipoMensaje = "EXITO";
                ViewBag.Mensaje ="Usuarios Actualizados con exito";
                ViewBag.TipoUsuario = "PACIENTE";
                return View("Index", ObtenerModeloUsuarios());
            }
            catch (Exception e)
            {

                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                ViewBag.TipoUsuario = "PACIENTE";
                return View("Index", ObtenerModeloUsuarios());
            }
        
        }

        private UsuariosViewModel ObtenerModeloUsuarios() {
            IEnumerable<Paciente> pacientes = _getPacientes.GetAll();
            IEnumerable<Recepcionista> recepcionistas = _getRecepcionistas.GetAll();
            IEnumerable<Administrador> admins = _getAdministradores.GetAll();
            IEnumerable<Medico> medicos= _getMedicos.GetAll();


            UsuariosViewModel modeloIndex = new UsuariosViewModel(pacientes, admins, recepcionistas, medicos);
            return modeloIndex;
        }

    }
}
