using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

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

        public Totem GetTotemPorUsr(string usr) {
            try
            {
                if (String.IsNullOrEmpty(usr)) {

                    throw new Exception("No se recibio nombre de usuario");
                }
                Totem totem = _context.Totems.FirstOrDefault(tot => tot.NombreUsuario == usr);
                return totem;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Totem> GetAll()
        {
            try
            {
                IEnumerable<Totem> totems = _context.Totems.Include(tot => tot.Sesiones).ThenInclude(sesion => sesion.Accesos).ToList();
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
                }//retorna el totem con todas sus sesiones y todos sus accesos
                var totem = _context.Totems.Include(tot => tot.Sesiones).ThenInclude(sesion => sesion.Accesos).FirstOrDefault(tot => tot.Id == id);
                   
                if (totem == null)
                {
                    throw new Exception("No se encontro ninguna totem con ese id");
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
        

        public void AgregarAcceso(AccesoTotem acceso, int idTotem)
        {
            try
            {
                if (acceso == null)
                {
                    throw new Exception("No se recibio el acceso");
                }

                if (_context.Totems.FirstOrDefault(tot => tot.Id == idTotem) == null) {
                    throw new Exception("No se encontro totem");
                }
                if (_context.Totems.FirstOrDefault(tot => tot.Id == idTotem)
                    .Sesiones.FirstOrDefault(sesion => sesion.Id == acceso.IdSesionTotem) == null)
                {
                    throw new Exception("No se encontro sesion");
                }

                _context.Totems.FirstOrDefault(tot => tot.Id == idTotem)
                    .Sesiones.FirstOrDefault(sesion => sesion.Id == acceso.IdSesionTotem).Accesos.Add(acceso);  

                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        

      
    }
}
