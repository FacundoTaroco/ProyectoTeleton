using AppTeleton.Models.Filtros;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.CasosUso.RecepcionistaCU;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using WebPush;
using LogicaAplicacion.CasosUso.DispositivoUsuarioCU;
using LogicaNegocio.DTO;
using LogicaAplicacion.CasosUso.NotificacionCU;
using LogicaAplicacion.Servicios;

namespace AppTeleton.Controllers
{
    public class NotificacionController : Controller
    {
        private GetRecepcionistas _getRecepcionistas;
        private GetPacientes _getPacientes;
        private GuardarDispositivoNotificacion _guardarDispositivoNotificacion;
        private GetDispositivos _getDispositivos;
        private ABNotificacion _ABNotificacion;
        private GetNotificacion _getNotificacion;
        private BorrarDispositivoNotificacion _borrarDispositivo;
        private EnviarNotificacionService _enviarNotificacionService;
   
        private IConfiguration _config;
        public NotificacionController(GetNotificacion getNotificacion, EnviarNotificacionService enviarNotificacion, BorrarDispositivoNotificacion borrarDispositivo,ABNotificacion aBNotificacion,IConfiguration configuration,GetRecepcionistas getRecepcionistas, GetPacientes getPacientes, GuardarDispositivoNotificacion guardarDisp, GetDispositivos getDispositivos) { 
            
            _getRecepcionistas = getRecepcionistas;
            _getPacientes = getPacientes;
            _guardarDispositivoNotificacion = guardarDisp;
            _getDispositivos = getDispositivos;
            _config=  configuration;
            _ABNotificacion= aBNotificacion;
            _borrarDispositivo = borrarDispositivo;
            _enviarNotificacionService = enviarNotificacion;
            _getNotificacion = getNotificacion;
        }
        public IActionResult Index()
        {
            return View();
        }



        [PacienteRecepcionistaLogueado]
        [HttpPost]
        public IActionResult GuardarDispositivoNotificacion(string pushEndpoint, string pushP256DH, string pushAuth)
        {

            try
            {
                string usuario = HttpContext.Session.GetString("USR");
                string tipoUsuario = HttpContext.Session.GetString("TIPO");
                Usuario usuarioLogueado;
                if (tipoUsuario == "PACIENTE")
                {

                    usuarioLogueado = _getPacientes.GetPacientePorUsuario(usuario);

                }
                else if (tipoUsuario == "RECEPCIONISTA")
                {
                    usuarioLogueado = _getRecepcionistas.GetRecepcionistaPorUsuario(usuario);
                }
                else {
                    throw new Exception();
                }

                DispositivoNotificacion dispositivo = new DispositivoNotificacion();
                dispositivo.Auth = pushAuth;
                dispositivo.P256dh = pushP256DH;
                dispositivo.Endpoint = pushEndpoint;
                dispositivo.Usuario = usuarioLogueado;
                dispositivo.IdUsuario = usuarioLogueado.Id;
                _guardarDispositivoNotificacion.GuardarDispositivo(dispositivo);

                if (tipoUsuario == "PACIENTE") {

                    return RedirectToAction("Index", "Paciente");
                }
                    return RedirectToAction("Index", "Recepcionista");
            }
            catch (Exception)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "Algo salio mal al activar las notificaciones";
                string tipoUsuario = HttpContext.Session.GetString("TIPO");
                if (tipoUsuario == "PACIENTE")
                {
                    return RedirectToAction("Index", "Paciente");
                }
                return RedirectToAction("Index", "Recepcionista");
            }


        }
        [HttpGet]

        public IActionResult EnviarAvisos() {

            try
            {
            ParametrosNotificaciones parametros =  _getNotificacion.GetParametrosRecordatorios();
            ViewBag.RecordatoriosEncendidos = parametros.RecordatoriosEncendidos;
            ViewBag.RecordatorioAntelacion = parametros.CadaCuantoEnviarRecordatorio;
            return View(_getPacientes.GetAll());
            }
            catch (Exception)
            {

                throw;
            }
          

        }

        [HttpGet]
        public IActionResult ConfigurarRecordatorios() {
            ParametrosNotificaciones parametros = _getNotificacion.GetParametrosRecordatorios();
            return View(parametros);
        }
        [HttpPost]
        public IActionResult ConfigurarRecordatorios(ParametrosNotificaciones nuevosParametros)
        {
            try
            {
                _ABNotificacion.ActualizarParametros(nuevosParametros);
                return RedirectToAction("EnviarAvisos");
            }
            catch (Exception e)
            {
                ParametrosNotificaciones parametros = _getNotificacion.GetParametrosRecordatorios();
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View(parametros);
            }
            
        }


        //EVENTUALMENTE HACER ESTO MAS DINAMICO(RECIBE UNA LISTA DE PACIENTES O LISTA DE IDS EN VEZ DE SER TODOS LOS PACIENTES PUEDE TRABAJAR CON GRUPOS DE PACIENTES)
        [RecepcionistaAdminLogueado]
        [HttpPost]
        public async Task<IActionResult> SendTodosLosPacientes(string titulo, string mensaje)
        {

            try
            {
                if (String.IsNullOrEmpty(titulo)) throw new Exception("Ingrese un titulo para la notificacion");
                if (String.IsNullOrEmpty(mensaje)) throw new Exception("Ingrese un mensaje para la notificacion");

                _enviarNotificacionService.EnviarATodos(titulo, mensaje);
                    ViewBag.Mensaje = "notificacion enviada con exito";
                    ViewBag.TipoMensaje = "EXITO";
                ParametrosNotificaciones parametros = _getNotificacion.GetParametrosRecordatorios();
                ViewBag.RecordatoriosEncendidos = parametros.RecordatoriosEncendidos;
                ViewBag.RecordatorioAntelacion = parametros.CadaCuantoEnviarRecordatorio;
                return View("EnviarAvisos", _getPacientes.GetAll());
            }

            catch (Exception e)
            {

                ViewBag.Mensaje = e.Message;
                ViewBag.TipoMensaje = "ERROR";
                ParametrosNotificaciones parametros = _getNotificacion.GetParametrosRecordatorios();
                ViewBag.RecordatoriosEncendidos = parametros.RecordatoriosEncendidos;
                ViewBag.RecordatorioAntelacion = parametros.CadaCuantoEnviarRecordatorio;
                return View("EnviarAvisos", _getPacientes.GetAll());
            }
        }



        [RecepcionistaAdminLogueado]
        [HttpPost]
        public async Task<IActionResult> SendUnUsuario(int idUsuario, string titulo, string mensaje)
        {
            try
            {
                if (idUsuario == 0) throw new Exception("No se recibio usuario");
                if(String.IsNullOrEmpty(titulo)) throw new Exception("Ingrese un titulo para la notificacion");
                if (String.IsNullOrEmpty(mensaje)) throw new Exception("Ingrese un mensaje para la notificacion");

                _enviarNotificacionService.Enviar(titulo, mensaje,idUsuario);
                    ViewBag.Mensaje = "notificacion enviada con exito";
                    ViewBag.TipoMensaje = "EXITO";
                ParametrosNotificaciones parametros = _getNotificacion.GetParametrosRecordatorios();
                ViewBag.RecordatoriosEncendidos = parametros.RecordatoriosEncendidos;
                ViewBag.RecordatorioAntelacion = parametros.CadaCuantoEnviarRecordatorio;
                return View("EnviarAvisos",_getPacientes.GetAll());
            }

            catch (Exception e)
            {
                ViewBag.Mensaje = e.Message;
                ViewBag.TipoMensaje = "ERROR";
                ParametrosNotificaciones parametros = _getNotificacion.GetParametrosRecordatorios();
                ViewBag.RecordatoriosEncendidos = parametros.RecordatoriosEncendidos;
                ViewBag.RecordatorioAntelacion = parametros.CadaCuantoEnviarRecordatorio;
                return View("EnviarAvisos",_getPacientes.GetAll());
            }
        }


   

    }
}
