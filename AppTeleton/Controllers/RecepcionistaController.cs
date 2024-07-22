﻿using AppTeleton.Models;
using AppTeleton.Models.Filtros;
using LogicaAccesoDatos.EF;
using LogicaAplicacion.CasosUso.AdministradorCU;
using LogicaAplicacion.CasosUso.CitaCU;
using LogicaAplicacion.CasosUso.DispositivoUsuarioCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.CasosUso.RecepcionistaCU;
using LogicaAplicacion.CasosUso.TotemCU;
using LogicaAplicacion.Excepciones;
using LogicaAplicacion.Servicios;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AppTeleton.Controllers
{
    [RecepcionistaLogueado]
    public class RecepcionistaController : Controller
    {
        public GuardarDispositivoNotificacion _guardarDispositivo;
        public GetRecepcionistas _getRecepcionistas;
        public IRepositorioCitaMedica _repositorioCitaMedica;
        public SolicitarCitasService _solicitarCitasService;
        public ILogger<RecepcionistaController> _logger;
        public GenerarAvisoLlegada _generarAvisoLlegada;
        public CambiarContrasenia _cambiarContrasenia;
        public IRepositorioPaciente _repositorioPaciente;
        public GetCitas _getCitas;
        public GetPacientes _getPacientes;
        public ABMCitas _abmCitaMedica;


        public RecepcionistaController(
            GuardarDispositivoNotificacion guardarDispositivo,
            GetRecepcionistas getRecepcionistas,
            IRepositorioCitaMedica repositorioCitaMedica,
            SolicitarCitasService solicitarCitasService,
            GenerarAvisoLlegada generarAvisoLlegada,
            ILogger<RecepcionistaController> logger,
            CambiarContrasenia cambiarContrasenia,
            IRepositorioPaciente repositorioPaciente,
            GetCitas getCitas,
            GetPacientes getPacientes,
            ABMCitas abmCitaMedica)
        {
            _guardarDispositivo = guardarDispositivo;
            _getRecepcionistas = getRecepcionistas;
            _repositorioCitaMedica = repositorioCitaMedica;
            _solicitarCitasService = solicitarCitasService;
            _generarAvisoLlegada = generarAvisoLlegada;
            _logger = logger;
            _cambiarContrasenia = cambiarContrasenia;
            _repositorioPaciente = repositorioPaciente;
            _getCitas = getCitas;
            _getPacientes = getPacientes;
            _abmCitaMedica = abmCitaMedica;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Obtener lista de todas las citas médicas
                IEnumerable<CitaMedicaDTO> todasLasCitas = await _solicitarCitasService.ObtenerCitas();

                DateTime _fecha = new DateTime(2024, 11, 4);
                TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
                DateTime fechaGMT = TimeZoneInfo.ConvertTimeFromUtc(_fecha, zonaHoraria);

                IEnumerable<CitaMedicaDTO> citasDelDia = todasLasCitas.Where(c => c.Fecha.Date == fechaGMT.Date);

                // Obtener el nombre de usuario del usuario autenticado
                string usuario = HttpContext.Session.GetString("USR");
                Recepcionista recepcionistaLogueado = _getRecepcionistas.GetRecepcionistaPorUsuario(usuario);

                if (recepcionistaLogueado == null)
                {
                    throw new Exception("No se pudo obtener el recepcionista logueado.");
                }

                // Crear el modelo para la vista
                RecepsionistaViewModel model = new RecepsionistaViewModel
                {
                    CitasMedicas = citasDelDia.ToList(),
                    IdUsuario = recepcionistaLogueado.Id
                };

                // Renderizar la vista con las citas médicas del día actual
                return View(model);
            }
            catch (TeletonServerException ex)
            {
                _logger.LogError($"Error al obtener las citas médicas del día: {ex.Message}");
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "Error al obtener las citas médicas del día.";
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al obtener las citas médicas del día: {ex.Message}");
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "Error inesperado al obtener las citas médicas del día.";
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarEstadoLlegadaAutomatico(int pkAgenda)
        {
            try
            {
                await _repositorioCitaMedica.RecepcionarPacienteAsync(pkAgenda);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar estado de llegada: {ex.Message}");
                return Json(new { success = false, message = "Error al actualizar estado de llegada." });
            }
        }

        [HttpPost]
        public IActionResult GuardarDispositivoNotificacion(string pushEndpoint, string pushP256DH, string pushAuth)
        {
            try
            {
                string usuario = HttpContext.Session.GetString("USR");
                Recepcionista recepcionistaLogueado = _getRecepcionistas.GetRecepcionistaPorUsuario(usuario);

                DispositivoNotificacion dispositivo = new DispositivoNotificacion
                {
                    Auth = pushAuth,
                    P256dh = pushP256DH,
                    Endpoint = pushEndpoint,
                    Usuario = recepcionistaLogueado,
                    IdUsuario = recepcionistaLogueado.Id
                };

                _guardarDispositivo.GuardarDispositivo(dispositivo);

                ViewBag.TipoMensaje = "SUCCESS";
                ViewBag.Mensaje = "Dispositivo de notificación guardado correctamente.";
                return View("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al guardar dispositivo de notificación: {ex.Message}");
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "Algo salió mal al activar las notificaciones";
                return View("Index");
            }
        }

        [HttpPost]
        public IActionResult SendPushNotification(int Id, string Titulo, string Payload)
        {
            try
            {
                ViewBag.Mensaje = "Notificación enviada correctamente";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al enviar notificación push: {ex.Message}");
                ViewBag.Mensaje = "Error al enviar notificación push";
            }

            return View("Send");
        }

        [HttpGet]
        public IActionResult CambiarContrasenia(int id)
        {
            ViewBag.IdUsuario = id;
            return View();
        }

        [HttpPost]
        public IActionResult CambiarContrasenia(int id, string nuevaContrasenia, string confirmarContrasenia)
        {
            if (nuevaContrasenia != confirmarContrasenia)
            {
                ViewBag.Mensaje = "Las contraseñas no coinciden";
                ViewBag.TipoMensaje = "ERROR";
                return View();
            }

            try
            {
                _cambiarContrasenia.ChangePassword(id, nuevaContrasenia);
                ViewBag.Mensaje = "Contraseña cambiada exitosamente";
                ViewBag.TipoMensaje = "EXITO";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListaPacientes()
        {
            try
            {
                var pacientesEntidades = _getPacientes.GetAll();

                var pacientesDTOs = pacientesEntidades.Select(p => new PacienteDTO
                {
                    NombreCompleto = p.Nombre,
                    Cedula = p.Cedula,
                }).ToList();

                var pacientes = pacientesDTOs.Select(dto => new Paciente
                {
                    Nombre = dto.NombreCompleto,
                    Cedula = dto.Cedula,
                }).ToList();

                var viewModel = new UsuariosViewModel
                {
                    Pacientes = pacientes,
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener la lista de pacientes: {ex.Message}");
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "Error al obtener la lista de pacientes.";
                return View();
            }
        }


        [HttpGet]
        public async Task<IActionResult> InformacionPaciente(string cedula)
        {
            var paciente = _repositorioPaciente.GetPacientePorCedula(cedula);
            if (paciente == null)
            {
                return NotFound();
            }

            var citasMedicas = _getCitas.ObtenerCitasPorCedula(cedula).Result;

            PacienteDTO pacienteDTO = new PacienteDTO
            {
                NombreCompleto = paciente.Nombre,
                Cedula = paciente.Cedula
            };

            ViewBag.CitasMedicas = citasMedicas;

            return View("InformacionPaciente", pacienteDTO);
        }

        [HttpGet]
        public async Task<IActionResult> InformacionCita(int id)
        {
            var citas = await _getCitas.ObtenerCitas();
            var cita = citas.FirstOrDefault(c => c.PkAgenda == id);

            if (cita == null)
            {
                return NotFound();
            }
            return View("InformacionCita", cita);
        }
    }
}