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
    public class RepositorioDispositivoNotificaciones : IRepositorioDispositivoNotificaciones
    {
        private LibreriaContext _context;
        public RepositorioDispositivoNotificaciones(LibreriaContext context)
        {
            _context = context;
        }

        public void Add(DispositivoNotificacion obj)
        {
            try
            {


                if (obj == null) { throw new NullOrEmptyException("No se recibio el dispositivo"); }
                obj.Validar();
                obj.Id = 0;
                _context.Dispositivos.Add(obj);
                _context.SaveChanges();


            }
            catch (NullOrEmptyException)
            {
                throw;
            }
            catch (DispositivoNotificacionException)
            {
                throw;
            }
            catch (UniqueException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ServerErrorException("Error del servidor, algo fallo al agregar el dispositivo");
            }
        }

        public void Delete(int idDisp)
        {
            try
            {
                DispositivoNotificacion disp = _context.Dispositivos.FirstOrDefault(d => d.Id == idDisp);
                _context.Dispositivos.Remove(disp);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<DispositivoNotificacion> GetAll()
        {
            try
            {
             IEnumerable<DispositivoNotificacion> dispositivos = new List<DispositivoNotificacion>();
            dispositivos = _context.Dispositivos.Include(disp=>disp.Usuario).ToList();
            return dispositivos;
            }
            catch (Exception)
            {

                throw;
            }
         
        }

        public IEnumerable<DispositivoNotificacion> GetDispositivosDePaciente(int idPaciente)
        {
            try
            {
                IEnumerable<DispositivoNotificacion> dispositivosPaciente = new List<DispositivoNotificacion>();
                dispositivosPaciente = _context.Dispositivos.Include(disp => disp.Usuario).Where(disp => disp.IdUsuario == idPaciente).ToList();
                return dispositivosPaciente;
            }
            catch (Exception)
            {

                throw;
            }
        }

      

        public IEnumerable<DispositivoNotificacion> GetDispositivosDeRecepcionista(int idRecepcionista) {


            try
            {
                IEnumerable<DispositivoNotificacion> dispositivosPaciente = new List<DispositivoNotificacion>();
                dispositivosPaciente = _context.Dispositivos.Where(disp => disp.IdUsuario == idRecepcionista).ToList();
                return dispositivosPaciente;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public DispositivoNotificacion GetPorId(int id)
        {
            try
            {
            DispositivoNotificacion dispositivo = new DispositivoNotificacion();
             dispositivo = _context.Dispositivos.FirstOrDefault(disp=>disp.Id==id);
                if (dispositivo == null) {
                    throw new Exception("No se encontro dispositivo");
                }
             return dispositivo;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void Update(DispositivoNotificacion obj)
        {
            throw new NotImplementedException();
        }
    }
}
