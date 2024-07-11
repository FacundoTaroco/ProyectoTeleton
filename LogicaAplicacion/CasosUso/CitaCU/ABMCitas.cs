using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CitaCU
{
    public class ABMCitas
    {
        private IRepositorioCitaMedica _repo;
        public ABMCitas(IRepositorioCitaMedica repo)
        {
            _repo = repo;
        }


        public void AltaCitaMedica(CitaMedica citaMedica)
        {
            try
            {
                _repo.Add(citaMedica);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void BajaCitaMedica(int id)
        {
            try
            {
                _repo.Delete(id);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void ModificarCitaMedica(CitaMedica citaMedica)
        {
            try
            {
                _repo.Update(citaMedica);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
