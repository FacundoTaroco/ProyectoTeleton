using LogicaNegocio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesDominio
{
    public interface ILogin
    {
        public TipoUsuario LoginCaso(string usuario, string contrasenia);

    }
}
