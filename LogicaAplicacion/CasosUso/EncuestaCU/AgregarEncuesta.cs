using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.EncuestaCU
{
    public class AgregarEncuesta
    {
        private IRepositorioEncuesta _repo;
        public AgregarEncuesta(IRepositorioEncuesta repo)
        {
            _repo = repo;
        }

        public void Agregar(Encuesta encuesta) {

            _repo.Add(encuesta);
        }
    }
}
