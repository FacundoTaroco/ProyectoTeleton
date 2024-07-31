using LogicaAccesoDatos.EF;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.PreguntasFrecCU
{
    public class GetPreguntasFrec
    {

        private IRepositorioPreguntaFrec _repo;
        public GetPreguntasFrec(IRepositorioPreguntaFrec repo)
        {
            _repo = repo;
        }


        public CategoriaPregunta GetCategoriaPorNombre(string nombre) {

            try
            {
                return _repo.GetCategoriaPorNombre(nombre);
            }
            catch (Exception)
            {

                throw;
            }
        
        }
        public IEnumerable<CategoriaPregunta> GetCategorias()
        {
            try
            {
                return _repo.GetAllCategorias();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IEnumerable<PreguntaFrec> GetAll()
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

        public IEnumerable<PreguntaFrec> GetPreguntasParaTotem() {
            try
            {
                return _repo.GetPreguntasTotem();
            }
            catch (Exception)
            {

                throw;
            }


        }

        public PreguntaFrec GetPreguntaFrecPorId(int id)
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
