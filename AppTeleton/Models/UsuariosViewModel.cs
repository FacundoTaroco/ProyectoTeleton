using LogicaNegocio.Entidades;

namespace AppTeleton.Models
{//clase para poder mostrar todos los tipos de pacientes en una sola vista
    public class UsuariosViewModel
    {


        public IEnumerable<Paciente> Pacientes;
        public IEnumerable<Administrador> Administrador;
        public IEnumerable<Recepcionista> Recepcionistas;
        public IEnumerable<Medico> Medicos;
        public IEnumerable<Totem> Totems;
        //public IEnumerable<Cita> Citas;

        public UsuariosViewModel() { }
        public UsuariosViewModel(IEnumerable<Paciente> pacientes, IEnumerable<Administrador> administradores, IEnumerable<Recepcionista> recepcionistas, IEnumerable<Medico> medicos, IEnumerable<Totem> totems) { 
            Pacientes = pacientes;
            Administrador = administradores;
            Recepcionistas = recepcionistas;
            Medicos = medicos;
            Totems = totems;
          
        }
    }
}
