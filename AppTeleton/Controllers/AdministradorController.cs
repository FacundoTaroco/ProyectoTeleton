using AppTeleton.Models;
using AppTeleton.Models.Filtros;
using LogicaAplicacion.CasosUso.AdministradorCU;
using LogicaAplicacion.CasosUso.MedicoCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.CasosUso.RecepcionistaCU;
using LogicaAplicacion.CasosUso.TotemCU;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Controllers
{
    [AdminLogueado]
    public class AdministradorController : Controller
    {

        public GetAdministradores _getAdministradores;
        public GetRecepcionistas _getRecepcionistas;
        public GetPacientes _getPacientes;
        public GetMedicos _getMedicos;
        public ABMAdministradores _ABMAdministradores;
        public ABMRecepcionistas _ABMRecepcionistas;
        public ActualizarPacientes _actualizarPacientes;
        public GetTotems _getTotems;
        
        public AdministradorController(GetAdministradores listaAdmins, GetRecepcionistas listaRecepcionistas, GetPacientes listaPacientes, GetMedicos listaMedicos, ABMAdministradores abmAdministradores, ABMRecepcionistas abmRecepcionistas, ActualizarPacientes actualizarPacientes,GetTotems getTotems)
        {
            _getAdministradores = listaAdmins;
            _getRecepcionistas = listaRecepcionistas;
            _getPacientes = listaPacientes;
            _getMedicos = listaMedicos;
            _ABMAdministradores = abmAdministradores;
            _ABMRecepcionistas = abmRecepcionistas;
            _actualizarPacientes = actualizarPacientes;
            _getTotems = getTotems;
        }
      
        [HttpGet]
        public IActionResult Index(TipoUsuario tipoUsuario,string tipoMensaje, string mensaje)
        {
            if(!String.IsNullOrEmpty(tipoMensaje) && !String.IsNullOrEmpty(mensaje)) {
                ViewBag.TipoMensaje = tipoMensaje;
                ViewBag.Mensaje = mensaje;  
            }

            ViewBag.TipoUsuario = tipoUsuario;
            if (tipoUsuario == TipoUsuario.NoLogueado) {
                ViewBag.TipoUsuario = TipoUsuario.Paciente;
            }
            return View(ObtenerModeloUsuarios());
        }

        [HttpGet]
        public IActionResult VerTipoUsuario(TipoUsuario opcion) {

            if (opcion == TipoUsuario.Paciente)
            {
                ViewBag.TipoUsuario = TipoUsuario.Paciente;
            }
            else if (opcion == TipoUsuario.Recepcionista) {
                ViewBag.TipoUsuario = TipoUsuario.Recepcionista;
            }
            else if (opcion == TipoUsuario.Admin)
            {
                ViewBag.TipoUsuario = TipoUsuario.Admin;
            }
            else if(opcion == TipoUsuario.Medico)
            {
                ViewBag.TipoUsuario = TipoUsuario.Medico;
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
                ViewBag.TipoUsuario = TipoUsuario.Admin;
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
                ViewBag.TipoUsuario = TipoUsuario.Recepcionista;
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
                ViewBag.TipoUsuario = TipoUsuario.Paciente;
                return View("Index", ObtenerModeloUsuarios());
            }
            catch (Exception e)
            {

                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                ViewBag.TipoUsuario = TipoUsuario.Paciente;
                return View("Index", ObtenerModeloUsuarios());
            }
        
        }

        private UsuariosViewModel ObtenerModeloUsuarios() {
            IEnumerable<Paciente> pacientes = _getPacientes.GetAll();
            IEnumerable<Recepcionista> recepcionistas = _getRecepcionistas.GetAll();
            IEnumerable<Administrador> admins = _getAdministradores.GetAll();
            IEnumerable<Medico> medicos= _getMedicos.GetAll();
            IEnumerable<Totem> totems = _getTotems.GetAll();
            UsuariosViewModel modeloIndex = new UsuariosViewModel(pacientes, admins, recepcionistas, medicos,totems);
            return modeloIndex;
        }

    }
}
