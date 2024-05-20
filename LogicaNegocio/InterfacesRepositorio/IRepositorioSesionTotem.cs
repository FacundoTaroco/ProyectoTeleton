using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioSesionTotem : IRepositorio<SesionTotem>
    {
        public IEnumerable<SesionTotem> getSesionesDeTotem(int idTotem);
        public IEnumerable<SesionTotem> getSesionesAbiertasDeTotem(int idTotem);
        public IEnumerable<SesionTotem> getSesionesAbiertasDeFecha(int idTotem, DateTime fecha);
    }
}
