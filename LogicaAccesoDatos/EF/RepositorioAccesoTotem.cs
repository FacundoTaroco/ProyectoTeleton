using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioAccesoTotem : IRepositorioAccesoTotem
    {


        private LibreriaContext _context;
        public RepositorioAccesoTotem(LibreriaContext context)
        {
            _context = context;
        }

        public AccesoTotem AgregarAcceso(AccesoTotem acceso)
        {
            try
            {
                if (acceso == null) { throw new Exception("No se recibio acceso"); }//hacer algunas excepciones personalizadas 
                acceso.Id = 0;
                //Validar totem unique con los config!!

                _context.AccesosTotem.Add(acceso);
                _context.SaveChanges();

                AccesoTotem nuevoAcceso = _context.AccesosTotem.OrderByDescending(s => s.Id).FirstOrDefault();

                return nuevoAcceso;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<AccesoTotem> GetAccesos(int idTotem)
        {
            try
            {
                if (idTotem == 0) {
                    throw new Exception("No se recibio totem");
                }
                if (_context.Totems.FirstOrDefault(tot => tot.Id == idTotem) == null) {
                    throw new Exception("No se encontro totem");
                }


                IEnumerable<AccesoTotem> accesos = _context.AccesosTotem.Where(a => a._SesionTotem.TotemId == idTotem).ToList();
                return accesos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<AccesoTotem> GetAccesosPorDia(int idTotem, DateTime fecha)
        {
            try
            {
                //VALIDAR FECHA
                if (idTotem == 0)
                {
                    throw new Exception("No se recibio totem");
                }
                if (_context.Totems.FirstOrDefault(tot => tot.Id == idTotem) == null)
                {
                    throw new Exception("No se encontro totem");
                }


                IEnumerable<AccesoTotem> accesos = _context.AccesosTotem.Where(a => a._SesionTotem.TotemId == idTotem && a.FechaHora.Day == fecha.Day).ToList();
                return accesos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<AccesoTotem> GetAccesosPorSesion(int idTotem, int idSesion)
        {
            try
            {
            
                if (idTotem == 0)
                {
                    throw new Exception("No se recibio totem");
                }
                if (idSesion == 0) {
                    throw new Exception("No se recibio sesion");
                }
                if (_context.Totems.FirstOrDefault(tot => tot.Id == idTotem) == null)
                {
                    throw new Exception("No se encontro totem");
                }
                if (_context.SesionesTotem.FirstOrDefault(ses => ses.Id == idSesion) == null)
                {
                    throw new Exception("No se encontro sesion");
                }

                IEnumerable<AccesoTotem> accesos = _context.AccesosTotem.Where(a => a.IdSesionTotem == idSesion &&  a._SesionTotem.TotemId == idTotem).ToList();
                return accesos;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
