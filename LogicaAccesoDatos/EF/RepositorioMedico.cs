using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioMedico : IRepositorioMedico
    {

        private LibreriaContext _context;
        public RepositorioMedico(LibreriaContext context)
        {
            _context = context;
        }
        //comentario de prueba
        public void Add(Medico obj)
        {

            try
            {
                if (obj == null) { throw new Exception("No se recibió el usuario"); }//hacer algunas excepciones personalizadas 
                obj.Validar();
                obj.Id = 0;

                ValidarUnique(obj);
                _context.Medicos.Add(obj);
                _context.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {

                var medico = GetPorId(id);
                if (medico == null) { throw new Exception("No se encontró medico"); }
                _context.Medicos.Remove(medico);

                _context.SaveChanges();

            }
            catch (Exception) { throw; }
        }
        public void ValidarUnique(Medico obj)
        {
            try
            {
                foreach (Medico a in GetAll())
                {

                    if (a.NombreUsuario.Equals(obj.NombreUsuario))
                    {

                        throw new Exception("El medico ya existe, ingrese otro nombre de usuario");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public IEnumerable<Medico> GetAll()
        {
            try
            {
                IEnumerable<Medico> medicos = _context.Medicos.ToList();
                return medicos;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public Medico GetPorId(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("No se recibio id");
                }
                var medicos = _context.Medicos.FirstOrDefault(med => med.Id == id);
                if (medicos == null)
                {
                    throw new Exception("No se encontro ninguna recepcionista con esa cedula");
                }
                return medicos;

            }
            catch (Exception) // Excepciones personalizadaaas
            {

                throw;
            }
        }

        public void Update(Medico obj)
        {
            try
            {
                if (obj == null) { throw new Exception("No se recibio recepcionista para editar"); }
                obj.Validar();

                _context.Medicos.Update(obj);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
