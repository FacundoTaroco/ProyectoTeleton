using AppTeleton.Models.Filtros;
using LogicaAplicacion.CasosUso.DispositivoUsuarioCU;
using LogicaAplicacion.CasosUso.RecepcionistaCU;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Controllers
{
    [RecepcionistaLogueado]
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


    }
}

