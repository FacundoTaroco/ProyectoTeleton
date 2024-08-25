using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.UsuarioCU
{
    public class GetUsuarios
    {
        private IRepositorioUsuario _repo;
        public GetUsuarios(IRepositorioUsuario repo)
        {
            _repo = repo;
        }


        public Usuario GetUsuario(int idUsuario) {
            try
            {
                return _repo.GetUsuario(idUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        
        }

        public Usuario GetUsuarioNombre(string nombreUsr)
        {
            try
            {
                return _repo.GetUsuarioPorNombre(nombreUsr);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
