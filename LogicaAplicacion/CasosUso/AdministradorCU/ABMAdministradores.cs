using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.AdministradorCU
{
    public class ABMAdministradores
    {
        private IRepositorioAdministrador _repo;
        public ABMAdministradores(IRepositorioAdministrador repo)
        {
            _repo = repo;
        }


        public void AltaAdmin(Administrador admin)
        {
            try
            {
                _repo.Add(admin);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void BajaAdmin(int id)
        {
            try
            {
                _repo.Delete(id);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void ModificarAdmin(Administrador admin)
        {
            try
            {
                _repo.Update(admin);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
