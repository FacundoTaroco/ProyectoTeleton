using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.TotemCU
{
    public class ABMTotem
    {
        private IRepositorioTotem _repo;
        public ABMTotem(IRepositorioTotem repo)
        {
            _repo = repo;
        }


        public void AltaTotem(Totem totem)
        {
            try
            {
                _repo.Add(totem);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void BajaTotem(int id)
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

        public void ModificarTotem(Totem totem)
        {
            try
            {
                _repo.Update(totem);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

