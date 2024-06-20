using LogicaAplicacion.CasosUso.ChatCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.CasosUso.RecepcionistaCU;
using LogicaAplicacion.Servicios;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using LogicaNegocio.EntidadesWit;
using LogicaNegocio.EntidadesWit.Entrenamiento;
using Microsoft.AspNetCore.SignalR;

namespace AppTeleton.Hubs
{
    public class HubConectado:Hub
    {
        private ChatBotService _chatBot;
        private ABMChat _abChat;
        private GetChats _getChats;
        private GetPacientes _getPacientes;
        private GetRecepcionistas _getRecepcionistas;
        private ABRespuestasEquivocadas _ABrespuestasEquivocadas;

        public HubConectado(ABRespuestasEquivocadas respuestasMal, ChatBotService chatbot, ABMChat abChat, GetChats getChats, GetPacientes getPacientes, GetRecepcionistas getRecepcionistas) { 
        _chatBot = chatbot;
        _abChat = abChat;
        _getChats = getChats;
        _getPacientes = getPacientes;
        _getRecepcionistas = getRecepcionistas;
        _ABrespuestasEquivocadas = respuestasMal;
        }

        public override Task OnConnectedAsync()
        {

            string usr = Context.GetHttpContext().Session.GetString("USR");
            ConexionChat conexion = new ConexionChat(Context.ConnectionId, usr);
            UsuariosConectados.usuariosConectados.Add(conexion);
            
            return base.OnConnectedAsync(); 
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            string usr = Context.GetHttpContext().Session.GetString("USR");
            UsuariosConectados.BorrarConexionPorId(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception); 
        }

        public async Task SendMessage(string userManda,string userRecibe, string message) {

            
           ActualizarChats(userManda,userRecibe,message);
           
            string idConexion = UsuariosConectados.GetIdConexionDeUsuario(userManda);
            if (!String.IsNullOrEmpty(idConexion))
            {

                if (userRecibe == "CHATBOT")
                {

                    MensajeBotDTO mensaje = new MensajeBotDTO("message", message);
                    Evento evento = _chatBot.PostEvent(mensaje);
                    string respuesta = evento.response.text;
                    //Aca por ahora con esto detectamos que el bot no reconocio la pregunta, faltaria hacerlo de forma generica(sin esperar un determinado texto)
                    //y ademas detectar cuando se equivoco segun el feedback de la persona(para mas adelante)
                    if (respuesta == "Reescriba la pregunta, por favor.")
                    {

                        RespuestaEquivocada respuestaEquivocada = new RespuestaEquivocada();
                        respuestaEquivocada.IntentAsignado = null;
                        respuestaEquivocada.Input = message;
                        _ABrespuestasEquivocadas.Agregar(respuestaEquivocada);
                    
                    }
                    ActualizarChats(userRecibe, userManda, respuesta);
                    await Clients.Client(idConexion).SendAsync("MensajeRecibido", "Asistente virtual", respuesta);

                }
                else {
                  
                    await Clients.All.SendAsync("MensajeRecibido", userManda, message);
                }
            }

        }

        public void ActualizarChats(string userManda, string userRecibe, string message) {

            if (_getPacientes.ExistePaciente(userManda))
            {
                Paciente paciente = _getPacientes.GetPacientePorUsuario(userManda);
                if (_getChats.PacienteTieneChatAbierto(paciente.Id))
                {
                    //SI el paciente tiene un chat abierto lo actualiza
                    Chat chat = _getChats.GetChatAbiertoDePaciente(paciente.Id);
                    Mensaje mensaje = new Mensaje(message,userManda);
                    chat.AgregarMensajePaciente(mensaje);
                    _abChat.Actualizar(chat);
                }
                else
                {
                    //si el paciente NO tiene un chat abierto lo crea
                    Chat chat = new Chat(paciente);
                    Mensaje mensaje = new Mensaje(message,userManda);
                    chat.AgregarMensajePaciente(mensaje);
                    _abChat.Crear(chat);
                }
            }
            else if (_getPacientes.ExistePaciente(userRecibe))
            {

                Paciente paciente = _getPacientes.GetPacientePorUsuario(userRecibe);
                if (_getChats.PacienteTieneChatAbierto(paciente.Id))
                {
                    //SI el paciente tiene un chat abierto lo actualiza
                    Chat chat = _getChats.GetChatAbiertoDePaciente(paciente.Id);
                    Mensaje mensaje = new Mensaje(message, userManda);
                    chat.AgregarMensajeBotRecepcion(mensaje);
                    _abChat.Actualizar(chat);
                }
                else
                {
                    //si el paciente NO tiene un chat abierto lo crea
                    Chat chat = new Chat(paciente);
                    Mensaje mensaje = new Mensaje(message, userManda);
                    chat.AgregarMensajeBotRecepcion(mensaje);
                    _abChat.Crear(chat);
                }
            }

        }


    }
}
