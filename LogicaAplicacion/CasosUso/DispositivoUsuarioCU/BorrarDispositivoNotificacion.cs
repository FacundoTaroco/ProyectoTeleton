using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.DispositivoUsuarioCU
{

    public class BorrarDispositivoNotificacion
    {
        private IRepositorioDispositivoNotificaciones _repo;
        public BorrarDispositivoNotificacion(IRepositorioDispositivoNotificaciones repo)
        {
            _repo = repo;
        }

        public void Delete(int idDisp) {

            try
            {
                _repo.Delete(idDisp);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
