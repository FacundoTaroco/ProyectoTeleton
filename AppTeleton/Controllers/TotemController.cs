
using AppTeleton.Models;
using LogicaNegocio.InterfacesDominio;
using LogicaAplicacion.CasosUso.AccesoTotemCU;
using LogicaAplicacion.CasosUso.CitaCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.CasosUso.SesionTotemCU;
using LogicaAplicacion.CasosUso.TotemCU;
using LogicaAplicacion.Excepciones;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
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
        private GenerarAvisoLlegada _generarAvisoLlegada;
        private GetCitas _getCitas;
        private ILogin _login;
        public TotemController(GetPacientes getPacientes, AccesoCU acceso, GetTotems getTotems, GetSesionTotem sesionTotem, GenerarAvisoLlegada generarAvisoLLegada,GetCitas getCitas,ILogin login)
        {
            _getPacientes = getPacientes;
            _acceso = acceso;
            _getTotems = getTotems;
            _sesionTotem =sesionTotem;
            _generarAvisoLlegada = generarAvisoLLegada;
            _getCitas = getCitas;
             _login = login;
            //Esto se rompe si no se inicio sesion en el totem antes

            _Totem = _getTotems.GetTotemPorUsr("totemMVD"); // VER COMO HACER ACA PARA QUE SEA GENERICO OSEA QUE LE LLEGUE EL NOMBRE DEL TOTEM
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult CerrarSesion()
        {
            return View();
        }
        public IActionResult HomeUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CerrarSesion(string NombreUsuario, string Contrasenia)
        {
            try
            {
                // Validar las credenciales del usuario
                string tipoUsuario = _login.LoginCaso(NombreUsuario, Contrasenia);

                // Si la validación es correcta y el usuario es un totem, redirige al Logout de UsuarioController
                if (tipoUsuario == "TOTEM")
                {
                    return RedirectToAction("Logout", "Usuario");
                }

                // Si no es un usuario totem, mostrar mensaje de error
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "Usuario o contraseña incorrectos para cerrar sesión del totem.";
                return View();
                 }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View();
            }
            
            }
                
        

        public async Task<IActionResult> Acceder(string cedula) {
            try
            {
                int idSesionActiva = (int)HttpContext.Session.GetInt32("SESIONTOTEM");
                //VALIDAT NULL
                SesionTotem sesionActiva = _sesionTotem.GetSesionPorId(idSesionActiva);

                Paciente paciente = _getPacientes.GetPacientePorCedula(cedula);
                AccesoTotem nuevoAcceso = new AccesoTotem(cedula, sesionActiva);
                _acceso.AgregarAcceso(nuevoAcceso); 
                AvisoMedicoDTO avisoMedico = new AvisoMedicoDTO(cedula,"Recepcionado",nuevoAcceso.FechaHora);
                _generarAvisoLlegada.GenerarAvisoLLamada(avisoMedico);
                IEnumerable<CitaMedicaDTO> citas = await _getCitas.ObtenerCitasPorCedula(cedula);




                AccesoTotemViewModel accesoTotemViewModel = new AccesoTotemViewModel(citas, paciente);
                return View(accesoTotemViewModel);
            }
            catch (ApiErrorException)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "No se pudieron cargar sus citas, consulte en recepcion";
                Paciente paciente = _getPacientes.GetPacientePorCedula(cedula);
                AccesoTotemViewModel accesoTotemViewModel = new AccesoTotemViewModel(paciente);
                return View(accesoTotemViewModel);
              }
            catch (Exception e)
            {
            ViewBag.TipoMensaje = "ERROR";
            ViewBag.Mensaje = e.Message;
            return View("Index");  
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
