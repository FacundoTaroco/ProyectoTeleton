using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.SesionTotemCU
{
    public class GetSesionTotem
    {

        private IRepositorioSesionTotem _repo;
        public GetSesionTotem(IRepositorioSesionTotem repo)
        {
            _repo = repo;
        }

        public SesionTotem GetSesionPorId(int id) {
            try
            {
                return _repo.GetSesionPorId(id);
            }
            catch (Exception)
            {

                throw;
            }
        
        
        }
        public IEnumerable<SesionTotem> GetSesiones(int idTotem)
        {

            try
            {
                return _repo.GetSesionesDeTotem(idTotem);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<SesionTotem> GetSesionesAbiertas(int idTotem)
        {
            try
            {
                return _repo.GetSesionesAbiertasDeTotem(idTotem);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<SesionTotem> GetSesionesDeDia(int idTotem, DateTime fechaDia)
        {

            try
            {
                return _repo.GetSesionesDeFecha(idTotem, fechaDia);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
