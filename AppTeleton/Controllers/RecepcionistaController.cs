using AppTeleton.Hubs;
using AppTeleton.Models;
using AppTeleton.Models.Filtros;

using LogicaAplicacion.CasosUso.CitaCU;
using LogicaAplicacion.CasosUso.DispositivoUsuarioCU;
using LogicaAplicacion.CasosUso.RecepcionistaCU;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

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
        private GetCitas _getCitas;
        

        public RecepcionistaController(GuardarDispositivoNotificacion guardarDispositivo, GetRecepcionistas getRecepcionistas, GetCitas getCitas)
        {
            _guardarDispositivo = guardarDispositivo;
            _getRecepcionistas = getRecepcionistas;
            _getCitas = getCitas;
            
        }

        public async Task<IActionResult> Index()
        {
            DateTime _fechaHoy = DateTime.UtcNow;
            TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            DateTime hoyGMT = TimeZoneInfo.ConvertTimeFromUtc(_fechaHoy, zonaHoraria);


            IEnumerable<CitaMedicaDTO> citas = await _getCitas.ObtenerCitas();
            IEnumerable<CitaMedicaDTO> citasDeHoy = citas.Where(c=>c.Fecha.Day == hoyGMT.Day && c.Fecha.Month == hoyGMT.Month && c.Fecha.Year == hoyGMT.Year).ToList();
            RecepcionistaIndexViewModel model = new RecepcionistaIndexViewModel(citasDeHoy);

            return View(model);
        }

       

        
    }
}

