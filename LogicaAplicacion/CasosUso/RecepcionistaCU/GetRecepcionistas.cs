using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.RecepcionistaCU
{
    public class GetRecepcionistas
    {
        private IRepositorioRecepcionista _repo;
        public GetRecepcionistas(IRepositorioRecepcionista repo)
        {
            _repo = repo;
        }


        public IEnumerable<Recepcionista> GetAll()
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

        public Recepcionista GetRecepcionistaPorId(int id)
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
        public Recepcionista GetRecepcionistaPorUsuario(string usuario)
        {
            try
            {
                return _repo.GetRecepcionistaPorUsuario(usuario);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
