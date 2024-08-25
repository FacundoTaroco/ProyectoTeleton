using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.ChatCU
{


    public class ABRespuestasEquivocadas
    {
        private IRepositorioRespuestasEquivocadas _repo;
        public ABRespuestasEquivocadas(IRepositorioRespuestasEquivocadas repo)
        {
            _repo = repo;
        }

        public void Agregar(RespuestaEquivocada res) {
            try
            {
                _repo.Add(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Borrar(int id) {

            try
            {
                _repo.Delete(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
