using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.Usuario
{
    public class Login : ILogin
    {
        private IRepositorioUsuario _repo;
        public Login(IRepositorioUsuario repo)
        {
            _repo = repo;
        }

        public string LoginCaso(string usuario, string contrasenia)
        {
          
            string retorno = _repo.Login(usuario, contrasenia);
            return retorno;
           
           
        }

    }
}
