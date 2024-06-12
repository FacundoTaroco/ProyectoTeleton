using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using LogicaAccesoDatos.EF.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using LogicaNegocio.Excepciones;
using Microsoft.EntityFrameworkCore;

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
                if (acceso == null) { throw new NullOrEmptyException("No se recibio acceso"); }
                acceso.Validar();
                acceso.Id = 0;
                
                _context.AccesosTotem.Add(acceso);
                _context.SaveChanges();

                
                AccesoTotem nuevoAcceso = _context.AccesosTotem.OrderByDescending(s => s.Id).Include(a => a._Totem).ToList().FirstOrDefault();
                return nuevoAcceso;
            }

            catch (AccesoTotemException)
            {
                throw;
            }
            catch (NullOrEmptyException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ServerErrorException("Error del servidor al agregar el acceso");
            }
        }

        public IEnumerable<AccesoTotem> GetAccesos(int idTotem)
        {
            try
            {
                if (idTotem == 0) {
                    throw new NullOrEmptyException("No se recibio totem");
                }
                if (_context.Totems.FirstOrDefault(tot => tot.Id == idTotem) == null) {
                    throw new NotFoundException("No se encontro totem");
                }

                IEnumerable<AccesoTotem> accesos = new List<AccesoTotem>();
                accesos = _context.AccesosTotem.Where(a => a.IdTotem == idTotem).Include(a => a._Totem).ToList();
                return accesos;
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

                throw new ServerErrorException("Error del servidor al obtener los accesos");
            }
        }

        public bool PacienteYaAccedioEnFecha(int idTotem, DateTime fecha, string cedulaPaciente) {

            AccesoTotem accesoTotem = _context.AccesosTotem.FirstOrDefault(a => a.IdTotem == idTotem &&
            (a.FechaHora.Day == fecha.Day && a.FechaHora.Month == fecha.Month && a.FechaHora.Year == fecha.Year)
            && a.CedulaPaciente.Equals(cedulaPaciente));

            if (accesoTotem == null)
            {
                return false;
            }
            else { 
                return true;
            }
        
        }

        public IEnumerable<AccesoTotem> GetAccesosPorDia(int idTotem, DateTime fecha)
        {
            try
            {
                if(fecha == DateTime.MinValue) {
                    throw new NullOrEmptyException("Ingrese una fecha valida");
                }
                if (idTotem == 0)
                {
                    throw new NullOrEmptyException("No se recibio totem");
                }
                if (_context.Totems.FirstOrDefault(tot => tot.Id == idTotem) == null)
                {
                    throw new NotFoundException("No se encontro totem");
                }

              
                IEnumerable<AccesoTotem> accesos = _context.AccesosTotem.Where(a => a.IdTotem == idTotem &&
                (a.FechaHora.Day == fecha.Day && a.FechaHora.Month == fecha.Month && a.FechaHora.Year == fecha.Year)).Include(a => a._Totem).ToList();
                return accesos;
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

                throw new ServerErrorException("Error del servidor al filtrar los accesos por dia");
            }
        }

       
    }
}
