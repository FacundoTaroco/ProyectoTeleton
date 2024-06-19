using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioNotificacion
    {
        public void Add(Notificacion notificacion);
        public Notificacion Get(int id);
        public void Delete(int idNotificacion);
        public IEnumerable<Notificacion> GetPorUsuario(int idUsuario);
        public ParametrosNotificaciones GetParametrosRecordatorios();
        public void ActualizarParametrosRecordatorios(ParametrosNotificaciones nuevosParametros);

        public Notificacion GetNotificacionMasRecienteDeUsuario(int idUsuario);
    }
}
