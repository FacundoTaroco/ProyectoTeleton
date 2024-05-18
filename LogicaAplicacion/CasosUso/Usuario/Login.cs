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
        private IRepositorioTotem _repo;
        public Login(IRepositorioTotem repo)
        {
            _repo = repo;
        }

        public bool LoginCaso(string nombre, string Contrasenia)
        {
            bool retorno = _repo.Login(Contrasenia, nombre);
            return retorno;
        }

    }
}
