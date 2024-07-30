using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.EncuestaCU
{
    public class GetEncuestas
    {
        private IRepositorioEncuesta _repo;
        public GetEncuestas(IRepositorioEncuesta repo)
        {
            _repo = repo;
        }


        public IEnumerable<Encuesta> GetAll() { 
        
        return _repo.GetEncuestas();
        }
    }
}
