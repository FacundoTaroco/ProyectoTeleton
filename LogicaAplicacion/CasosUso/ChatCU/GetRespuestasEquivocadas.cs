using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.ChatCU
{
    public class GetRespuestasEquivocadas
    {
        private IRepositorioRespuestasEquivocadas _repo;
        public GetRespuestasEquivocadas(IRepositorioRespuestasEquivocadas repo)
        {
            _repo = repo;
        }

        public IEnumerable<RespuestaEquivocada> GetAll() {

            try
            {
                return _repo.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
        
        }
    }
}
