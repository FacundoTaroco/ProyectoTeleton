using LogicaNegocio.Entidades;
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
                if (sesion == null) { throw new Exception("No se recibio sesion"); }//hacer algunas excepciones personalizadas 
                sesion.Id = 0;
                //Validar totem unique con los config!!

                _context.SesionesTotem.Add(sesion);
                _context.SaveChanges();

                SesionTotem nuevaSesion = _context.SesionesTotem.OrderByDescending(s => s.Id).FirstOrDefault();

                return nuevaSesion;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void CerrarSesion(SesionTotem sesion)
        {
            try
            {
                if (_context.SesionesTotem.FirstOrDefault(s => s.Id == sesion.Id) == null)
                {
                    throw new Exception("No se encontro sesion");
                }
                if (sesion.SesionAbierta) {
                    throw new Exception("No se cerro la sesion correctamente");
                }
                _context.SesionesTotem.Update(sesion);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public SesionTotem GetSesionPorId(int id)
        {
            try
            {
                if (id == 0) { 
                    throw new Exception("No se recibio id");
                }
                if (_context.SesionesTotem.FirstOrDefault(s => s.Id == id) == null) {

                    throw new Exception("No se encontro sesion");
                }

                return _context.SesionesTotem.FirstOrDefault(s => s.Id == id);
            }
            catch (Exception)
            {

                throw;
            }
        }
            public IEnumerable<SesionTotem> GetSesionesDeTotem(int idTotem)
        {
            try
            {
                if (idTotem == 0)
                {
                    throw new Exception("No se recibio totem");
                }
                if (_context.Totems.FirstOrDefault(tot => tot.Id == idTotem) == null)
                {
                    throw new Exception("No se encontro totem");
                }
                IEnumerable<SesionTotem> sesiones = _context.SesionesTotem.Where(s=>s.TotemId == idTotem).Include(s => s.Accesos).ToList();
                return sesiones;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public IEnumerable<SesionTotem> GetSesionesAbiertasDeTotem(int idTotem)
        {
            try
            {
                if (idTotem == 0)
                {
                    throw new Exception("No se recibio totem");
                }
                if (_context.Totems.FirstOrDefault(tot => tot.Id == idTotem) == null)
                {
                    throw new Exception("No se encontro totem");
                }

                IEnumerable<SesionTotem> sesiones = _context.SesionesTotem.Where(s => s.TotemId == idTotem && s.SesionAbierta).Include(s=>s.Accesos).ToList();
                return sesiones;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<SesionTotem> GetSesionesDeFecha(int idTotem, DateTime fecha)
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


                IEnumerable<SesionTotem> sesiones = _context.SesionesTotem.Where(s => s.TotemId == idTotem && s.InicioSesion.Day == fecha.Day).Include(s => s.Accesos).ToList();
                return sesiones;

            }
            catch (Exception)
            {

                throw;
            }
        }

       
    }
}
