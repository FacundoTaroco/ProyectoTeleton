using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioAccesoTotem
    {

        public IEnumerable<AccesoTotem> GetAccesos(int idTotem);
        public IEnumerable<AccesoTotem> GetAccesosPorDia(int idTotem, DateTime fecha);
        public AccesoTotem AgregarAcceso(AccesoTotem acceso);
        public bool PacienteYaAccedioEnFecha(int idTotem, DateTime fecha, string cedulaPaciente);
    }
}
