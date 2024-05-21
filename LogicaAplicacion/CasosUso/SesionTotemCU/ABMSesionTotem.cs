using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.SesionTotemCU
{
    public class ABMSesionTotem
    {

        private IRepositorioSesionTotem _repo;
        public ABMSesionTotem(IRepositorioSesionTotem repo)
        {
            _repo = repo;
        }
        public SesionTotem AgregarSesion(SesionTotem sesion)
        {
            try
            {
                return _repo.AgregarSesion(sesion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void CerrarSesion(SesionTotem sesion)
        {

            try
            {
                _repo.CerrarSesion(sesion);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
