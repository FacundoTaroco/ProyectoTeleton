using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioChat
    {
        public void Add(Chat chat);
        public void Delete(int id);
        public void Actualizar(Chat chat);
        public Chat GetPorId(int id);
        public Chat GetChatAbiertoDePaciente(int idPaciente);
        public bool PacienteTieneChatAbierto(int idPaciente);

        public IEnumerable<Chat> GetChatsDePaciente(int idPaciente);
        public IEnumerable<Chat> GetChatsQueSolicitaronAsistenciaNoAtendidos();
        public IEnumerable<Chat> GetChatsDeRecepcionista(int idRecepcionista);
        public bool ExisteChatPacienteRecepcionista(string pacienteUsuario, string recepcionistaUsuario);

    }
}
