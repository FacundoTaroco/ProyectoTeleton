using LogicaAccesoDatos.EF.Excepciones;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioChat : IRepositorioChat
    {
        private LibreriaContext _context;
        public RepositorioChat(LibreriaContext context)
        {
            _context = context;
        }

        public void Add(Chat chat)
        {
            try
            {
                if (chat == null)
                {

                    throw new NullOrEmptyException("No se recibio chat");
                }
                chat.Validar();
                chat.Id = 0;

                _context.Chats.Add(chat);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {

                var chat = GetPorId(id);


                _context.Chats.Remove(chat);

                _context.SaveChanges();

            }
            catch (Exception) { throw; }
        }


        public IEnumerable<Chat> GetChatsDePaciente(int idPaciente) {
            try
            {
                IEnumerable<Chat> chats = new List<Chat>();

                chats = _context.Chats.Include(c => c._Paciente).Include(c=>c._Recepcionista).Where(c => c._Paciente.Id == idPaciente).ToList();

                return chats;
            }
            catch (Exception)
            {

                throw;
            }
        
        }
        public IEnumerable<Chat> GetChatsQueSolicitaronAsistenciaNoAtendidos() {
            try
            {
                IEnumerable<Chat> chats = new List<Chat>();
                chats = _context.Chats.Include(c => c._Paciente).Where(c => c.AsistenciaAutomatica == false && c._Recepcionista == null).ToList();

                return chats;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Chat> GetChatsDeRecepcionista(int idRecepcionista) {
            try
            {
                IEnumerable<Chat> chats = new List<Chat>();
                chats = _context.Chats.Include(c => c._Paciente).Where(c => c._Recepcionista != null && c._Recepcionista.Id == idRecepcionista).ToList();

                return chats;
            }
            catch (Exception)
            {

                throw;
            }
        
        
        
        
        }




        public Chat GetChatAbiertoDePaciente(int idPaciente)
        {
            try
            {
                Chat chatAbiertoDePaciente = _context.Chats.Include(c => c._Paciente).Include(c => c.Mensajes).Include(c => c._Recepcionista).FirstOrDefault(c => c._Paciente.Id == idPaciente && c.Abierto);
                if (chatAbiertoDePaciente == null) {
                    throw new NotFoundException("No se encontro ningun chat abierto del paciente");
                }
                return chatAbiertoDePaciente;
            }
            catch (NotFoundException)
            {

                throw;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ExisteChatPacienteRecepcionista(string pacienteUsuario, string recepcionistaUsuario) {

            Chat chatEntreLosDos = _context.Chats.Include(c => c._Paciente).Include(c => c._Recepcionista).FirstOrDefault(c => c._Paciente.NombreUsuario == pacienteUsuario && c._Recepcionista.NombreUsuario == recepcionistaUsuario);
            if (chatEntreLosDos == null)
            {
                return false;
            }
            return true;

        }

        public bool PacienteTieneChatAbierto(int idPaciente) {
            Chat chatAbiertoDePaciente = _context.Chats.Include(c => c._Paciente).FirstOrDefault(c => c._Paciente.Id == idPaciente && c.Abierto);
            if (chatAbiertoDePaciente == null) { 
                return false;
            }
            return true;

        }

        public Chat GetPorId(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new NullOrEmptyException("No se recibio id");
                }
                var chat = _context.Chats.Include(c => c._Paciente).Include(c => c.Mensajes).Include(c => c._Recepcionista).FirstOrDefault(chat => chat.Id == id);
                if (chat == null)
                {
                    throw new NotFoundException("No se encontro ningun paciente con ese id");
                }
                return chat;

            }
            catch (NullOrEmptyException)
            {

                throw;
            }
            catch (NotFoundException)
            {

                throw;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Actualizar(Chat chat)
        {
                try
                {
                    if (chat == null)
                    {
                        throw new Exception("No se recibió chat para editar");
                    }

                    chat.Validar();

                    var chatExistente = _context.Chats.FirstOrDefault(c => c.Id == chat.Id);

                    if (chatExistente == null)
                    {
                        throw new NotFoundException("No se encontró chat a editar");
                    }
                    _context.Chats.Update(chatExistente);
                    _context.SaveChanges();
                }
                catch (NotFoundException)
                {
                    throw;
                }
                catch (UsuarioException)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw;
                }
            
        }
    }
}
