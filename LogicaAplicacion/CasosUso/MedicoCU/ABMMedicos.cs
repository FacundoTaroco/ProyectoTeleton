using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.MedicoCU
{
    public class ABMMedicos
    {
        private IRespositorioMedico _repo;

        public ABMMedicos(IRespositorioMedico repo)
        {
            _repo = repo;
        }

        public void AltaMedico(Medico medico)
        {
            try
            {
                _repo.Add(medico);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void BajaMedico(int id)
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

        public void ModificarMedico(Medico medico)
        {
            try
            {
                _repo.Update(medico);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
