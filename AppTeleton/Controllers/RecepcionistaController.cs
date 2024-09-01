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




        







    }
}

