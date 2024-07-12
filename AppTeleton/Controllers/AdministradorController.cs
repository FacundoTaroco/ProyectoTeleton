using AppTeleton.Models;
using AppTeleton.Models.Filtros;
using LogicaAccesoDatos.EF;
using LogicaAplicacion.CasosUso.AdministradorCU;
using LogicaAplicacion.CasosUso.CitaCU;
using LogicaAplicacion.CasosUso.MedicoCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.CasosUso.PreguntasFrecCU;
using LogicaAplicacion.CasosUso.RecepcionistaCU;
using LogicaAplicacion.CasosUso.TotemCU;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace AppTeleton.Controllers
{
    [AdminLogueado]
    public class AdministradorController : Controller
    {

        public GetAdministradores _getAdministradores;
        public GetRecepcionistas _getRecepcionistas;
        public GetPacientes _getPacientes;
        public GetMedicos _getMedicos;
        public ABMAdministradores _ABMAdministradores;
        public ABMRecepcionistas _ABMRecepcionistas;
        public ActualizarPacientes _actualizarPacientes;
        public GetTotems _getTotems;
        public CambiarContrasenia _cambiarContrasenia;
        public ABMTotem _ABMTotems;
        public GetCitas _getCitas;
        public IRepositorioPaciente _repositorioPaciente;
        public IRepositorioCitaMedica _repositorioCitaMedica;
        public ABMCitas _abmCitaMedica;

        public AdministradorController(GetAdministradores listaAdmins,
            GetRecepcionistas listaRecepcionistas,
            GetPacientes listaPacientes,
            GetMedicos listaMedicos,
            ABMAdministradores abmAdministradores,
            ABMRecepcionistas abmRecepcionistas,
            ActualizarPacientes actualizarPacientes,
            GetTotems getTotems,
            CambiarContrasenia cambiarContrasenia,
            ABMTotem abmtotems,
            GetCitas getCitas,
            IRepositorioPaciente repositorioPaciente,
            IRepositorioCitaMedica repositorioCitaMedica,
            ABMCitas abmCitaMedica
            )
        {
            _getAdministradores = listaAdmins;
            _getRecepcionistas = listaRecepcionistas;
            _getPacientes = listaPacientes;
            _getMedicos = listaMedicos;
            _ABMAdministradores = abmAdministradores;
            _ABMRecepcionistas = abmRecepcionistas;
            _actualizarPacientes = actualizarPacientes;
            _getTotems = getTotems;
            _cambiarContrasenia = cambiarContrasenia;
            _ABMTotems = abmtotems;
            _getCitas = getCitas;
            _repositorioPaciente = repositorioPaciente;
            _repositorioCitaMedica = repositorioCitaMedica;
            _abmCitaMedica = abmCitaMedica;
        }

        [HttpGet]
        public IActionResult Index(TipoUsuario tipoUsuario, string tipoMensaje, string mensaje)
        {
            if (!String.IsNullOrEmpty(tipoMensaje) && !String.IsNullOrEmpty(mensaje))
            {
                ViewBag.TipoMensaje = tipoMensaje;
                ViewBag.Mensaje = mensaje;
            }

            ViewBag.TipoUsuario = tipoUsuario;
            if (tipoUsuario == TipoUsuario.NoLogueado)
            {
                ViewBag.TipoUsuario = TipoUsuario.Paciente;
            }
            return View(ObtenerModeloUsuarios());
        }

        [HttpGet]
        public async Task<IActionResult> InformacionPaciente(string cedula)
        {
            var paciente = _repositorioPaciente.GetPacientePorCedula(cedula);
            if (paciente == null)
            {
                return NotFound();
            }

            var citasMedicas = _getCitas.ObtenerCitasPorCedula(cedula).Result;

            PacienteDTO pacienteDTO = new PacienteDTO
            {
                NombreCompleto = paciente.Nombre,
                Cedula = paciente.Cedula
            };

            ViewBag.CitasMedicas = citasMedicas;

            return View("InformacionPaciente", pacienteDTO);
        }

        [HttpGet]
        public async Task<IActionResult> InformacionCita(int id)
        {
            var citas = await _getCitas.ObtenerCitas();
            var cita = citas.FirstOrDefault(c => c.PkAgenda == id);

            if (cita == null)
            {
                return NotFound();
            }
            return View("InformacionCita", cita);
        }

        [HttpGet]
        public IActionResult VerTipoUsuario(TipoUsuario opcion)
        {

            if (opcion == TipoUsuario.Paciente)
            {
                ViewBag.TipoUsuario = TipoUsuario.Paciente;
            }
            else if (opcion == TipoUsuario.Recepcionista)
            {
                ViewBag.TipoUsuario = TipoUsuario.Recepcionista;
            }
            else if (opcion == TipoUsuario.Admin)
            {
                ViewBag.TipoUsuario = TipoUsuario.Admin;
            }
            else if (opcion == TipoUsuario.Medico)
            {
                ViewBag.TipoUsuario = TipoUsuario.Medico;
            }
            else if (opcion == TipoUsuario.Totem)
            {
                ViewBag.TipoUsuario = TipoUsuario.Totem;
            }

            return View("Index", ObtenerModeloUsuarios());

        }


        [HttpGet]
        public IActionResult AgregarAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AgregarAdmin(Administrador admin)
        {

            try
            {
                _ABMAdministradores.AltaAdmin(admin);
                ViewBag.TipoMensaje = "EXITO";
                ViewBag.Mensaje = "Administrador agregado con exito";
                ViewBag.TipoUsuario = TipoUsuario.Admin;
                return View("Index", ObtenerModeloUsuarios());


            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View();
            }
        }


        [HttpGet]
        public IActionResult AgregarRecepcionista()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AgregarRecepcionista(Recepcionista recepcionista)
        {

            try
            {
                _ABMRecepcionistas.AltaRecepcionista(recepcionista);
                ViewBag.TipoMensaje = "EXITO";
                ViewBag.Mensaje = "Recepcionista agregado con exito";
                ViewBag.TipoUsuario = TipoUsuario.Recepcionista;
                return View("Index", ObtenerModeloUsuarios());
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult AgregarTotem()
        {
            return View();
        }

        public IActionResult AgregarTotem(TotemDTO totem)
        {
            if (!ModelState.IsValid)
            {
                return View(totem);
            }

            try
            {
                var nuevoTotem = new Totem
                {
                    Nombre = totem.Nombre,
                    NombreUsuario = totem.NombreUsuario,
                    Contrasenia = totem.Contrasenia
                };

                _ABMTotems.AltaTotem(nuevoTotem);
                ViewBag.TipoMensaje = "EXITO";
                ViewBag.Mensaje = "Totem agregado con éxito";
                ViewBag.TipoUsuario = TipoUsuario.Totem;
                return View("Index", ObtenerModeloUsuarios());
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View(totem);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarPacientes()
        {
            try
            {

                await _actualizarPacientes.Actualizar();
                ViewBag.TipoMensaje = "EXITO";
                ViewBag.Mensaje = "Usuarios Actualizados con exito";
                ViewBag.TipoUsuario = TipoUsuario.Paciente;
                return View("Index", ObtenerModeloUsuarios());
            }
            catch (Exception e)
            {

                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                ViewBag.TipoUsuario = TipoUsuario.Paciente;
                return View("Index", ObtenerModeloUsuarios());
            }

        }

        public IActionResult EnviarNotificacionUsuario(int idUsuario, string mensaje)
        {

            if (!String.IsNullOrEmpty(mensaje))
            {
                ViewBag.Mensaje = mensaje;
            }

            ViewBag.idUsuario = idUsuario;
            return View("MandarNotificacionUsuario");

        }

        [HttpGet]
        public IActionResult CambiarContrasenia(int id)
        {
            ViewBag.IdUsuario = id;
            return View();
        }

        [HttpPost]
        public IActionResult CambiarContrasenia(int id, string nuevaContrasenia, string confirmarContrasenia)
        {
            if (nuevaContrasenia != confirmarContrasenia)
            {
                ViewBag.Mensaje = "Las contraseñas no coinciden";
                ViewBag.TipoMensaje = "ERROR";
                return View();
            }

            try
            {
                _cambiarContrasenia.ChangePassword(id, nuevaContrasenia);
                ViewBag.Mensaje = "Contraseña cambiada exitosamente";
                ViewBag.TipoMensaje = "EXITO";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = ex.Message;
                return View();
            }
            return View();
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
