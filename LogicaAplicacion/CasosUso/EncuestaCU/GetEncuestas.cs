using LogicaNegocio.DTO;
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

        public double PromedioSatisfaccionGeneral() { 
        
        
        return _repo.GetPromedioSatisfaccionGeneral();
        }
        public double PromedioSatisfaccionRecepcion()
        {


            return _repo.GetPromedioSatisfaccionRecepcion();
        }
        public double PromedioSatisfaccionAplicacion()
        {


            return _repo.GetPromedioSatisfaccionAplicacion();
        }
        public double PromedioSatisfaccionEstadoCentro()
        {


            return _repo.GetPromedioSatisfaccionEstadoCentro();
        }
        public IEnumerable<ComentarioEncuestaDTO> GetComentarios()
        {

            return _repo.GetComentariosPuntuados();
        }
    }
}
