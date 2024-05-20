using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.TotemCU
{
    public class GetTotems
    {
        private IRepositorioTotem _repo;
        public GetTotems(IRepositorioTotem repo)
        {
            _repo = repo;
        }


        public IEnumerable<Totem> GetAll()
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

        public Totem GetTotemPorId(int id)
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
        public Totem GetTotemPorUsr(string usr) {

            try
            {
                return _repo.GetTotemPorUsr(usr);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
