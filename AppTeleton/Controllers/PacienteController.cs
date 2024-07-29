﻿using AppTeleton.Models;
using AppTeleton.Models.Filtros;
using LogicaAccesoDatos.EF;
using LogicaAplicacion.CasosUso.AdministradorCU;
using LogicaAplicacion.CasosUso.DispositivoUsuarioCU;
using LogicaAplicacion.CasosUso.EncuestaCU;
using LogicaAplicacion.CasosUso.NotificacionCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.CasosUso.PreguntasFrecCU;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppTeleton.Controllers
{
    public class PacienteController : Controller
    {
        public ABMPacientes _abmPacientes { get;}
        public GetPacientes _getPacientes { get;  }
        public GetNotificacion _getNotificaciones { get; }
        public CambiarContrasenia _cambiarContrasenia;
        public ABMEncuestas _abmEncuestas;
        public GetEncuestas _getEncuestas;
        public GetPreguntasFrec _getPreguntasFrec;

        public PacienteController(ABMPacientes abmPacientes, GetPacientes getPacientes,GetNotificacion getNotificacion, CambiarContrasenia cambiarContrasenia, ABMEncuestas abmEncuestas, GetEncuestas getEncuestas, GetPreguntasFrec getPreguntasFrec) { 
        
            _abmPacientes = abmPacientes;   
            _getPacientes = getPacientes;
            _getNotificaciones = getNotificacion;
            _cambiarContrasenia = cambiarContrasenia;
            _abmEncuestas = abmEncuestas;
            _getEncuestas = getEncuestas;
            _getPreguntasFrec = getPreguntasFrec;
        }
        [HttpGet]
        public IActionResult CrearEncuesta()
        {
            return View();
        }

        // Acción HTTP POST para procesar el formulario de creación de encuestas
        [HttpPost]
        public IActionResult GuardarEncuesta(Encuesta encuesta)
        {
            /*if (ModelState.IsValid)
            {
                _abmEncuestas.AltaEncuesta(encuesta);
                return RedirectToAction("Index");
            }
            return View("CrearEncuesta");*/
            if (ModelState.IsValid)
            {
                try
                {
                    _abmEncuestas.AltaEncuesta(encuesta);
                    return RedirectToAction("Index");
                }
                catch (ArgumentException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Ocurrió un error inesperado. Por favor, inténtelo de nuevo.";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Debe completar todos los campos correctamente.";
            }

            return View("CrearEncuesta");
        }

        [PacienteLogueado]
        public IActionResult Index()
        {
            return View();
        }
        [AdminLogueado]
        public IActionResult Delete(int id) {
            try
            {
                _abmPacientes.BajaPaciente(id);
                return RedirectToAction("Index", "Administrador", new { tipoUsuario = "PACIENTE", mensaje = "Paciente eliminado temporalmente con exito, se cargara del servidor central nuevamente en el proximo llamado ", tipoMensaje = "EXITO" });
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Administrador", new { tipoUsuario = "PACIENTE", mensaje = "Error al eliminar al paciente", tipoMensaje = "ERROR" });
            }

        }

        [HttpGet]
        public IActionResult CambiarContrasenia()
        {
            int idUsuario = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            ViewBag.IdUsuario = idUsuario;
            return View();
        }

        [HttpPost]
        public IActionResult CambiarContrasenia(string nuevaContrasenia, string confirmarContrasenia)
        {
            int idUsuario = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            if (nuevaContrasenia != confirmarContrasenia)
            {
                ViewBag.Mensaje = "Las contraseñas no coinciden";
                ViewBag.TipoMensaje = "ERROR";
                return View();
            }

            try
            {
                _cambiarContrasenia.ChangePassword(idUsuario, nuevaContrasenia);
                ViewBag.Mensaje = "Contraseña cambiada exitosamente";
                ViewBag.TipoMensaje = "EXITO";
            }
            catch (Exception ex)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = ex.Message;
                return View();
            }
            return View();
        }

        public IActionResult PreguntasFrecuentes()
        {
            var modelo = ObtenerModeloPreguntasFrec();
            return View(modelo);
        }

        public PreguntasFrecViewModel ObtenerModeloPreguntasFrec()
        {
            IEnumerable<PreguntaFrec> preguntasFrec = _getPreguntasFrec.GetAll();
            PreguntasFrecViewModel modeloIndex = new PreguntasFrecViewModel(preguntasFrec);
            return modeloIndex;
        }

        public IActionResult Detalle(int id)
        {
            var preguntaFrec = _getPreguntasFrec.GetPreguntaFrecPorId(id);
            if (preguntaFrec == null)
            {
                return NotFound();
            }
            return View(preguntaFrec);
        }


        [PacienteLogueado]
        [HttpGet]
        public IActionResult NotificacionesPaciente() {
            string usuarioPaciente = HttpContext.Session.GetString("USR");
            Paciente pacienteLogueado = _getPacientes.GetPacientePorUsuario(usuarioPaciente);
            IEnumerable<Notificacion> notificaciones = _getNotificaciones.GetPorUsuario(pacienteLogueado.Id);
            return View(notificaciones);
        }

    }
}
