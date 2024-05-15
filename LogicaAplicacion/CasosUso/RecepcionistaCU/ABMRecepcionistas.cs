using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.RecepcionistaCU
{
    public class ABMRecepcionistas
    {
        private IRepositorioRecepcionista _repo;
        public ABMRecepcionistas(IRepositorioRecepcionista repo)
        {
            _repo = repo;
        }


        public void AltaRecepcionista(Recepcionista recepcionista)
        {
            try
            {
                _repo.Add(recepcionista);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void BajaRecepcionista(int id)
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

        public void ModificarRecepcionista(Recepcionista recepcionista)
        {
            try
            {
                _repo.Update(recepcionista);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
