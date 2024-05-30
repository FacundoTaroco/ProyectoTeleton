using LogicaAccesoDatos.EF.Excepciones;
using LogicaAplicacion.Excepciones;
using LogicaAplicacion.Servicios;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.PacienteCU  
{
    //Esta clase es la encargada de recibir a la lista de pacientes obtenida del servidor central de la teleton(o del endpoint de testing)
    //y validar si la lista de la aplicacion esta actualizada, si no lo esta se encarga de mapear o delegar el mapeo de un paciente de la teleton
    //a un usuario Paciente de la aplicacion para luego guardarlo en el contexto
    public class ActualizarPacientes
    {
        private GetPacientes _getPacientesCU;
        private ABMPacientes _abmPacientes;
        private SolicitarPacientesService _solicitarPacientesTeleton;
        public ActualizarPacientes(SolicitarPacientesService servicioPacientes, GetPacientes getPacientes, ABMPacientes abmPacientes)
        {
            _getPacientesCU = getPacientes;
            _solicitarPacientesTeleton = servicioPacientes;
            _abmPacientes = abmPacientes;
        }



       
        public async Task<bool> Actualizar() {
            try
            {
                IEnumerable<PacienteDTO> pacientesObtenidos = await _solicitarPacientesTeleton.solicitarPacientesATeleton();
                pacientesObtenidos = limpiezaListaPacientes(pacientesObtenidos);
                foreach (PacienteDTO p in pacientesObtenidos)
                {
                    if (!existePaciente(p.Cedula))
                    {
                        crearNuevoUsuarioPaciente(p);
                    }
                    //Ver que hacer si se borran pacientes?¿?¿
                }

                return true;

            }
            catch (TeletonServerException) {
                throw;
            }
            catch (Exception)
            {

                throw;
            }
            
        
        
        }

        public void crearNuevoUsuarioPaciente(PacienteDTO paciente) { 
     
        
            Paciente nuevoUsuarioPaciente = new Paciente(crearNombreUsuario(paciente.NombreCompleto,paciente.Cedula), paciente.Cedula, paciente.NombreCompleto, paciente.Cedula);
            _abmPacientes.AltaPaciente(nuevoUsuarioPaciente);
            

            
            //VALIDAR QUE PASA SI NOS LLEGAN PACIENTES VACIOS
        }



        public string crearNombreUsuario(string nombreCompleto,string cedula) {
            string[] partesNombre = nombreCompleto.Split(' ');
            string inicialNombre = partesNombre[0].Substring(0, 1);
            string apellido = "";
            if(partesNombre.Length > 2) {
                 apellido = partesNombre[partesNombre.Length-1].Replace(" ", "");
            }
            else
            {
                 apellido = partesNombre[1].Replace(" ", "");
            }
            string digitoFinalCedula = cedula.Substring(cedula.Length-1);
            string dosDigitosFinalCedula = cedula.Substring(cedula.Length - 2);
            string nombreUsuario = inicialNombre + apellido + digitoFinalCedula;

            try
            {
                _getPacientesCU.GetPacientePorUsuario(nombreUsuario);
                //si llega aca es porque si existe el usuario entonces tenemos que agregarle otro digito de la cedula
                nombreUsuario = inicialNombre + apellido + dosDigitosFinalCedula;
                return nombreUsuario;

            }
            catch (NotFoundException)
            {
                //si llego aca es porque no existe otro paciente con ese nombre de usuario
                return nombreUsuario;
            }

        }


        public bool existePaciente(string cedula) {
            try
            {
              _getPacientesCU.GetPacientePorCedula(cedula);
               return true;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        public IEnumerable<PacienteDTO> limpiezaListaPacientes(IEnumerable<PacienteDTO> pacientesALimpiar) { 

            //VER QUE HACER SI NOS LLEGAN DATOS VACIOS!!!!!!!
            //En este metodo se hace una limpieza de los datos recibidos del servidor central de la teleton(sacarle el guion y punto a las cedulas, etc)
            List<PacienteDTO> listaPacienteLimpia = new List<PacienteDTO>();
            foreach (PacienteDTO p in pacientesALimpiar) { 
                PacienteDTO pacienteLimpio = new PacienteDTO();
  
                pacienteLimpio.NombreCompleto = limpiarNombre(p.NombreCompleto);
                pacienteLimpio.Cedula = limpiarCedula(p.Cedula);

                listaPacienteLimpia.Add(pacienteLimpio);
            }
            
            return listaPacienteLimpia;
        }

        //Estos metodos se tienen que mejorar segun como vengan los datos del servidor de la teleton
        private string limpiarCedula(string cedulaSucia) { 
            string patron = @"\D+";
            string cedulaLimpia = Regex.Replace(cedulaSucia, patron, "");
            return cedulaLimpia;
        }
       
        private string limpiarNombre(string nombreSucio) { 

            //
            return nombreSucio.ToLower();
        }
    }
}
