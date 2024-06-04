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

namespace AppTeleton.Controllers
{
    public class NotificacionController : Controller
    {
        private GetRecepcionistas _getRecepcionistas;
        private GetPacientes _getPacientes;
        private GuardarDispositivoNotificacion _guardarDispositivoNotificacion;
        private GetDispositivos _getDispositivos;
   
        private IConfiguration _config;
        public NotificacionController(IConfiguration configuration,GetRecepcionistas getRecepcionistas, GetPacientes getPacientes, GuardarDispositivoNotificacion guardarDisp, GetDispositivos getDispositivos) { 
            
            _getRecepcionistas = getRecepcionistas;
            _getPacientes = getPacientes;
            _guardarDispositivoNotificacion = guardarDisp;
            _getDispositivos = getDispositivos;
            _config=  configuration;
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
                return View("Index");
            }


        }
        [HttpGet]

        public IActionResult EnviarAvisos() {

            return View( _getPacientes.GetAll());

        }

        [RecepcionistaAdminLogueado]
        [HttpPost]
        public async Task<IActionResult> SendTodosLosPacientes(string titulo, string mensaje)
        {
            try
            {
                IEnumerable<DispositivoNotificacion> dispositivos = _getDispositivos.getAllDispositivos();
                if (dispositivos.Count() > 0)
                {
                    NotificacionDTO payload = new NotificacionDTO(titulo, mensaje);
                    string vapidPublicKey = _config["ClavesNotificaciones:PublicKey"];
                    string vapidPrivateKey = _config["ClavesNotificaciones:PrivateKey"];
                    //NO LE LLEGA EL CUERPO Y TITULO
                    foreach (DispositivoNotificacion dispositivo in dispositivos)
                    {
                        if(dispositivo.Usuario is Paciente) { 
                        PushSubscription pushSubscription = new PushSubscription(dispositivo.Endpoint, dispositivo.P256dh, dispositivo.Auth);
                        VapidDetails vapidDetails = new VapidDetails("mailto:lucasahre05@gmail.com", vapidPublicKey, vapidPrivateKey);
                        WebPushClient webPushClient = new WebPushClient();
                        webPushClient.SetVapidDetails(vapidDetails);
                        string payloadToken = JsonConvert.SerializeObject(payload, new JsonSerializerSettings()
                        {
                            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                            ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        });
                        webPushClient.SendNotification(pushSubscription, payloadToken);
                        }
                    }

                    ViewBag.Mensaje = "notificacion enviada con exito a todos los pacientes con dispositivos registrados";
                    ViewBag.TipoMensaje = "EXITO";
                    return View("EnviarAvisos", _getPacientes.GetAll());
                 
                }
                else
                {
                    throw new Exception("El paciente no tiene dispositivos registrados");
                }
            }

            catch (Exception e)
            {

                ViewBag.Mensaje = e.Message;
                ViewBag.TipoMensaje = "ERROR";
                return View("EnviarAvisos", _getPacientes.GetAll());
            }
        }



        [RecepcionistaAdminLogueado]
        [HttpPost]
        public async Task<IActionResult> SendUnUsuario(int idUsuario, string titulo, string mensaje)
        {
            try
            {
                IEnumerable<DispositivoNotificacion> dispositivos = _getDispositivos.getDispositivosPacientePorId(idUsuario);
                if (dispositivos.Count() > 0)
                {

                    NotificacionDTO payload = new NotificacionDTO(titulo, mensaje);


                    string vapidPublicKey = _config["ClavesNotificaciones:PublicKey"];
                    string vapidPrivateKey = _config["ClavesNotificaciones:PrivateKey"];

                    //NO LE LLEGA EL CUERPO Y TITULO
                    foreach (DispositivoNotificacion dispositivo in dispositivos)
                    {

                        PushSubscription pushSubscription = new PushSubscription(dispositivo.Endpoint, dispositivo.P256dh, dispositivo.Auth);
                        VapidDetails vapidDetails = new VapidDetails("mailto:lucasahre05@gmail.com", vapidPublicKey, vapidPrivateKey);
                        WebPushClient webPushClient = new WebPushClient();
                        webPushClient.SetVapidDetails(vapidDetails);
                        string payloadToken = JsonConvert.SerializeObject(payload, new JsonSerializerSettings()
                        {
                            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                            ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        });
                        webPushClient.SendNotification(pushSubscription, payloadToken);
                       
                    }

                    ViewBag.Mensaje = "notificacion enviada con exito";
                    ViewBag.TipoMensaje = "EXITO";
                    return View("EnviarAvisos",_getPacientes.GetAll());
                }
                else
                {
                    throw new Exception("El paciente no tiene dispositivos registrados");
                }
            }

            catch (Exception e)
            {
                ViewBag.Mensaje = e.Message;
                ViewBag.TipoMensaje = "ERROR";
                return View("EnviarAvisos",_getPacientes.GetAll());
            }
        }


        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

    }
}
