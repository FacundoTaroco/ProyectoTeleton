using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioEncuesta
    {
        public void Add(Encuesta encuesta);
        public IEnumerable<Encuesta> GetEncuestas();
      

        public Encuesta GetPorId(int id);

    }

}
