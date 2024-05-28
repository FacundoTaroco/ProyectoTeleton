using LogicaAccesoDatos.EF.Excepciones;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioSesionTotem : IRepositorioSesionTotem
    {

        private LibreriaContext _context;
        public RepositorioSesionTotem(LibreriaContext context)
        {
            _context = context;
        }
        public SesionTotem AgregarSesion(SesionTotem sesion)
        {
            try
            {
                if (sesion == null) { throw new NullOrEmptyException("No se recibio sesion"); }
                sesion.Id = 0;
                sesion.Validar();

                _context.SesionesTotem.Add(sesion);
                _context.SaveChanges();

                SesionTotem nuevaSesion = _context.SesionesTotem.OrderByDescending(s => s.Id).Include(s=>s.Accesos).FirstOrDefault();
                return nuevaSesion;
            }
            catch (SesionTotemException)
            {
                throw;
            }
            catch (NullOrEmptyException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ServerErrorException("Error del servidor al iniciar una nueva sesion de totem");
            }
        }


        public void CerrarSesion(SesionTotem sesion)
        {
            try
            {
                if (_context.SesionesTotem.FirstOrDefault(s => s.Id == sesion.Id) == null)
                {
                    throw new NotFoundException("No se encontro sesion");
                }
                if (sesion.SesionAbierta) {
                    sesion.SesionAbierta = false;
                }
                sesion.Validar();
                _context.SesionesTotem.Update(sesion);
                _context.SaveChanges();
            }
            catch (SesionTotemException)
            {

                throw;
            }
            catch (NotFoundException)
            {

                throw;
            }
            catch (Exception)
            {

                throw new ServerErrorException("Error al cerrar sesion de totem");
            }
        }

        public SesionTotem GetSesionPorId(int id)
        {
            try
            {
                if (id == 0) { 
                    throw new NullOrEmptyException("No se recibio id");
                }
                if (_context.SesionesTotem.FirstOrDefault(s => s.Id == id) == null) {

                    throw new NotFoundException("No se encontro sesion");
                }

                SesionTotem sesion = _context.SesionesTotem.FirstOrDefault(s => s.Id == id);

                return sesion;
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

                throw new ServerErrorException("Error del servidor al obtener la sesion");
            }
        }
            public IEnumerable<SesionTotem> GetSesionesDeTotem(int idTotem)
        {
            try
            {
                if (idTotem == 0)
                {
                    throw new NullOrEmptyException("No se recibio totem");
                }
                if (_context.Totems.FirstOrDefault(tot => tot.Id == idTotem) == null)
                {
                    throw new NotFoundException("No se encontro totem");
                }
                IEnumerable<SesionTotem> sesiones = new List<SesionTotem>();    
                sesiones = _context.SesionesTotem.Where(s=>s.TotemId == idTotem).Include(s => s.Accesos).ToList();
                return sesiones;

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

                throw new ServerErrorException("Error del servidor al obtener las sesiones del totem");
            }

        }

        public IEnumerable<SesionTotem> GetSesionesAbiertasDeTotem(int idTotem)
        {
            try
            {
                if (idTotem == 0)
                {
                    throw new NullOrEmptyException("No se recibio totem");
                }
                if (_context.Totems.FirstOrDefault(tot => tot.Id == idTotem) == null)
                {
                    throw new NotFoundException("No se encontro totem");
                }
                IEnumerable<SesionTotem> sesiones = new List<SesionTotem>();
                sesiones = _context.SesionesTotem.Where(s => s.TotemId == idTotem && s.SesionAbierta).Include(s=>s.Accesos).ToList();
                return sesiones;

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

                throw new ServerErrorException("Error del servidor al obtener las sesiones abiertas del totem");
            }
        }

        public IEnumerable<SesionTotem> GetSesionesDeFecha(int idTotem, DateTime fecha)
        {
            try
            {
                if(fecha == DateTime.MinValue)
                {
                    throw new NullOrEmptyException("La fecha ingresada no es valida");
                }
                if (idTotem == 0)
                {
                    throw new NullOrEmptyException("No se recibio totem");
                }
                if (_context.Totems.FirstOrDefault(tot => tot.Id == idTotem) == null)
                {
                    throw new NotFoundException("No se encontro totem");
                }

                IEnumerable<SesionTotem> sesiones = new List<SesionTotem>();
                sesiones = _context.SesionesTotem.Where(s => s.TotemId == idTotem && s.InicioSesion.Day == fecha.Day).Include(s => s.Accesos).ToList();
                return sesiones;

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

                throw new ServerErrorException("Error del servidor al obtener las sesiones por dia");
            }
        }

       
    }
}
