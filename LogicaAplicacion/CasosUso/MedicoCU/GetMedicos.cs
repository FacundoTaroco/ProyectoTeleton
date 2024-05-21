using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.MedicoCU
{
    public class GetMedicos
    {
        private IRepositorioMedico _repo;
        public GetMedicos(IRepositorioMedico repo)
        {
            _repo = repo;
        }


        public IEnumerable<Medico> GetAll()
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

        public Medico GetMedicoPorId(int id)
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
