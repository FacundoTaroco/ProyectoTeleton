
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
using AppTeleton.Worker;
using LogicaAplicacion.Servicios;



namespace AppTeleton.Controllers
{
    [TotemLogueado]
    //Controller que gestiona las diferentes acciones relacionadas al totem de recepcion
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
        private static readonly object _lock = new object();

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

        //vista index
        public IActionResult Index()
        {
            return View();
        }

        //Vista de Cerrar sesion
        public IActionResult CerrarSesion()
        {
            return View();
        }
        //Vista principal del totem
        public async Task<IActionResult> HomeUsuario(string cedula)
        {
            try
            {
                ViewBag.CedulaUsuario = cedula;
                Paciente paciente = _getPacientes.GetPacientePorCedula(cedula); //Se obtiene al usuario que accedio al totem
                DateTime _fecha = DateTime.UtcNow;
                TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
                DateTime fechaHoy = TimeZoneInfo.ConvertTimeFromUtc(_fecha, zonaHoraria);
                IEnumerable<CitaMedicaDTO> citas = await _getCitas.ObtenerCitasPorCedula(cedula); //se obtienen las citas del usuario
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

        //Vista de preguntas para el totem de recepcion
        public IActionResult PreguntasParaTotem(string cedula)
        {
            try
            {
                ViewBag.CedulaUsuario = cedula;
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

        //Carga la vista del mapa del totem
        public IActionResult Mapa(string cedula)
        {
            try
            {
                ViewBag.CedulaUsuario = cedula;
               
              
                return View("MapaTotem");
            }
            catch (Exception e)
            {
                ViewBag.Mensaje = e.Message;
                ViewBag.TipoMensaje = "ERROR";
                return View("MapaTotem");
            }
        }

        //cerrar sesion del totem, funciona como un login ya que se deben ingresar las credenciales nuevamente
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
                
        //Accion que gestiona el acceso al totem
        public async Task<IActionResult> Acceder(string cedula) {
            try
            {
                //Limpiamos la cedula
                cedula = cedula.Replace(" ", "")
                              .Replace(".", "")
                              .Replace("-", "");

                ViewBag.CedulaUsuario = cedula;
                Totem totem = GetTotemLogueado(); //Se obtiene el totem
                Paciente paciente = _getPacientes.GetPacientePorCedula(cedula);//Se obtiene al paciente
                AccesoTotem nuevoAcceso = new AccesoTotem(cedula, totem); //generamos un nuevo acceso
         
                IEnumerable<CitaMedicaDTO> citas = new List<CitaMedicaDTO>();
                IEnumerable<CitaMedicaDTO> citasDeHoy = new List<CitaMedicaDTO>();


                DateTime _fecha = DateTime.UtcNow;
                TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
                DateTime fechaHoy = TimeZoneInfo.ConvertTimeFromUtc(_fecha, zonaHoraria); //formatear la fecha al UTC correcto


                if (!_acceso.PacienteYaAccedioEnFecha(totem.Id, fechaHoy, cedula)) //Verifica si el paciente ya accedio al totem en el dia
                {
                    _acceso.AgregarAcceso(nuevoAcceso); //Si no accedio, guarda el acceso
                    try
                    {
                        citas = await _getCitas.ObtenerCitasPorCedula(cedula); //Se obtienen las citas del paciente para el dia actual
                        citasDeHoy = citas.Where(c => c.Cedula == cedula && (c.Fecha.Day == nuevoAcceso.FechaHora.Day && c.Fecha.Month == nuevoAcceso.FechaHora.Month && c.Fecha.Year == nuevoAcceso.FechaHora.Year)).OrderBy(c => c.HoraInicio).ToList();
                        foreach (var cita in citasDeHoy)
                        {
                            //Llama al servicio para modificar el estado de las citas directamente en el servidor central de teleton ya que el paciente se encuentra en el centro
                            _generarAvisoLlegada.GenerarAvisoLLamada(cita.PkAgenda);
                            cita.Estado = "RCP";
                        }
                    }
                    catch (TeletonServerException)
                    { //SI EL servidor central NO se encuentra disponible 
                        AccesosFallidos.accesosFallidos.Add(nuevoAcceso); //Guardamos el acceso como un acceso fallido
                        if (!AccesosFallidos.servicioDeReintentoActivado) {
                            AccesosFallidos.servicioDeReintentoActivado = true; 
                            AccesosFallidosService.IniciarServicioDeReintento(_getCitas, _generarAvisoLlegada);//Activamos el servicio de reintento de comunicacion con el servidor central para evitar que se pierdan los accesos
                        }
                        ViewBag.TipoMensaje = "ERROR";
                        ViewBag.Mensaje = "No se pudieron cargar sus citas, consulte en recepción";  
                        AccesoTotemViewModel model = new AccesoTotemViewModel(paciente);
                        return View("HomeUsuario", model);
                    }

                    _actualizarListadosHub.Clients.All.SendAsync("ActualizarListado", citasDeHoy); //Se actualiza el listado de recepcionistas en tiempo real
                    ActualizarListadoMedicos();//Se actualiza el listado de medicos en tiempo real
                }
                else { //Si el paciente ya habia accedido se le cargan sus citas sin realizar toda la logica anterior ya que ya fue realizada previamente en el dia
                    citas = await _getCitas.ObtenerCitasPorCedula(cedula);
                    citasDeHoy = citas.Where(c => c.Cedula == cedula && (c.Fecha.Day == nuevoAcceso.FechaHora.Day && c.Fecha.Month == nuevoAcceso.FechaHora.Month && c.Fecha.Year == nuevoAcceso.FechaHora.Year)).OrderBy(c => c.HoraInicio).ToList();

                }



                AccesoTotemViewModel accesoTotemViewModel = new AccesoTotemViewModel(citasDeHoy, paciente);
               
                return View("HomeUsuario", accesoTotemViewModel);
            }
       
            catch (Exception e)
            {
            ViewBag.TipoMensaje = "ERROR";
            ViewBag.Mensaje = e.Message;
            return View("Index");  
            }
            }


        public async void ActualizarListadoMedicos() {

            //actualiza los listados de los medicos en tiempo real

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

