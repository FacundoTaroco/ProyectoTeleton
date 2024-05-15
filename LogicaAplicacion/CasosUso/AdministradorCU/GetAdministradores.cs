using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.AdministradorCU
{
    public class GetAdministradores
    {
        private IRepositorioAdministrador _repo;
        public GetAdministradores(IRepositorioAdministrador repo)
        {
            _repo = repo;
        }


        public IEnumerable<Administrador> GetAll()
        {
            try
            {
                return _repo.GetAll();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Administrador GetAdministradorPorId(int id)
        {
            try
            {
                return _repo.GetPorId(id);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
