using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioTotem : IRepositorio<Totem>
    {
        public bool Login(string nombre, string contrasenia);
    }
}
