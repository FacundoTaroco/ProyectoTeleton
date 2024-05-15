using LogicaAplicacion;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Controllers
{
    public class AppTeleton : Controller
    {
        [ApiController]
        [Route("[controller]")]
        public class TotemController : ControllerBase
        {
            private readonly TotemService _totemService;

            public TotemController(TotemService totemService)
            {
                _totemService = totemService;
            }

            [HttpPost("registro-acceso")]
            public IActionResult RegistrarAcceso()
            {
                _totemService.RegistrarAcceso();
                return Ok("Acceso registrado exitosamente.");
            }

            [HttpPost("iniciar-sesion")]
            public IActionResult IniciarSesion()
            {
                _totemService.IniciarSesion();
                return Ok("Sesión iniciada.");
            }

            [HttpPost("finalizar-sesion/{sesionId}")]
            public IActionResult FinalizarSesion(int sesionId)
            {
                _totemService.FinalizarSesion(sesionId);
                return Ok("Sesión finalizada.");
            }
        }
    }
}
