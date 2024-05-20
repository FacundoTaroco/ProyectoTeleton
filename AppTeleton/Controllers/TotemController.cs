using LogicaAplicacion.CasosUso.AccesoTotemCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.CasosUso.SesionTotemCU;
using LogicaAplicacion.CasosUso.TotemCU;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AppTeleton.Controllers
{
    public class TotemController : Controller
    {

        private GetPacientes _getPacientes;
        private GetTotems _getTotems;
        private Totem _Totem;
        private AccesoCU _acceso;
        private GetSesionTotem _sesionTotem;

        public TotemController(GetPacientes getPacientes, AccesoCU acceso, GetTotems getTotems, GetSesionTotem sesionTotem)
        {
            _getPacientes = getPacientes;
            _acceso = acceso;
            _getTotems = getTotems;
            _sesionTotem =sesionTotem;
            //Esto se rompe si no se inicio sesion en el totem antes

            _Totem = _getTotems.GetTotemPorUsr("totemMVD"); // VER COMO HACER ACA PARA QUE SEA GENERICO OSEA QUE LE LLEGUE EL NOMBRE DEL TOTEM
        }

       
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Acceder(string cedula) {
            try
            {
                int idSesionActiva = (int)HttpContext.Session.GetInt32("SESIONTOTEM");
                //VALIDAT NULL
                SesionTotem sesionActiva = _sesionTotem.GetSesionPorId(idSesionActiva);

                Paciente paciente = _getPacientes.GetPacientePorCedula(cedula);
                AccesoTotem nuevoAcceso = new AccesoTotem(cedula, sesionActiva);
                _acceso.AgregarAcceso(nuevoAcceso); 

                return View(paciente);
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View();  
            }
        }


        public IActionResult Sesiones() {
            IEnumerable<SesionTotem> sesiones = _sesionTotem.GetSesiones(_Totem.Id);
            return View(sesiones);
        }

        public IActionResult Accesos()
        {
            IEnumerable<AccesoTotem> accesos = _acceso.GetAccesos(_Totem.Id);
            return View(accesos);
        }
    }
}
