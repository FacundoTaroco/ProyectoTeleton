using AppTeleton.Models;
using AppTeleton.Models.Filtros;
using LogicaAplicacion.CasosUso.DispositivoUsuarioCU;
using LogicaAplicacion.CasosUso.RecepcionistaCU;
using LogicaAplicacion.Excepciones;
using LogicaAplicacion.Servicios;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AppTeleton.Controllers
{
    [RecepcionistaLogueado]
    public class RecepcionistaController : Controller
    {
        private readonly GuardarDispositivoNotificacion _guardarDispositivo;
        private readonly GetRecepcionistas _getRecepcionistas;
        private readonly IRepositorioCitaMedica _repositorioCitaMedica;
        private readonly SolicitarCitasService _solicitarCitasService;
        private readonly ILogger<RecepcionistaController> _logger;

        public RecepcionistaController(
            GuardarDispositivoNotificacion guardarDispositivo,
            GetRecepcionistas getRecepcionistas,
            IRepositorioCitaMedica repositorioCitaMedica,
            SolicitarCitasService solicitarCitasService,
            ILogger<RecepcionistaController> logger)
        {
            _guardarDispositivo = guardarDispositivo;
            _getRecepcionistas = getRecepcionistas;
            _repositorioCitaMedica = repositorioCitaMedica;
            _solicitarCitasService = solicitarCitasService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Obtener lista de todas las citas médicas
                IEnumerable<CitaMedicaDTO> todasLasCitas = await _solicitarCitasService.ObtenerCitas();

                // Filtrar las citas para obtener solo las del día actual
                DateTime _fecha = new DateTime(2024, 11, 4);
                TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
                DateTime fechaGMT = TimeZoneInfo.ConvertTimeFromUtc(_fecha, zonaHoraria);

                IEnumerable<CitaMedicaDTO> citasDelDia = todasLasCitas.Where(c => c.Fecha.Date == fechaGMT.Date);

                // Crear el modelo para la vista
                //que a RecepsionistaViewModel le el dto
                RecepsionistaViewModel model = new RecepsionistaViewModel
                {
                    CitasMedicas = citasDelDia
                };/*{
                    CitasMedicas = (IEnumerable<CitaMedicaDTO>)citasDelDia.Select(c => new CitaMedica(
                        pkAgenda: c.PkAgenda,
                        cedula: c.Cedula,
                        nombreCompleto: c.NombreCompleto,
                        servicio: "",
                        fecha: c.Fecha,
                        horaInicio: c.HoraInicio,
                        tratamiento: c.Tratamiento,
                        estado: c.Estado
                    )).ToList()
                };*/

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
        public async Task<IActionResult> ActualizarEstadoLlegadaAutomatico(int idCita, string llego)
        {
            try
            {
                // Actualizar estado de llegada en la base de datos
                await _repositorioCitaMedica.ActualizarEstadoLlegadaAsync(idCita, llego);
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
            // Lógica para enviar notificaciones push
            try
            {
                // Aquí implementa la lógica para enviar la notificación push
                ViewBag.Mensaje = "Notificación enviada correctamente";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al enviar notificación push: {ex.Message}");
                ViewBag.Mensaje = "Error al enviar notificación push";
            }

            return View("Send");
        }
    }
}