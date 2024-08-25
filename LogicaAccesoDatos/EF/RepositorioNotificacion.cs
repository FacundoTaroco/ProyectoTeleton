using LogicaAccesoDatos.EF.Excepciones;
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
    public class RepositorioNotificacion : IRepositorioNotificacion
    {

        private LibreriaContext _context;
        public RepositorioNotificacion(LibreriaContext context)
        {
            _context = context;
        }
        public void Add(Notificacion notificacion)
        {
            try
            {
                if (notificacion == null) {

                    throw new NullOrEmptyException("No se recibio notificacion");
                }
                notificacion.Validar();
                notificacion.Id = 0;

                _context.Notificaciones.Add(notificacion);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int idNotificacion)
        {
            try
            {

            Notificacion notificacion = Get(idNotificacion);
             _context.Notificaciones.Remove(notificacion);
            _context.SaveChanges(); 
            }
            catch (Exception)
            {

                throw;
            }

       
        }

        public Notificacion Get(int id)
        {
            try
            {
                if (id == 0) { throw new NullOrEmptyException("no se recibio id de notificacion"); }
                Notificacion notificacion = _context.Notificaciones.Include(n => n.Usuario).FirstOrDefault(n=>n.Id == id);
                if (notificacion == null) { throw new NotFoundException("no se encontro la notificacion solicitada"); }
                return notificacion;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Notificacion> GetPorUsuario(int idUsuario)
        {
            try
            {
                if (idUsuario == 0) { throw new NullOrEmptyException("no se recibio id de usuario"); }
                IEnumerable<Notificacion> notificaciones = new List<Notificacion>();
                notificaciones = _context.Notificaciones.Where(n => n.IdUsuario == idUsuario).ToList();
                return notificaciones;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Notificacion GetNotificacionMasRecienteDeUsuario(int idUsuario) {
            try
            {
                if (idUsuario == 0) { throw new NullOrEmptyException("no se recibio id de usuario"); }
                IEnumerable<Notificacion> notificaciones = new List<Notificacion>();
                notificaciones = _context.Notificaciones.Where(n => n.IdUsuario == idUsuario).OrderByDescending(n => n.fecha).ToList();
                if (notificaciones.Count() > 0)
                {

                    Notificacion notificacion = notificaciones.First();
                    return notificacion;
                }
                
                return null;
            }
            catch (Exception)
            {

                throw;
            }
            
        
        }

        public ParametrosNotificaciones GetParametrosRecordatorios() {

            try
            {
                ParametrosNotificaciones parametros = _context.ParametrosRecordatorios.FirstOrDefault(p => p.Id == 1);
                if (parametros == null) {
                    throw new NotFoundException("Algo salio mal con los recordatorios");
                }
                return parametros;
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

        public void ActualizarParametrosRecordatorios(ParametrosNotificaciones nuevosParametros) {
            try
            {
                nuevosParametros.Validar();
                _context.Update(nuevosParametros);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
                
        
        }
    }
}
