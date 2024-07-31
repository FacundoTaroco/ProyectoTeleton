
using AppTeleton.Models;
using LogicaNegocio.InterfacesDominio;
using LogicaAplicacion.CasosUso.AccesoTotemCU;
using LogicaAplicacion.CasosUso.CitaCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.CasosUso.TotemCU;
using LogicaAplicacion.Excepciones;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using LogicaNegocio.Enums;
using AppTeleton.Models.Filtros;
using AppTeleton.Hubs;
using Microsoft.AspNetCore.SignalR;
using LogicaAplicacion.CasosUso.PreguntasFrecCU;


namespace AppTeleton.Controllers
{
    [TotemLogueado]
    public class TotemController : Controller
    {

        private GetPreguntasFrec _getPreguntasFrec;
        private GetPacientes _getPacientes;
        private GetTotems _getTotems;
        private AccesoCU _acceso;
        private GenerarAvisoLlegada _generarAvisoLlegada;
        private GetCitas _getCitas;
        private ILogin _login;
        private IHubContext<ActualizarListadoHub> _actualizarListadosHub;
        private IHubContext<ListadoParaMedicosHub> _listadoMedicosHub;

        public TotemController(GetPreguntasFrec getPreguntas,IHubContext<ListadoParaMedicosHub> listadoMedicosHub, IHubContext<ActualizarListadoHub> listadoHub, GetPacientes getPacientes, AccesoCU acceso, GetTotems getTotems, GenerarAvisoLlegada generarAvisoLLegada,GetCitas getCitas,ILogin login)
        {
            _getPacientes = getPacientes;
            _acceso = acceso;
            _getTotems = getTotems;
            _generarAvisoLlegada = generarAvisoLLegada;
            _getCitas = getCitas;
            _login = login;
            _actualizarListadosHub = listadoHub;
            _listadoMedicosHub = listadoMedicosHub;
            _getPreguntasFrec = getPreguntas;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult CerrarSesion()
        {
            return View();
        }
        public async Task<IActionResult> HomeUsuario(string cedula)
        {
            try
            {
                Paciente paciente = _getPacientes.GetPacientePorCedula(cedula);
                DateTime _fecha = DateTime.UtcNow;
                TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
                DateTime fechaHoy = TimeZoneInfo.ConvertTimeFromUtc(_fecha, zonaHoraria);
                IEnumerable<CitaMedicaDTO> citas = await _getCitas.ObtenerCitasPorCedula(cedula);
                IEnumerable<CitaMedicaDTO> citasDeHoy = citas.Where(c => c.Cedula == cedula && (c.Fecha.Day == fechaHoy.Day && c.Fecha.Month == fechaHoy.Month && c.Fecha.Year == fechaHoy.Year)).OrderBy(c => c.HoraInicio).ToList();
                AccesoTotemViewModel accesoTotemViewModel = new AccesoTotemViewModel(citasDeHoy, paciente);
                return View(accesoTotemViewModel);
            }
            catch (Exception e)
            {

                ViewBag.Mensaje = e.Message;
                ViewBag.TipoMensaje = "ERROR";
                return View("Index");
            }
        
        }


        public IActionResult PreguntasParaTotem()
        {
            try
            {
                IEnumerable<PreguntaFrec> preguntasParaTotem = new List<PreguntaFrec>();
                preguntasParaTotem = _getPreguntasFrec.GetPreguntasParaTotem();
                return View("PreguntasTotem",preguntasParaTotem);
            }
            catch (Exception e)
            {
                ViewBag.Mensaje = e.Message;
                ViewBag.TipoMensaje = "ERROR";
                return View("Index");
            }
        }

        [HttpPost]
        public IActionResult CerrarSesion(string NombreUsuario, string Contrasenia)
        {
            try
            {
                if(String.IsNullOrEmpty(NombreUsuario) || String.IsNullOrEmpty(Contrasenia))
                {
                    throw new Exception("Ingrese todos los campos");
                }
                if (!NombreUsuario.Equals(GetTotemLogueado().NombreUsuario))
                {
                    throw new Exception("Ingrese las credenciales del totem");
                }
                // Validar las credenciales del usuario
                TipoUsuario tipoUsuario = _login.LoginCaso(NombreUsuario, Contrasenia);

                // Si la validación es correcta y el usuario es un totem, redirige al Logout de UsuarioController
                if (tipoUsuario == TipoUsuario.Totem)
                {
                    return RedirectToAction("Logout", "Usuario");
                }
                // Si no es un usuario totem, mostrar mensaje de error
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "Usuario o contraseña incorrectos para cerrar sesión del totem";
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


                ViewBag.CedulaUsuario = cedula;
                Totem totem = GetTotemLogueado();

                Paciente paciente = _getPacientes.GetPacientePorCedula(cedula);
                AccesoTotem nuevoAcceso = new AccesoTotem(cedula, totem);
         
                IEnumerable<CitaMedicaDTO> citas = await _getCitas.ObtenerCitasPorCedula(cedula);
                IEnumerable<CitaMedicaDTO> citasDeHoy = citas.Where(c => c.Cedula == cedula && (c.Fecha.Day == nuevoAcceso.FechaHora.Day && c.Fecha.Month == nuevoAcceso.FechaHora.Month && c.Fecha.Year == nuevoAcceso.FechaHora.Year)).OrderBy(c => c.HoraInicio).ToList(); 

                if (!_acceso.PacienteYaAccedioEnFecha(totem.Id, DateTime.Now, cedula))
                {
                     _acceso.AgregarAcceso(nuevoAcceso);
                    foreach (var cita in citasDeHoy) { 
                        _generarAvisoLlegada.GenerarAvisoLLamada(cita.PkAgenda);
                        cita.Estado = "RCP";
                    }


                     _actualizarListadosHub.Clients.All.SendAsync("ActualizarListado", citasDeHoy);

                    ActualizarListadoMedicos();



                }
                
              
            
                AccesoTotemViewModel accesoTotemViewModel = new AccesoTotemViewModel(citasDeHoy, paciente);
               
                return View("HomeUsuario", accesoTotemViewModel);
            }
            catch (TeletonServerException)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "No se pudieron cargar sus citas, consulte en recepcion";
                Paciente paciente = _getPacientes.GetPacientePorCedula(cedula);
                AccesoTotemViewModel accesoTotemViewModel = new AccesoTotemViewModel(paciente);
                return View("HomeUsuario",accesoTotemViewModel);
            }
            catch (Exception e)
            {
            ViewBag.TipoMensaje = "ERROR";
            ViewBag.Mensaje = e.Message;
            return View("Index");  
            }
            }


        public async void ActualizarListadoMedicos() {

            DateTime _fecha = DateTime.UtcNow;
            TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            DateTime fechaHoy = TimeZoneInfo.ConvertTimeFromUtc(_fecha, zonaHoraria);
            IEnumerable<CitaMedicaDTO> citas = await _getCitas.ObtenerCitas();
            IEnumerable<CitaMedicaDTO> citasDeHoy = citas.Where(c => (c.Fecha.Day == fechaHoy.Day && c.Fecha.Month == fechaHoy.Month && c.Fecha.Year == fechaHoy.Year) && c.Estado.Equals("RCP")).OrderBy(c => c.HoraInicio).ToList();


            _listadoMedicosHub.Clients.All.SendAsync("ActualizarListado", citasDeHoy);
        }


        public IActionResult CerrarAcceso() { 
            
            return View("Index");
        
        }
    
        public IActionResult Accesos()
        {
            
            Totem _Totem = GetTotemLogueado();
            IEnumerable<AccesoTotem> accesos = _acceso.GetAccesos(_Totem.Id);
            return View(accesos);
        }

        public Totem GetTotemLogueado() {
            try
            {
            string usuarioTotemLogueado = HttpContext.Session.GetString("USR");
            Totem totem = _getTotems.GetTotemPorUsr(usuarioTotemLogueado);
                return totem;
            }
            catch (Exception)
            {

                return null;
            }
            

        }

       
    }
}
