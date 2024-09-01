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
        //Controller que maneja las acciones especificas del rol de usuario Administrador

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
        public IActionResult AgregarAdmin() { 
        return View();
        }

        //Agregar un nuevo administrador
        [HttpPost]
        public IActionResult AgregarAdmin(Administrador admin) {

            try
            {
                _ABMAdministradores.AltaAdmin(admin);
                
                return RedirectToAction("ListadoUsuarios", "Usuario", new { tipoUsuario = TipoUsuario.Admin, tipoMensaje = "EXITO", mensaje= "Administrador agregado con exito" });

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
        //Agregar un nuevo Recepcionista
        [HttpPost]
        public IActionResult AgregarRecepcionista(Recepcionista recepcionista)
        {

            try
            {
                _ABMRecepcionistas.AltaRecepcionista(recepcionista);
                ViewBag.TipoMensaje = "EXITO";
                ViewBag.Mensaje = "Recepcionista agregado con exito";
                ViewBag.TipoUsuario = TipoUsuario.Recepcionista;
                return RedirectToAction("ListadoUsuarios", "Usuario", new { tipoUsuario = TipoUsuario.Recepcionista, tipoMensaje = "EXITO", mensaje = "Recepcionista agregado con exito" });
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View();
            }

           
        }

        public IActionResult EnviarNotificacionUsuario(int idUsuario, string mensaje) {

            if (!String.IsNullOrEmpty(mensaje)) {
                ViewBag.Mensaje = mensaje;
            }

            ViewBag.idUsuario = idUsuario;
        return View("MandarNotificacionUsuario");
        
        }


    }
}
