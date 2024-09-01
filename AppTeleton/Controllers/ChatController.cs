using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using AppTeleton.Models.Filtros;
using LogicaAplicacion.CasosUso.ChatCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaNegocio.Entidades;
using LogicaAplicacion.Servicios;
using AppTeleton.Models;
using LogicaNegocio.EntidadesWit;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using LogicaAplicacion.CasosUso.RecepcionistaCU;
using LogicaNegocio.DTO;

namespace AppTeleton.Controllers
{
    //controller que maneja las acciones relacionadas al chat

    public class ChatController : Controller
    {

        private GetChats _getChats;
        private ABMChat _abmChat;
        private GetPacientes _getPacientes;
        private ChatBotService _chatBotService;
        private GetRespuestasEquivocadas _getRespuestasEquivocadas;
        private ABRespuestasEquivocadas _abRespuestasEquivocadas;
        private GetRecepcionistas _getRecepcionistas;
        public ChatController(ABMChat abmChat, ABRespuestasEquivocadas abrespuestasMal, GetRespuestasEquivocadas respuestasMal ,GetChats getChats, GetPacientes getPacientes, ChatBotService chatbot, GetRecepcionistas getRecepcionistas) { 
            _getChats = getChats;
            _getPacientes = getPacientes;
            _chatBotService = chatbot;
            _getRespuestasEquivocadas = respuestasMal;
            _abRespuestasEquivocadas = abrespuestasMal;
            _getRecepcionistas = getRecepcionistas; 
            _abmChat = abmChat;
        }
        [PacienteRecepcionistaLogueado] //Filtro para evitar que usuarios que no sean de los roles permitidos accedan a la vista
        [HttpGet]
        //carga vista prinicpal del chat tanto para pacientes como para administradores
        public IActionResult Chat()
        {
            try
            {
            string usuario = HttpContext.Session.GetString("USR");
            string tipo = HttpContext.Session.GetString("TIPO");
            ViewBag.Usuario = usuario;
            ViewBag.TipoUsuario = tipo;
            int idUsuario = 0;
            IEnumerable<Chat> chatsListado = new List<Chat>();
                if (tipo == "PACIENTE") //carga la vista para pacientes
                {
                    Paciente paciente = _getPacientes.GetPacientePorUsuario(usuario); //obtiene el paciente
                    idUsuario = paciente.Id;
                    if (_getChats.PacienteTieneChatAbierto(idUsuario)) //verifica si el paciente tiene un chat abierto
                    {
                        Chat chatAbierto =  _getChats.GetChatAbiertoDePaciente(idUsuario); //obtiene el chat abierto del paciente
                        if (chatAbierto._Recepcionista == null)
                        {
                            //si el chat no se da con ninguna recepcionista entonces se selecciona al chatbot
                            ViewBag.UsuarioRecibe = "CHATBOT";
                        }
                        else {
                            //Si el chat es con una recepcionista entonces se carga el nombre de esta
                            ViewBag.UsuarioRecibe = chatAbierto._Recepcionista.NombreUsuario;
                        }
                        //Se pasa el chat para cargarlo directamente en el codigo javascript
                        ViewBag.ChatCargar = chatAbierto;
                    }
                    else
                    {
                        //si no tiene ningun chat abierto el paciente entonces se le crea uno con el chatbot seleccionado por defecto
                        ViewBag.UsuarioRecibe = "CHATBOT";
                        ViewBag.ChatCargar = new Chat(paciente);
                    }
                    //todos los chats cerrados del paciente
                    chatsListado = _getChats.GetChatsDePaciente(idUsuario);
                }
                else if (tipo == "RECEPCIONISTA") {//una recepcionista entra a la vista del chat

                    Recepcionista recepcionista = _getRecepcionistas.GetRecepcionistaPorUsuario(usuario); // se obtiene la recepcionista
                    idUsuario = recepcionista.Id;
                    IEnumerable<Chat> chatsRecepcionista = _getChats.GetChatsDeRecepcionista(idUsuario); //todos los chats de la recepcionista
                    IEnumerable<Chat> chatsSinAsistencia = _getChats.GetChatsQueSolicitaronAsistenciaNoAtendidos(); //todos los chats que solicitaron asistencia pero no fueron tomados por ninguna recepcionista
                    chatsListado = chatsRecepcionista.Concat(chatsSinAsistencia); //se juntan ambas listas de chats

                    ViewBag.ChatCargar = new Chat();
                }

                chatsListado = chatsListado.OrderByDescending(c => c.FechaApertura).ToList();

                return View(chatsListado);
            }
            catch (Exception)
            {

                return View();
            }
           
        }



        [PacienteRecepcionistaLogueado]
        [HttpGet]
        //Carga un chat cerrado o un chat que haya solicitado asistencia segun el tipo de usuario que solicite la accion
        public IActionResult CargarChatCerrado(int idChat) {

            string usuario = HttpContext.Session.GetString("USR");
            int idUsuario = 0;  
            ViewBag.Usuario = usuario;
            ViewBag.TipoUsuario = HttpContext.Session.GetString("TIPO");
            IEnumerable<Chat> chatsListado = new List<Chat>();

            if (HttpContext.Session.GetString("TIPO") == "PACIENTE") //un paciente carga un chat cerrado
            {
                Paciente paciente = _getPacientes.GetPacientePorUsuario(usuario);
                idUsuario = paciente.Id;
                Chat chatACargar = _getChats.GetChatPorId(idChat); //se obtiene el chat por id
                if (chatACargar._Recepcionista == null)
                {
                    ViewBag.UsuarioRecibe = "CHATBOT";
                }
                else
                {
                    ViewBag.UsuarioRecibe = chatACargar._Recepcionista.NombreUsuario;
                }
                ViewBag.ChatCargar = chatACargar; //se manda el chat para ser cargado desde el codigo Javascript
                chatsListado = _getChats.GetChatsDePaciente(idUsuario);

            }
            else if (HttpContext.Session.GetString("TIPO") == "RECEPCIONISTA") {//una recepcionista carga un chat que solicito asistencia

                Recepcionista recepcionista = _getRecepcionistas.GetRecepcionistaPorUsuario(usuario);
                idUsuario = recepcionista.Id;
                IEnumerable<Chat> chatsRecepcionista = _getChats.GetChatsDeRecepcionista(idUsuario);
                IEnumerable<Chat> chatsSinAsistencia = _getChats.GetChatsQueSolicitaronAsistenciaNoAtendidos();

                Chat chatActivo = _getChats.GetChatPorId(idChat); //se obtiene el chat por id
                if (chatActivo._Recepcionista == null) { 

                    chatActivo._Recepcionista = recepcionista;//Si no fue tomado por ninguna recepcionista entonces se le asigna a la recepcionista que lo solicito
                    _abmChat.Actualizar(chatActivo);
                    
                }

                ViewBag.UsuarioRecibe = chatActivo._Paciente.NombreUsuario;
                ViewBag.ChatCargar = chatActivo;
                chatsListado = chatsRecepcionista.Concat(chatsSinAsistencia);

            }
            chatsListado = chatsListado.OrderByDescending(c => c.FechaApertura).ToList();
            return View("Chat", chatsListado);

        }

        [RecepcionistaAdminLogueado]
        [HttpGet]
        //vista de administracion del chatbot para entrenamiento asistido negativo
        public async Task<IActionResult> AdministracionBot()
        {

            try
            {
                IEnumerable<RespuestaEquivocada> respuestas = _getRespuestasEquivocadas.GetAll();
                IEnumerable<Intent> intents = _chatBotService.GetIntent();
                AdministracionBotViewModel vm = new AdministracionBotViewModel();
                vm.intents = intents;
                vm.respuestas = respuestas;
                
                return View(vm);
            }
            catch (Exception)
            {

                throw;
            }

            
        }


        [RecepcionistaAdminLogueado]
        [HttpPost]
        //manda una frase de entrenamiento al chatbot cuando se agrega una pregunta frecuente
        public async Task<IActionResult> AgregarUtterance(string input, string intentname, int idRespuesta) {

            try
            {
              
                Utterance utterance = new Utterance();    
                utterance.text = input;
                utterance.intent = intentname;
                utterance.traits = new List<UtteranceTrait>();
                utterance.entities = new List<UtteranceEntity>();   

                List<Utterance> utterances = new List<Utterance> { utterance };

                _chatBotService.PostUtterance(utterances);

                _abRespuestasEquivocadas.Borrar(idRespuesta);

                return RedirectToAction("AdministracionBot");
                
            }
            catch (Exception)
            {

                return RedirectToAction("AdministracionBot");
            }
        
        }
        [RecepcionistaAdminLogueado]
        [HttpPost]
        //elimina un mensaje equivocado en la vista de administrar chatbot
        public IActionResult EliminarMensajeEquivocado(int idMensaje) {
            try
            {
                _abRespuestasEquivocadas.Borrar(idMensaje);
                return RedirectToAction("AdministracionBot");
            }
            catch (Exception)
            {

                return RedirectToAction("AdministracionBot");
            }
        
        }

    }
}
