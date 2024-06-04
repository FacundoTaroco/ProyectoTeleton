using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.DispositivoUsuarioCU
{
    public class GuardarDispositivoNotificacion
    {
        private IRepositorioDispositivoNotificaciones _repo;
        public GuardarDispositivoNotificacion(IRepositorioDispositivoNotificaciones repo)
        {
            _repo = repo;
        }


        public void GuardarDispositivo(DispositivoNotificacion dispositivo)
        {

            try
            {
                _repo.Add(dispositivo);
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
