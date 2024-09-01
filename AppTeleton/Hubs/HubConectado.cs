
using LogicaAplicacion.CasosUso.ChatCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.CasosUso.RecepcionistaCU;
using LogicaAplicacion.Servicios;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using LogicaNegocio.EntidadesWit;
using Microsoft.AspNetCore.SignalR;

namespace AppTeleton.Hubs
{//ESTA CLASE MANEJA TODA LA LOGICA EN TIEMPO REAL DEL CHAT 
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

        //Un usuario se conecta a la vista de chat
        public override Task OnConnectedAsync()
        {

            string usr = Context.GetHttpContext().Session.GetString("USR");
            ConexionChat conexion = new ConexionChat(Context.ConnectionId, usr);
            UsuariosConectados.usuariosConectados.Add(conexion);
            
            return base.OnConnectedAsync(); 
        }
        //Un usuario se desconecta a la vista de chat
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            string usr = Context.GetHttpContext().Session.GetString("USR");
            UsuariosConectados.BorrarConexionPorId(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception); 
        }

        //Funcion que manda mensajes en tiempo real
        public async Task SendMessage(string userManda, string userRecibe, string message) {

            string idConexion = "";
        
            ActualizarChats(userManda, userRecibe, message); //Actualiza el chat en la base de datos

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
                  

                        string respuestaFinal = _chatBot.Responder(message); //Obtenemos la respuesta del chatbot a partir del mensaje enviado

                     
                        if (respuestaFinal == "Reescriba la pregunta, por favor.") //El chatbot no supo responder
                        {

                            RespuestaEquivocada respuestaEquivocada = new RespuestaEquivocada(); //Generamos una respuesta Equivocada
                            respuestaEquivocada.IntentAsignado = "";
                            respuestaEquivocada.Input = message;
                            _ABrespuestasEquivocadas.Agregar(respuestaEquivocada); //agregamos la respuesta equivocada
                            AumentarIndiceReintento(userManda); //Aumentamos el indice de reintento, es decir la cantidad de veces que se equivoco el chat

                        }
                        ActualizarChats(userRecibe, userManda, respuestaFinal); //Actualizamos el chat en la base de datos
                        await Clients.Client(idConexion).SendAsync("MensajeRecibido", "CHATBOT", userRecibe, respuestaFinal, true, true); //Generamos la respuesta en tiempo real
                    
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
      
        //Cerrar un chat de paciente
        public async Task CerrarChat(string userPaciente) {

            Paciente paciente = _getPacientes.GetPacientePorUsuario(userPaciente);
            //cerramos el chat
            Chat chatPaciente = _getChats.GetChatAbiertoDePaciente(paciente.Id);
            chatPaciente.Abierto = false;
            _abChat.Actualizar(chatPaciente);

            string idConexion = UsuariosConectados.GetIdConexionDeUsuario(userPaciente);
            if (!String.IsNullOrEmpty(idConexion)) {


                await Clients.Client(idConexion).SendAsync("CerrarChatEnVivo");

            }
        }

        //Funcion para entrenar positivamente al chatbot si este respondio bien la consulta
        public async Task FeedBackPositivo(string mensaje, string user) {

            Paciente paciente = _getPacientes.GetPacientePorUsuario(user);


            //cerramos el chat
            Chat chatPaciente = _getChats.GetChatAbiertoDePaciente(paciente.Id);
            chatPaciente.Abierto = false;
            _abChat.Actualizar(chatPaciente);
            //Volvemos a obtener la respuesta
            MensajeRespuesta mensajeGetMessage = _chatBot.GetMessage(mensaje);

            //Cargamos la informacion de entrenamiento
            Utterance utterance = new Utterance();
            utterance.text = mensaje;
            if (mensajeGetMessage.Intents.Count() > 0 && mensajeGetMessage.Intents.First() != null) { 
               utterance.intent = mensajeGetMessage.Intents.First().name; 
            }
            utterance.traits = new List<UtteranceTrait>();
            utterance.entities = new List<UtteranceEntity>();
            List<Utterance> utterances = new List<Utterance> { utterance };

            //Enviamos la frase de entrenamiento a wit ai
            _chatBot.PostUtterance(utterances);
        }

        //en caso de que el feedback sea negativo(no le sirvio la resuesta) se manda a la pestaña de administracion para ser revisada
        public async Task FeedBackNegativo(string mensaje, string userManda) {

            AumentarIndiceReintento(userManda); //Aumentamos la cantidad de veces que el chatbot se equivoco
            MensajeRespuesta mensajeGetMessage = _chatBot.GetMessage(mensaje);
            RespuestaEquivocada respuestaEquivocada = new RespuestaEquivocada();
            if (mensajeGetMessage.Intents.Count() > 0 && mensajeGetMessage.Intents.First() != null)
            {  respuestaEquivocada.IntentAsignado = mensajeGetMessage.Intents.First().name;
            }
            respuestaEquivocada.Input = mensaje;
            _ABrespuestasEquivocadas.Agregar(respuestaEquivocada);

        }

        //Se ejecuta cuando un usuario solicita asistencia personalizada
        public async Task SolicitarAsistenciaPersonalizada(string userManda)
        {


            if (_getPacientes.ExistePaciente(userManda))
            {
                Paciente paciente = _getPacientes.GetPacientePorUsuario(userManda);
                //Enviamos la notificacion de asistencia a las recepcionistas
                _enviarNotificacion.EnviarATodosRecepcion("Solicitud de asistencia", paciente.Nombre + " esta solicitando asistencia personalizada por chat", "https://appteletonrecepcion.azurewebsites.net/Chat/Chat");
                if (_getChats.PacienteTieneChatAbierto(paciente.Id))
                {
                    //SI el paciente tiene un chat abierto lo actualiza
                    Chat chat = _getChats.GetChatAbiertoDePaciente(paciente.Id);
                    Mensaje mensaje = new Mensaje("Una recepcionista lo atenderá por esta u otra via lo antes posible, recuerde que el horario de atención personalizada es de 8am hasta las 5pm", "CHATBOT");
                    chat.AgregarMensajeBotRecepcion(mensaje);
                    chat.AsistenciaAutomatica = false; //desactiva el chatbot
                    _abChat.Actualizar(chat);
                }

            }
        }

        //Aumentamos la cantidad de veces que el chatbot se equivoco
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

        //Mostramos el formulario de asistencia personalizada
        private async void MostrarBotoneraAsistencia(string userManda)
        {

            string idConexion = UsuariosConectados.GetIdConexionDeUsuario(userManda);
            if (!String.IsNullOrEmpty(idConexion))
            {
                await Clients.Client(idConexion).SendAsync("MostrarBotoneraAsistencia");
            }

        }
        //Actualizamos los chats en la base de datos
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
