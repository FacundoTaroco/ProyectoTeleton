using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioTotem : IRepositorioTotem
    {
        private LibreriaContext _context;
        public RepositorioTotem(LibreriaContext context)
        {
            _context = context;
        }

        public void Add(Totem obj)
        {

            try
            {
                if (obj == null) { throw new Exception("No se recibio el usuario"); }//hacer algunas excepciones personalizadas 
                obj.Validar();
                obj.Id = 0;
                //Validar totem unique con los config!!

                _context.Totems.Add(obj);
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

                var totem = GetPorId(id);
                if (totem == null) { throw new Exception("No se encontró totem"); }
                _context.Totems.Remove(totem);

                _context.SaveChanges();

            }
            catch (Exception) { throw; }
        }

        public IEnumerable<Totem> GetAll()
        {
            try
            {
                IEnumerable<Totem> totems = _context.Totems.ToList();
                return totems;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public Totem GetPorId(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("No se recibio id");
                }
                var totem = _context.Totems.FirstOrDefault(tot => tot.Id == id);
                if (totem == null)
                {
                    throw new Exception("No se encontro ninguna totem con esa cedula");
                }
                return totem;

            }
            catch (Exception) // Excepciones personalizadas
            {

                throw;
            }
        }

        public void Update(Totem obj)
        {
            try
            {
                if (obj == null) { throw new Exception("No se recibio totem para editar"); }
                obj.Validar();

                _context.Totems.Update(obj);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
