using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioUsuario
    {
        public TipoUsuario Login(string usuario, string contra);
        public Usuario GetUsuario(int idUsuario);
        public Usuario GetUsuarioPorNombre(string nombreUsu);
        public void CambiarContrasenia(Usuario usu);

    }
}
