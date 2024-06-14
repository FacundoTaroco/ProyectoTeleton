using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.ChatCU
{
    public class ABMChat
    {
        private IRepositorioChat _repo;
        public ABMChat(IRepositorioChat repo)
        {
            _repo = repo;
        }



        public void Borrar(int idChat) {
            try
            {
                _repo.Delete(idChat);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Actualizar(Chat chat) {
            try
            {
                _repo.Actualizar(chat);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Crear(Chat chat) {
            try
            {
                _repo.Add(chat);
            }
            catch (Exception)
            {

                throw;
            }
        
        }
    }
}
