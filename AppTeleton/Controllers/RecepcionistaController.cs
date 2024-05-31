using AppTeleton.Models.Filtros;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.CasosUso.RecepcionistaCU;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Controllers
{
    [RecepcionistaLogueado]
    public class RecepcionistaController : Controller
    {

        private GuardarDispositivoNotificacion _guardarDispositivo;
        private GetRecepcionistas _getRecepcionistas;
        public RecepcionistaController(GuardarDispositivoNotificacion guardarDispositivo, GetRecepcionistas getRecepcionistas) {
            _guardarDispositivo = guardarDispositivo;
            _getRecepcionistas = getRecepcionistas;
        }    
        public IActionResult Index()
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

                DispositivoNotificacion dispositivo = new DispositivoNotificacion();
                dispositivo.Auth = pushAuth;
                dispositivo.P256dh = pushP256DH;
                dispositivo.Endpoint = pushEndpoint;
                dispositivo.Usuario = recepcionistaLogueado;
                dispositivo.IdUsuario = recepcionistaLogueado.Id;
                _guardarDispositivo.GuardarDispositivo(dispositivo);


                return View("Index");

            }
            catch (Exception)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "Algo salio mal al activar las notificaciones";
                return View("Index");
            }


        }

    }
}

