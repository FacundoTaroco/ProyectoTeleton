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
    //Controller para la gestion de notificaciones
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

        //borra una notificacion
        public IActionResult Borrar(int id) {
            try
            {
                _ABNotificacion.Delete(id);
                return RedirectToAction("NotificacionesPaciente", "Paciente");
            }
            catch (Exception)
            {

                throw;
            }
        
        
        
        }


        //guarda un dispositivo para mandarle notificaciones push con sus credenciales

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

                

                    return RedirectToAction("Index", "Citas");
                
            }
            catch (Exception)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "Algo salio mal al activar las notificaciones";
                string tipoUsuario = HttpContext.Session.GetString("TIPO");
                
                    return RedirectToAction("Index", "Citas");
                
               
            }


        }
        [HttpGet]
        [RecepcionistaAdminLogueado]
        //Carga la vista para enviar avisos generales
        public IActionResult EnviarAvisos() {

            try
            {
            ParametrosNotificaciones parametros =  _getNotificacion.GetParametrosRecordatorios();
            ViewBag.RecordatoriosEncendidos = parametros.RecordatoriosEncendidos;
            ViewBag.RecordatorioAntelacion = parametros.CadaCuantoEnviarRecordatorio;
                return View(_getPacientes.GetAll().OrderBy(p => p.Nombre).ToList()); 
            }
            catch (Exception)
            {

                throw;
            }
          

        }
        [RecepcionistaAdminLogueado]
        [HttpGet]
        //carga la vista para configurar los recordatorios
        public IActionResult ConfigurarRecordatorios() {
            ParametrosNotificaciones parametros = _getNotificacion.GetParametrosRecordatorios();
            return View(parametros);
        }
        [RecepcionistaAdminLogueado]
        [HttpPost]
        //Actualiza la configuracion de recordatorios
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


       
        [RecepcionistaAdminLogueado]
        [HttpPost]
        //Manda un aviso como notificacion push a todos los pacientes
        public async Task<IActionResult> SendTodosLosPacientes(string titulo, string mensaje)
        {

            try
            {
                if (String.IsNullOrEmpty(titulo)) throw new Exception("Ingrese un titulo para la notificacion");
                if (String.IsNullOrEmpty(mensaje)) throw new Exception("Ingrese un mensaje para la notificacion");

                _enviarNotificacionService.EnviarATodos(titulo, mensaje, "https://localhost:7051/Paciente/NotificacionesPaciente");
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
        //manda un aviso como notificacion push a una lista de pacientes
        public async Task<IActionResult> SendPacientes(string titulo, string mensaje, List<int> seleccionados)
        {
            try
            {
                if (seleccionados.Count()==0) throw new Exception("Seleccione por lo menos un paciente");
                if(String.IsNullOrEmpty(titulo)) throw new Exception("Ingrese un titulo para la notificación");
                if (String.IsNullOrEmpty(mensaje)) throw new Exception("Ingrese un mensaje para la notificación");


                foreach (int idPaciente in seleccionados) { 
                
                    //llama al servicio de envio de notificaciones para cada paciente del listado
                 _enviarNotificacionService.Enviar(titulo, mensaje, "https://localhost:7051/Paciente/NotificacionesPaciente", idPaciente);
                
                }

               
                    ViewBag.Mensaje = "Notificación enviada con éxito";
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
