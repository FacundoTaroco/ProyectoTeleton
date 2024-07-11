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
    public class ABMPreguntasFrec
    {
        private IRepositorioPreguntaFrec _repo;
        public ABMPreguntasFrec(IRepositorioPreguntaFrec repo)
        {
            _repo = repo;
        }



        public void AltaCategoria(CategoriaPregunta categoria) {
            try
            {
                _repo.AddCategoria(categoria);
            }
            catch (Exception)
            {

                throw;
            }
        
        }

        public void AltaPreguntaFrec(PreguntaFrec preguntaFrec)
        {
            try
            {
           
                _repo.Add(preguntaFrec);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void BajaPreguntaFrec(int id)
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

        public void ModificarPreguntaFrec(PreguntaFrec preguntaFrec)
        {
            try
            {
                _repo.Update(preguntaFrec);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
