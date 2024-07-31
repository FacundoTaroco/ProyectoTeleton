using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioPreguntaFrec : IRepositorio<PreguntaFrec>
    {
        public void AddCategoria(CategoriaPregunta categoria);
        public IEnumerable<PreguntaFrec> GetPreguntasTotem();
        public IEnumerable<CategoriaPregunta> GetAllCategorias();

        public CategoriaPregunta GetCategoriaPorNombre(string nombre);
    }
}
