using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioRecepcionista:IRepositorioRecepcionista
    {
        private LibreriaContext _context;
        public RepositorioRecepcionista(LibreriaContext context)
        {
            _context = context;
        }

        public void Add(Recepcionista obj)
        {

            try
            {
                if (obj == null) { throw new Exception("No se recibio el usuario"); }//hacer algunas excepciones personalizadas 
                obj.Validar();
                obj.Id = 0;
                //Validar paciente unique con los config!!

                _context.Recepcionistas.Add(obj);
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

                var recepcionista = GetPorId(id);
                if (recepcionista == null) { throw new Exception("No se encontro recepcionista"); }
                _context.Recepcionistas.Remove(recepcionista);

                _context.SaveChanges();

            }
            catch (Exception) { throw; }
        }

        public IEnumerable<Recepcionista> GetAll()
        {
            try
            {
                IEnumerable<Recepcionista> recepcionistas = _context.Recepcionistas.ToList();
                return recepcionistas;
            }
            catch (Exception)
            {

                throw;
            }
        }

       

        public Recepcionista GetPorId(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("No se recibio id");
                }
                var recepcionista = _context.Recepcionistas.FirstOrDefault(recep => recep.Id == id);
                if (recepcionista == null)
                {
                    throw new Exception("No se encontro ninguna recepcionista con esa cedula");
                }
                return recepcionista;

            }
            catch (Exception) // Excepciones personalizadaaas
            {

                throw;
            }
        }

        public void Update(Recepcionista obj)
        {
            try
            {
                if (obj == null) { throw new Exception("No se recibio recepcionista para editar"); }
                obj.Validar();

                _context.Recepcionistas.Update(obj);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
