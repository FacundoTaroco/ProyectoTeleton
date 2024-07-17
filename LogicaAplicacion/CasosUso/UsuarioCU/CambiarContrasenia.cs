using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.UsuarioCU
{
    public class CambiarContrasenia
    {
        private IRepositorioUsuario _repo;
        public CambiarContrasenia(IRepositorioUsuario repo)
        {
            _repo = repo;
        }


        public void Cambiar(Usuario usuario) { 

            _repo.CambiarContrasenia(usuario);
      
        }
    }
}
