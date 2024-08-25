using LogicaAccesoDatos.EF.Excepciones;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioPaciente : IRepositorioPaciente
    {
        private LibreriaContext _context;
        public RepositorioPaciente(LibreriaContext context)
        {
            _context = context;
        }

        public void Add(Paciente obj)
        {
           
            try {
                if (obj == null) { throw new NullOrEmptyException("No se recibio el usuario"); }
                obj.Validar();
                obj.Id = 0;
                ValidarUnique(obj);
                _context.Pacientes.Add(obj);
                _context.SaveChanges();

            }
            catch (NullOrEmptyException)
            {
                throw;
            }
            catch (UsuarioException)
            {
                throw;
            }
            catch (UniqueException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ServerErrorException("error del servidor al agregar un nuevo paciente");
            }
        }
        public void ValidarUnique(Paciente obj)
        {
            try
            {
                foreach (Usuario a in _context.Usuarios.ToList())
                {

                    if (a.NombreUsuario.Equals(obj.NombreUsuario))
                    {
                        throw new UniqueException("El usuario ya existe, ingrese otro nombre de usuario");
                    }
                }
            }
            catch (UniqueException)
            {

                throw;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void Delete(int id)
        {
            try { 
            
                var paciente = GetPorId(id);
              

                _context.Pacientes.Remove(paciente);
               
                _context.SaveChanges();
            
            }catch (Exception) { throw; }
        }

        public IEnumerable<Paciente> GetAll()
        {
            try
            {
                IEnumerable<Paciente> pacientes = new List<Paciente>();
                pacientes = _context.Pacientes.ToList();
                return pacientes;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Paciente GetPacientePorCedula(string cedula)
        {

            try
            {
            if (String.IsNullOrEmpty(cedula)) {
                throw new NullOrEmptyException("No se recibio cedula");
            }
                var paciente = _context.Pacientes.FirstOrDefault(paciente => paciente.Cedula.Equals(cedula));
                if (paciente == null) {

                    throw new NotFoundException("No se encontro ningun paciente con esa cedula");
                }
                return paciente;

            }
            catch (NullOrEmptyException)
            {

                throw;
            }
            catch (NotFoundException)
            {

                throw;
            }
            catch (Exception) 
            {

                throw;
            }
           
       
        }

        public bool ExistePaciente(string usuario) {

            var paciente = _context.Pacientes.FirstOrDefault(paciente => paciente.NombreUsuario.Equals(usuario));

            if (paciente == null)
            {
                return false;
            }
            return true;
        }

        public Paciente GetPacientePorUsuario(string usuario)
        {
            try
            {
                if (String.IsNullOrEmpty(usuario))
                {
                    throw new NullOrEmptyException("No se recibio usuario");
                }
                var paciente = _context.Pacientes.FirstOrDefault(paciente => paciente.NombreUsuario.Equals(usuario));
                if (paciente == null)
                {
                    throw new NotFoundException("La cedula ingresada no existe");
                }
                return paciente;

            }
            catch (NullOrEmptyException)
            {

                throw;
            }
            catch (NotFoundException)
            {

                throw;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Paciente GetPorId(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new NullOrEmptyException("No se recibio id");
                }
                var paciente = _context.Pacientes.FirstOrDefault(paciente => paciente.Id == id);
                if (paciente == null)
                {
                    throw new NotFoundException("No se encontro ningun paciente con ese id");
                }
                return paciente;

            }
            catch (NullOrEmptyException)
            {

                throw;
            }
            catch (NotFoundException)
            {

                throw;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(Paciente obj)
        {
            try
            {
                if (obj == null) { throw new NullOrEmptyException("No se recibio paciente para editar"); }
                obj.Validar();

                _context.Pacientes.Update(obj); 
                _context.SaveChanges();
            }
            catch (NullOrEmptyException)
            {
                throw;
            }
            catch (UsuarioException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ServerErrorException("Error del servidor al actualizar el paciente");
            }
        }
        
    }
}
