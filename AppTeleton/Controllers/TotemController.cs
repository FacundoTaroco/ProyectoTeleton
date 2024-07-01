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
using LogicaAplicacion.Servicios;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.Data.SqlClient;
using LogicaAplicacion.CasosUso.AdministradorCU;
using LogicaAplicacion.CasosUso.MedicoCU;
using LogicaAplicacion.CasosUso.RecepcionistaCU;


namespace AppTeleton.Controllers
{
    [TotemLogueado]
    public class TotemController : Controller
    {
        public GetPacientes _getPacientes;
        public GetTotems _getTotems;
        public AccesoCU _acceso;
        public GenerarAvisoLlegada _generarAvisoLlegada;
        public SolicitarCitasService _solicitarCitasService;
        public IRepositorioCitaMedica _repositorioCitaMedica;
        public GetCitas _getCitas;
        public ILogin _login;
        public ILogger<RecepcionistaController> _logger;
        public ABMTotem _ABMTotems;
        public GetRecepcionistas _getRecepcionistas;
        public GetAdministradores _getAdministradores;
        public GetMedicos _getMedicos;

        public TotemController(
            SolicitarCitasService solicitarCitasService,
            IRepositorioCitaMedica repositorioCitaMedica,
            GetPacientes getPacientes,
            AccesoCU acceso,
            GetTotems getTotems,
            GenerarAvisoLlegada generarAvisoLLegada,
            GetCitas getCitas,
            ILogin login,
            ILogger<RecepcionistaController> logger,
            ABMTotem abmtotem)
        {
            _repositorioCitaMedica = repositorioCitaMedica;
            _solicitarCitasService = solicitarCitasService;
            _getPacientes = getPacientes;
            _acceso = acceso;
            _getTotems = getTotems;
            _generarAvisoLlegada = generarAvisoLLegada;
            _getCitas = getCitas;
            _login = login;
            _logger = logger;
            _ABMTotems = abmtotem;
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
                if (String.IsNullOrEmpty(NombreUsuario) || String.IsNullOrEmpty(Contrasenia))
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



        /*public async Task<IActionResult> Acceder(string cedula)
        {
            try
            {
                //ACA FALTA VALIDAR QUE SI LA PERSONA YA ACCEDIO ESE DIA NO VUELVA A GENERAR UN NUEVO ACCESSSOOOO

                Totem totem = GetTotemLogueado();

                Paciente paciente = _getPacientes.GetPacientePorCedula(cedula);
                AccesoTotem nuevoAcceso = new AccesoTotem(cedula, totem);
                //VER QUE HACER CON AVISO MEDICO AHORA QUE NO TENEMOS MAS API
                AvisoMedicoDTO avisoMedico = new AvisoMedicoDTO(cedula, "Recepcionado", nuevoAcceso.FechaHora);
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
                return View("HomeUsuario", accesoTotemViewModel);
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View("Index");
            }
        }*/
        [HttpPost]
        public async Task<IActionResult> Acceder(string cedula)
        {
            try
            {
                // OBTENER CITAS POR CÉDULA Y FECHA
                DateTime fecha = DateTime.Now.Date;
                IEnumerable<CitaMedicaDTO> citas = await _repositorioCitaMedica.ObtenerCitasPorCedulaYFecha(cedula, fecha);

                // LÓGICA ADICIONAL
                Totem totem = GetTotemLogueado();
                Paciente paciente = _getPacientes.GetPacientePorCedula(cedula);
                AccesoTotem nuevoAcceso = new AccesoTotem(cedula, totem);
                AvisoMedicoDTO avisoMedico = new AvisoMedicoDTO(cedula, "Recepcionado", nuevoAcceso.FechaHora);

                if (!_acceso.PacienteYaAccedioEnFecha(totem.Id, DateTime.Now, cedula))
                {
                    _acceso.AgregarAcceso(nuevoAcceso);
                    _generarAvisoLlegada.GenerarAvisoLLamada(avisoMedico);
                }

                AccesoTotemViewModel accesoTotemViewModel = new AccesoTotemViewModel(citas, paciente);
                return View("HomeUsuario", accesoTotemViewModel);
            }
            catch (TeletonServerException)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "No se pudieron cargar sus citas, consulte en recepcion";
                Paciente paciente = _getPacientes.GetPacientePorCedula(cedula);
                AccesoTotemViewModel accesoTotemViewModel = new AccesoTotemViewModel(paciente);
                return View("HomeUsuario", accesoTotemViewModel);
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View("Index");
            }
        }


        public IActionResult CerrarAcceso()
        {

            return View("Index");

        }

        public IActionResult Accesos()
        {

            Totem _Totem = GetTotemLogueado();
            IEnumerable<AccesoTotem> accesos = _acceso.GetAccesos(_Totem.Id);
            return View(accesos);
        }

        public Totem GetTotemLogueado()
        {
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

        private UsuariosViewModel ObtenerModeloUsuarios()
        {
            IEnumerable<Paciente> pacientes = _getPacientes.GetAll();
            IEnumerable<Recepcionista> recepcionistas = _getRecepcionistas.GetAll();
            IEnumerable<Administrador> admins = _getAdministradores.GetAll();
            IEnumerable<Medico> medicos = _getMedicos.GetAll();
            IEnumerable<Totem> totems = _getTotems.GetAll();
            UsuariosViewModel modeloIndex = new UsuariosViewModel(pacientes, admins, recepcionistas, medicos, totems);
            return modeloIndex;
        }


    }
}