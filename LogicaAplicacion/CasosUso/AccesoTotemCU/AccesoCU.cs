using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.AccesoTotemCU
{
    public class AccesoCU
    {
        private IRepositorioAccesoTotem _repo;
        public AccesoCU(IRepositorioAccesoTotem repo)
        {
            _repo = repo;
        }

        public AccesoTotem AgregarAcceso(AccesoTotem acceso)
        {
            try
            {
               AccesoTotem nuevoAccesoTotem = _repo.AgregarAcceso(acceso);
               return nuevoAccesoTotem;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<AccesoTotem> GetAccesos(int idTotem) {
            try
            {
                return _repo.GetAccesos(idTotem);
            }
            catch (Exception)
            {

                throw;
            } 
        }
        public IEnumerable<AccesoTotem> GetAccesosPorDia(int idTotem, DateTime fecha) {
            try
            {
                return _repo.GetAccesosPorDia(idTotem, fecha);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool PacienteYaAccedioEnFecha(int idTotem, DateTime fecha, string cedulaPaciente) { 
            
            return _repo.PacienteYaAccedioEnFecha(idTotem,fecha,cedulaPaciente);
            
        }

    }
}
