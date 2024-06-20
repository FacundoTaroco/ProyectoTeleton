
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
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using LogicaNegocio.Enums;
using AppTeleton.Models.Filtros;


namespace AppTeleton.Controllers
{
    [TotemLogueado]
    public class TotemController : Controller
    {


        private GetPacientes _getPacientes;
        private GetTotems _getTotems;
        private AccesoCU _acceso;
        private GenerarAvisoLlegada _generarAvisoLlegada;
        private GetCitas _getCitas;
        private ILogin _login;
        public TotemController(GetPacientes getPacientes, AccesoCU acceso, GetTotems getTotems, GenerarAvisoLlegada generarAvisoLLegada,GetCitas getCitas,ILogin login)
        {
            _getPacientes = getPacientes;
            _acceso = acceso;
            _getTotems = getTotems;
            _generarAvisoLlegada = generarAvisoLLegada;
            _getCitas = getCitas;
             _login = login;
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
               
                
                
                Totem totem = GetTotemLogueado();

                Paciente paciente = _getPacientes.GetPacientePorCedula(cedula);
                AccesoTotem nuevoAcceso = new AccesoTotem(cedula, totem);
                //VER QUE HACER CON AVISO MEDICO AHORA QUE NO TENEMOS MAS API
                AvisoMedicoDTO avisoMedico = new AvisoMedicoDTO(cedula,"Recepcionado",nuevoAcceso.FechaHora);
                if (!_acceso.PacienteYaAccedioEnFecha(totem.Id, DateTime.Now, cedula))
                {
                _acceso.AgregarAcceso(nuevoAcceso); 
                    
                    _generarAvisoLlegada.GenerarAvisoLLamada(avisoMedico);
                }
               
                //OBTENER CITAS DE HOY ACA
                IEnumerable<CitaMedicaDTO> citas = await _getCitas.ObtenerCitasPorCedula(cedula);
                AccesoTotemViewModel accesoTotemViewModel = new AccesoTotemViewModel(citas, paciente);
               
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
