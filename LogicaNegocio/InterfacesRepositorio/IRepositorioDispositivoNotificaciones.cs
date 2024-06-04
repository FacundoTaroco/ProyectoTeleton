using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioDispositivoNotificaciones:IRepositorio<DispositivoNotificacion>
    {

        public IEnumerable<DispositivoNotificacion> GetDispositivosDePaciente(int idPaciente);
        public IEnumerable<DispositivoNotificacion> GetDispositivosDeRecepcionista(int id);
    }
}
