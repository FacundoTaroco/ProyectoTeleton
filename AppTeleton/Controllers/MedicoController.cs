using AppTeleton.Hubs;
using AppTeleton.Models;
using AppTeleton.Models.Filtros;
using LogicaAccesoDatos.EF.Excepciones;
using LogicaAplicacion.CasosUso.CitaCU;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AppTeleton.Controllers
{
   //Controller que gestiona las acciones de los usuarios de tipo medico
    public class MedicoController : Controller
    {
        private GetCitas _getCitas;
        private IHubContext<PantallaLLamadosHub> _pantallaLlamados;
        public MedicoController(IHubContext<PantallaLLamadosHub> pantallaLLamados,GetCitas getCitas) { 
            
            _getCitas = getCitas;
            _pantallaLlamados = pantallaLLamados;
       
        }
        //Accion principal
        [MedicoLogueado]
        public async Task<IActionResult> Index()
        {
            try
            {
                MedicoViewModel model = await ObtenerModeloMedicos();
                return View(model);

            }
            catch (Exception e)
            {
                MedicoViewModel model = new MedicoViewModel();
                model.Citas = new List<CitaMedicaDTO>();
                ViewBag.Mensaje = e.Message;
                ViewBag.TipoMensaje = "ERROR";
                return View(model);
            }
          
        }


        //realizar llamados a consultorios
        [HttpPost]
        public async Task<IActionResult> RealizarLLamado(string nombre, string cedula, string consultorio) {
            try
            {

                if (String.IsNullOrEmpty(consultorio)) {
                    throw new NullOrEmptyException("Ingrese el nombre/numero del consultorio");
                }
              

                LLamado nuevoLLamado = new LLamado(nombre,cedula,consultorio);
                _pantallaLlamados.Clients.All.SendAsync("NuevoLlamado", nuevoLLamado);
                MedicoViewModel model = await ObtenerModeloMedicos();
                return View("Index",model);
            }
            catch (NullOrEmptyException e)
            {

                MedicoViewModel model = await ObtenerModeloMedicos();
                ViewBag.Mensaje = e.Message;
                ViewBag.TipoMensaje = "ERROR";
                return View("Index", model);
            }
            catch (Exception e)
            {

                MedicoViewModel model = new MedicoViewModel();
                model.Citas = new List<CitaMedicaDTO>();
                ViewBag.Mensaje = e.Message;
                ViewBag.TipoMensaje = "ERROR";
                return View("Index",model);
            }
        }




        public async Task <MedicoViewModel> ObtenerModeloMedicos() {


            DateTime _fecha = DateTime.UtcNow;
            TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            DateTime fechaHoy = TimeZoneInfo.ConvertTimeFromUtc(_fecha, zonaHoraria);
            IEnumerable<CitaMedicaDTO> citas = await _getCitas.ObtenerCitas();
            IEnumerable<CitaMedicaDTO> citasDeHoy = citas.Where(c => (c.Fecha.Day == fechaHoy.Day && c.Fecha.Month == fechaHoy.Month && c.Fecha.Year == fechaHoy.Year) && c.Estado.Equals("RCP")).OrderBy(c => c.HoraInicio).ToList();
            MedicoViewModel model = new MedicoViewModel();
            model.Citas = citasDeHoy;
            return model;   

        }
    }
}
