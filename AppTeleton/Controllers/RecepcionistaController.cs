using AppTeleton.Models.Filtros;
using LogicaAplicacion.CasosUso.DispositivoUsuarioCU;
using LogicaAplicacion.CasosUso.RecepcionistaCU;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Controllers
{
    /*[RecepcionistaLogueado]
    public class RecepcionistaController : Controller
    {

  
        private GetRecepcionistas _getRecepcionistas;
        public RecepcionistaController( GetRecepcionistas getRecepcionistas) {

            _getRecepcionistas = getRecepcionistas;
        }    
        public IActionResult Index()
        {
            return View();
        }


    }*/
    [RecepcionistaLogueado]
    public class RecepcionistaController : Controller
    {
        private GuardarDispositivoNotificacion _guardarDispositivo;
        private GetRecepcionistas _getRecepcionistas;

        public RecepcionistaController(GuardarDispositivoNotificacion guardarDispositivo, GetRecepcionistas getRecepcionistas)
        {
            _guardarDispositivo = guardarDispositivo;
            _getRecepcionistas = getRecepcionistas;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Send()
        {
            return View();
        }

        [RecepcionistaLogueado]
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
            catch (Exception)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "Algo salió mal al activar las notificaciones";
                return View("Index");
            }
        }

        [HttpPost]
        public IActionResult SendPushNotification(int Id, string Titulo, string Payload)
        {
            // acá envío las notificaciones

            ViewBag.Mensaje = "Notificación enviada correctamente";
            return View("Send");
        }
    }
}

