using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.NotificacionCU
{
    public class GetNotificacion
    {
        private IRepositorioNotificacion _repo;

        public GetNotificacion(IRepositorioNotificacion repo)
        {
            _repo = repo;
        }

        public Notificacion GetPorId(int idNotificacion) {
            try
            {
                return _repo.Get(idNotificacion);
            }
            catch (Exception)
            {

                throw;
            }
        
        
        }
        public IEnumerable<Notificacion> GetPorUsuario(int idUsuario) {
            try
            {
               return _repo.GetPorUsuario(idUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        
        }
        public Notificacion GetMasRecientePorUsuario(int idUsuario) { 
            return _repo.GetNotificacionMasRecienteDeUsuario(idUsuario);
        
        }

        public ParametrosNotificaciones GetParametrosRecordatorios() { 
            return _repo.GetParametrosRecordatorios();
        }
    }
}
