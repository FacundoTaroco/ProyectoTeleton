using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.NotificacionCU
{
    public class ABNotificacion
    {
        private IRepositorioNotificacion _repo;

        public ABNotificacion(IRepositorioNotificacion repo)
        {
            _repo = repo;
        }

        public void Add(Notificacion notificacion) {
            try
            {
                _repo.Add(notificacion);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Delete(int idNotificacion) {
            try
            {
                _repo.Delete(idNotificacion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ActualizarParametros(ParametrosNotificaciones param) { 
            _repo.ActualizarParametrosRecordatorios(param);
        }

    }
}
