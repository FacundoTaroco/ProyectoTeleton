using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.UsuarioCU
{
    public class Login : ILogin
    {
        private IRepositorioUsuario _repo;
        public Login(IRepositorioUsuario repo)
        {
            _repo = repo;
        }

        public TipoUsuario LoginCaso(string usuario, string contrasenia)
        {
          
            TipoUsuario retorno = _repo.Login(usuario, contrasenia);
            return retorno;
           
           
        }

    }
}
