using AppTeleton.Models;
using AppTeleton.Models.Filtros;
using LogicaAplicacion.CasosUso.CitaCU;
using LogicaAplicacion.CasosUso.NotificacionCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.Excepciones;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace AppTeleton.Controllers
{
    //Controller para gestionar las diferentes acciones relacionadas a las citas medicas
    public class CitasController : Controller
    {

        private GetCitas _getCitas;
        private GetPacientes _getPacientes;
        private GetNotificacion _getNotificacion;

        public CitasController(GetNotificacion getNotificacion,GetCitas getCitas, GetPacientes getPacientes) { 
            _getCitas = getCitas;
            _getPacientes  = getPacientes;
            _getNotificacion = getNotificacion;
        }

        //carga la vista principal con diferencias segun el rol de usuario que solicita la accion
        [RecepcionistaAdminPacienteLogueado] //filtro de usuarios
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.Encuestar = "no";
                string tipoUsuario = HttpContext.Session.GetString("TIPO");
                string usuario = HttpContext.Session.GetString("USR");
                ViewBag.TipoUsuario = tipoUsuario;
                IEnumerable<CitaMedicaDTO> citasAMostrar = new List<CitaMedicaDTO>();
                DateTime _fechaHoy = DateTime.UtcNow;
                TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
                DateTime hoyGMT = TimeZoneInfo.ConvertTimeFromUtc(_fechaHoy, zonaHoraria);

                CitasViewModel model;

                if (tipoUsuario == "PACIENTE")
                {
                    Paciente pacienteLogueado = _getPacientes.GetPacientePorUsuario(usuario);
                    citasAMostrar = await _getCitas.ObtenerCitasPorCedula(pacienteLogueado.Cedula);
                    citasAMostrar = citasAMostrar.OrderBy(c => c.Fecha).ThenBy(c => c.HoraInicio).Where(c => c.Fecha.Date >= hoyGMT.Date).ToList();

                    Paciente paciente = _getPacientes.GetPacientePorUsuario(HttpContext.Session.GetString("USR"));
                    Notificacion notificacionMasReciente = _getNotificacion.GetMasRecientePorUsuario(paciente.Id);
                    model = new CitasViewModel(notificacionMasReciente);
                    if (paciente.ParaEncuestar)
                    {
                        ViewBag.Encuestar = "si";
                    }

                    model.CargarModelo(citasAMostrar);

                    return View(model);
                }
                else
                {

                    IEnumerable<CitaMedicaDTO> citas = await _getCitas.ObtenerCitas();
                    citasAMostrar = citas.Where(c => c.Fecha.Day == hoyGMT.Day && c.Fecha.Month == hoyGMT.Month && c.Fecha.Year == hoyGMT.Year).ToList();
                    model = new CitasViewModel();
                }

                model.CargarModelo(citasAMostrar);

                return View(model);
            }
            catch (TeletonServerException e) {

                ViewBag.Mensaje = "El servidor central no se encuentra disponible para obtener las citas agendadas";
                ViewBag.TipoMensaje = "ERROR";
                return View(new CitasViewModel());
            }
            catch (Exception e)
            {

                ViewBag.Mensaje = e.Message;
                ViewBag.TipoMensaje = "ERROR";
                return View(new CitasViewModel());
            }
        }

        //carga los detalles de una cita
        [HttpGet]
        public async Task<IActionResult> Detalles(int pkAgenda) {

            try
            {
            IEnumerable<CitaMedicaDTO> citas = await _getCitas.ObtenerCitas();
                CitaMedicaDTO citaAMostrar = citas.FirstOrDefault(c => c.PkAgenda == pkAgenda);
                if (citaAMostrar == null) {
                    throw new Exception("No se encontro la cita medica");
                }
                return View(citaAMostrar);
            }
            catch (Exception e)
            {


                return RedirectToAction("Index");
            }

           


        }

        //filtro de citas por nombre, fecha de inicio y fecha de fin

        [RecepcionistaAdminLogueado]
        public async Task<IActionResult> IndexFiltro(string nombre,string cedula, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {


            ViewBag.TipoUsuario = HttpContext.Session.GetString("TIPO");

            DateTime _fechaHoy = DateTime.UtcNow;
            TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            DateTime hoyGMT = TimeZoneInfo.ConvertTimeFromUtc(_fechaHoy, zonaHoraria);

              

                if (String.IsNullOrEmpty(cedula))
                {

                    IEnumerable<CitaMedicaDTO> citas = await _getCitas.ObtenerCitas();
                    IEnumerable<CitaMedicaDTO> citasFiltradas;


                    if ((nombre != null && nombre != "") && (!fechaInicio.Equals(DateTime.MinValue) && !fechaFin.Equals(DateTime.MinValue)))// La recepcionista filtra por nombre y entre dos fechas
                    {
                        citasFiltradas = citas.Where(c => (c.NombreCompleto.ToLower().Contains(nombre.ToLower())) && (c.Fecha >= fechaInicio && c.Fecha <= fechaFin)).ToList();
                    }
                    else if ((nombre == null || nombre == "") && (!fechaInicio.Equals(DateTime.MinValue) && !fechaFin.Equals(DateTime.MinValue))) //Filtra solo entre fechas
                    {
                        citasFiltradas = citas.Where(c => (c.Fecha >= fechaInicio && c.Fecha <= fechaFin)).ToList();
                    }
                    else if ((nombre != null && nombre != "") && (fechaInicio.Equals(DateTime.MinValue) && fechaFin.Equals(DateTime.MinValue)))//filtra solo por nombre
                    {
                        citasFiltradas = citas.Where(c => (c.NombreCompleto.ToLower().Contains(nombre.ToLower()))).ToList();
                    }
                    else if ((nombre != null && nombre != "") && (!fechaInicio.Equals(DateTime.MinValue)) && (fechaFin.Equals(DateTime.MinValue))) // filtra por nombre, desde determinada fecha hacia delante
                    {
                        citasFiltradas = citas.Where(c => (c.NombreCompleto.ToLower().Contains(nombre.ToLower())) && c.Fecha >= fechaInicio).ToList();
                    }
                    else if ((nombre != null && nombre != "") && (fechaInicio.Equals(DateTime.MinValue)) && (!fechaFin.Equals(DateTime.MinValue))) // filtra por nombre, desde determinada fecha hacia atras
                    {
                        citasFiltradas = citas.Where(c => (c.NombreCompleto.ToLower().Contains(nombre.ToLower())) && c.Fecha <= fechaFin).ToList();
                    }
                    else if ((nombre == null || nombre == "") && (!fechaInicio.Equals(DateTime.MinValue)) && (fechaFin.Equals(DateTime.MinValue))) // filtra desde determinada fecha hacia delante
                    {
                        citasFiltradas = citas.Where(c => c.Fecha >= fechaInicio).ToList();
                    }
                    else if ((nombre == null || nombre == "") && (fechaInicio.Equals(DateTime.MinValue)) && (!fechaFin.Equals(DateTime.MinValue))) // filtra desde determinada fecha hacia atras
                    {
                        citasFiltradas = citas.Where(c => c.Fecha <= fechaFin).ToList();
                    }
                    else
                    {
                        citasFiltradas = citas.Where(c => c.Fecha.Day == hoyGMT.Day && c.Fecha.Month == hoyGMT.Month && c.Fecha.Year == hoyGMT.Year).ToList();
                    }
                    CitasViewModel model = new CitasViewModel();

                    model.CargarModelo(citasFiltradas);


                    return View("Index", model);


                }
                else {

                    IEnumerable<CitaMedicaDTO> citas = await _getCitas.ObtenerCitasPorCedula(cedula);
                    IEnumerable<CitaMedicaDTO> citasFiltradas = citas.OrderBy(c => c.Fecha).ThenBy(c => c.HoraInicio).ToList();
                    CitasViewModel model = new CitasViewModel();

                    model.CargarModelo(citasFiltradas);

                    return View("Index", model);
                }
           
            }
            catch (Exception e )
            {
                ViewBag.Mensaje = e.Message;
                ViewBag.TipoMensaje = "ERROR";
                return View(new CitasViewModel());
            }
        }
    }
}
