using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.EncuestaCU
{
    public class ABMEncuestas
    {
        private IRepositorioEncuesta _repo;
        public ABMEncuestas(IRepositorioEncuesta repo)
        {
            _repo = repo;
        }


        public void AltaEncuesta(Encuesta encuesta)
        {
            try
            {
                _repo.Add(encuesta);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void BajaEncuesta(int id)
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

        public void ModificarEncuesta(Encuesta Encuesta)
        {
            try
            {
                _repo.Update(Encuesta);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
