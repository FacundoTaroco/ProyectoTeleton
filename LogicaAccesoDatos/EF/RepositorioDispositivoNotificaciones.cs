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

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DispositivoNotificacion> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DispositivoNotificacion> GetDispositivosDePaciente(int idPaciente)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DispositivoNotificacion> GetDispositivosDePacienteCedula(string cedula)
        {
            throw new NotImplementedException();
        }

        public DispositivoNotificacion GetPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(DispositivoNotificacion obj)
        {
            throw new NotImplementedException();
        }
    }
}
