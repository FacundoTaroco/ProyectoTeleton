using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.AdministradorCU
{
    public class CambiarContrasenia
    {
        public IRepositorioUsuario _repo;

        public CambiarContrasenia(IRepositorioUsuario repo)
        {
            _repo = repo;
        }

        public void ChangePassword(int id, string nuevaContrasenia)
        {
            try
            {
                var usuario = _repo.GetPorId(id);
                if (usuario != null)
                {
                    usuario.Contrasenia = nuevaContrasenia;
                    _repo.Update(usuario);
                }
                else
                {
                    throw new Exception("Usuario no encontrado");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar la contraseña: " + ex.Message);
            }
        }

    }
}
