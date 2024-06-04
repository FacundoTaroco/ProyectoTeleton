using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.DispositivoUsuarioCU
{
    public class GetDispositivos
    {
        private IRepositorioDispositivoNotificaciones _repo;
        public GetDispositivos(IRepositorioDispositivoNotificaciones repo)
        {
            _repo = repo;
        }

        public IEnumerable<DispositivoNotificacion> getAllDispositivos() {
            try
            {
                return _repo.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<DispositivoNotificacion> getDispositivosPacientePorId(int id)
        {
            try
            {
                return _repo.GetDispositivosDePaciente(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
       

        public IEnumerable<DispositivoNotificacion> getDispositivosRecepcionistaPorId(int id)
        {
            try
            {
                return _repo.GetDispositivosDeRecepcionista(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
