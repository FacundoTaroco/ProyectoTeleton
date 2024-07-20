
using LogicaAplicacion.CasosUso.ChatCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.CasosUso.RecepcionistaCU;
using LogicaAplicacion.Servicios;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using LogicaNegocio.EntidadesWit;
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
        public EnviarNotificacionService _enviarNotificacion;

        public HubConectado(EnviarNotificacionService enviarNotificacion, ABRespuestasEquivocadas respuestasMal, ChatBotService chatbot, ABMChat abChat, GetChats getChats, GetPacientes getPacientes, GetRecepcionistas getRecepcionistas) { 
        _chatBot = chatbot;
        _abChat = abChat;
        _getChats = getChats;
        _getPacientes = getPacientes;
        _getRecepcionistas = getRecepcionistas;
        _ABrespuestasEquivocadas = respuestasMal;
        _enviarNotificacion = enviarNotificacion;
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

        public async Task SendMessage(string userManda, string userRecibe, string message) {

            string idConexion = "";
        
            ActualizarChats(userManda, userRecibe, message);

            if (_getPacientes.ExistePaciente(userManda))
            {
               

                    if (userRecibe == "") { 
                    
                    

                    }
                    else if (userRecibe == "CHATBOT")
                    {
                        //si el mensaje es para el chatbot entonces se responde directamente al paciente(el usuario que mando el mensaje)
                        idConexion = UsuariosConectados.GetIdConexionDeUsuario(userManda);
                    if (!String.IsNullOrEmpty(idConexion))
                    { 
                    /*MensajeBotDTO mensaje = new MensajeBotDTO("message", message);
                        Evento evento = _chatBot.PostEvent(mensaje);


                        string respuesta = evento.response.text;*/

                        string respuestaFinal = _chatBot.Responder(message);

                        /* MensajeRespuesta mensajeGetMessage = _chatBot.GetMessage(message);
                         //Aca por ahora con esto detectamos que el bot no reconocio la pregunta, faltaria hacerlo de forma generica(sin esperar un determinado texto)
                         //y ademas detectar cuando se equivoco segun el feedback de la persona(para mas adelante)*/

                        if (respuestaFinal == "Reescriba la pregunta, por favor.")
                        {

                            RespuestaEquivocada respuestaEquivocada = new RespuestaEquivocada();
                            respuestaEquivocada.IntentAsignado = "";
                            respuestaEquivocada.Input = message;
                            _ABrespuestasEquivocadas.Agregar(respuestaEquivocada);
                            AumentarIndiceReintento(userManda);

                        }
                        ActualizarChats(userRecibe, userManda, respuestaFinal);
                        await Clients.Client(idConexion).SendAsync("MensajeRecibido", "CHATBOT", userRecibe, respuestaFinal, true, true); //no msiempre tiene pq ser true
                    
                    }

                        

                    }
                    else
                    {
                        //si no es un mensaje para el chatbot entonces se manda un mensaje a la recepcionista(si existe)

                        if (_getChats.ExisteChatPacienteRecepcionista(userManda, userRecibe)) {

                            idConexion = UsuariosConectados.GetIdConexionDeUsuario(userRecibe);
                        if (!String.IsNullOrEmpty(idConexion))
                        { 
                        await Clients.Client(idConexion).SendAsync("MensajeRecibido", userManda, userRecibe, message, false, false);
                        
                        }
                            
                        }

                       
                        

                    }
                


            }
            else if(_getPacientes.ExistePaciente(userRecibe)) {
                idConexion = UsuariosConectados.GetIdConexionDeUsuario(userRecibe);
                if (!String.IsNullOrEmpty(idConexion))
                { 
                await Clients.Client(idConexion).SendAsync("MensajeRecibido", userManda, userRecibe, message, false, true);
                }
                    
            }



         
            
            

        }
        //se asume que es el chat con el bot mas adelante cuando se implemente recepcionista ver
        //si el mensaje fue bien respondido entonces se carga la utterance para aumentar el porcentaje de confianza
        //y ademas se cierra el chat ya que fue resuelta la consulta


        //VER QUE HACER CON LAS ENTITIES
        public async Task FeedBackPositivo(string mensaje, string user) {

            Paciente paciente = _getPacientes.GetPacientePorUsuario(user);


            //cerramos el chat
            Chat chatPaciente = _getChats.GetChatAbiertoDePaciente(paciente.Id);
            chatPaciente.Abierto = false;
            _abChat.Actualizar(chatPaciente);

            MensajeRespuesta mensajeGetMessage = _chatBot.GetMessage(mensaje);

            Utterance utterance = new Utterance();
            utterance.text = mensaje;
            utterance.intent = mensajeGetMessage.Intents.First().name; //VALIDAR QUE NO SEA NULO
            utterance.traits = new List<UtteranceTrait>();
            utterance.entities = new List<UtteranceEntity>();

            List<Utterance> utterances = new List<Utterance> { utterance };

            _chatBot.PostUtterance(utterances);
        }

        //en caso de que el feedback sea negativo(no le sirvio la resuesta) se manda a la pestaña de administracion para ser revisada
        public async Task FeedBackNegativo(string mensaje, string userManda) {

            AumentarIndiceReintento(userManda);
            MensajeRespuesta mensajeGetMessage = _chatBot.GetMessage(mensaje);
            RespuestaEquivocada respuestaEquivocada = new RespuestaEquivocada();
            respuestaEquivocada.IntentAsignado = mensajeGetMessage.Intents.First().name; //VALIDAR NO NULO
            respuestaEquivocada.Input = mensaje;
            _ABrespuestasEquivocadas.Agregar(respuestaEquivocada);

        }

        public async Task SolicitarAsistenciaPersonalizada(string userManda)
        {


            if (_getPacientes.ExistePaciente(userManda))
            {
                Paciente paciente = _getPacientes.GetPacientePorUsuario(userManda);
                _enviarNotificacion.EnviarATodosRecepcion("Solicitud de asistencia", paciente.Nombre + " esta solicitando asistencia personalizada por chat");
                if (_getChats.PacienteTieneChatAbierto(paciente.Id))
                {
                    //SI el paciente tiene un chat abierto lo actualiza
                    Chat chat = _getChats.GetChatAbiertoDePaciente(paciente.Id);
                    Mensaje mensaje = new Mensaje("Una recepcionista lo atendera por esta u otra via lo antes posible, recuerde que el horario de atencion personalizada es de 8am hasta las 5pm", "CHATBOT");
                    chat.AgregarMensajeBotRecepcion(mensaje);
                    chat.AsistenciaAutomatica = false;
                    _abChat.Actualizar(chat);
                }

            }
        }


        public void AumentarIndiceReintento(string userManda) {

            if (_getPacientes.ExistePaciente(userManda))
            {
                Paciente paciente = _getPacientes.GetPacientePorUsuario(userManda);
                 if (_getChats.PacienteTieneChatAbierto(paciente.Id))
                {
                    //SI el paciente tiene un chat abierto lo actualiza
                    Chat chat = _getChats.GetChatAbiertoDePaciente(paciente.Id);
                    chat.IndiceReintento += 1;
                    _abChat.Actualizar(chat);

                    if (chat.IndiceReintento > 2) {
                        MostrarBotoneraAsistencia(userManda);
                    }
                }
                else
                {
                    //si el paciente NO tiene un chat abierto lo crea
                    Chat chat = new Chat(paciente);
                    chat.IndiceReintento += 1;
                    _abChat.Crear(chat);
                }
            }
        
        }

        private async void MostrarBotoneraAsistencia(string userManda)
        {

            string idConexion = UsuariosConectados.GetIdConexionDeUsuario(userManda);
            if (!String.IsNullOrEmpty(idConexion))
            {
                await Clients.Client(idConexion).SendAsync("MostrarBotoneraAsistencia");
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
