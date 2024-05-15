using LogicaNegocio.Entidades;
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
                if (obj == null) { throw new Exception("No se recibio el usuario"); }//hacer algunas excepciones personalizadas 
                obj.Validar();
                obj.Id = 0;
                //Validar paciente unique con los config!!
                _context.Usuarios.Add(obj);
                _context.Pacientes.Add(obj); //Los pacientes se guardan en dos tablas una de usuarios generales y otra de pacientes ver mas adelante si dejar una sola
                 
            }catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            try { 
            
                var paciente = GetPacientePorId(id);
                _context.Pacientes.Remove(paciente);
                _context.Usuarios.Remove(paciente);
                _context.SaveChanges();
            
            }catch (Exception) { throw; }
        }

        public IEnumerable<Paciente> GetAll()
        {
            try
            {
                IEnumerable<Paciente> pacientes = _context.Pacientes.ToList();
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
                throw new Exception("No se recibio cedula");
            }
                var paciente = _context.Pacientes.FirstOrDefault(paciente => paciente.Cedula.Equals(cedula));
                if (paciente == null) {

                    throw new Exception("No se encontro ningun paciente con esa cedula");
                }
                return paciente;

            }
            catch (Exception) // Excepciones personalizadaaas
            {

                throw;
            }
           
       
        }

        public Paciente GetPacientePorId(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("No se recibio id");
                }
                var paciente = _context.Pacientes.FirstOrDefault(paciente => paciente.Id == id);
                if (paciente == null)
                {
                    throw new Exception("No se encontro ningun paciente con esa cedula");
                }
                return paciente;

            }
            catch (Exception) // Excepciones personalizadaaas
            {

                throw;
            }
        }

        public void Update(Paciente obj)
        {
            try
            {
                if (obj == null) { throw new Exception("No se recibio paciente para editar"); }
                obj.Validar();

                _context.Pacientes.Update(obj); 
                _context.Usuarios.Update(obj); // ver si usar las dos tablas o dejar una
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}
