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
        [PacienteRecepcionistaLogueado]
        [HttpGet]
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
                if (tipo == "PACIENTE")
                {
                    Paciente paciente = _getPacientes.GetPacientePorUsuario(usuario);
                    idUsuario = paciente.Id;
                    if (_getChats.PacienteTieneChatAbierto(idUsuario))
                    {
                        Chat chatAbierto =  _getChats.GetChatAbiertoDePaciente(idUsuario);
                        if (chatAbierto._Recepcionista == null)
                        {
                            ViewBag.UsuarioRecibe = "CHATBOT";
                        }
                        else {
                            ViewBag.UsuarioRecibe = chatAbierto._Recepcionista.NombreUsuario;
                        }
                        ViewBag.ChatCargar = chatAbierto;
                    }
                    else
                    {
                        ViewBag.UsuarioRecibe = "CHATBOT";
                        ViewBag.ChatCargar = new Chat(paciente);
                    }

                    chatsListado = _getChats.GetChatsDePaciente(idUsuario);
                }
                else if (tipo == "RECEPCIONISTA") {
                    Recepcionista recepcionista = _getRecepcionistas.GetRecepcionistaPorUsuario(usuario);
                    idUsuario = recepcionista.Id;
                    IEnumerable<Chat> chatsRecepcionista = _getChats.GetChatsDeRecepcionista(idUsuario);
                    IEnumerable<Chat> chatsSinAsistencia = _getChats.GetChatsQueSolicitaronAsistenciaNoAtendidos();
                    chatsListado = chatsRecepcionista.Concat(chatsSinAsistencia);

                    ViewBag.ChatCargar = new Chat();
                }

               
                return View(chatsListado);
            }
            catch (Exception)
            {

                return View();
            }
           
        }



        [PacienteRecepcionistaLogueado]
        [HttpGet]
        public IActionResult CargarChatCerrado(int idChat) {

            string usuario = HttpContext.Session.GetString("USR");
            int idUsuario = 0;  
            ViewBag.Usuario = usuario;
            ViewBag.TipoUsuario = HttpContext.Session.GetString("TIPO");
            IEnumerable<Chat> chatsListado = new List<Chat>();

            if (HttpContext.Session.GetString("TIPO") == "PACIENTE")
            {
                Paciente paciente = _getPacientes.GetPacientePorUsuario(usuario);
                idUsuario = paciente.Id;
                Chat chatACargar = _getChats.GetChatPorId(idChat);
                if (chatACargar._Recepcionista == null)
                {
                    ViewBag.UsuarioRecibe = "CHATBOT";
                }
                else
                {
                    ViewBag.UsuarioRecibe = chatACargar._Recepcionista.NombreUsuario;
                }
                ViewBag.ChatCargar = chatACargar;
                chatsListado = _getChats.GetChatsDePaciente(idUsuario);

            }
            else if (HttpContext.Session.GetString("TIPO") == "RECEPCIONISTA") {

                Recepcionista recepcionista = _getRecepcionistas.GetRecepcionistaPorUsuario(usuario);
                idUsuario = recepcionista.Id;
                IEnumerable<Chat> chatsRecepcionista = _getChats.GetChatsDeRecepcionista(idUsuario);
                IEnumerable<Chat> chatsSinAsistencia = _getChats.GetChatsQueSolicitaronAsistenciaNoAtendidos();

                Chat chatActivo = _getChats.GetChatPorId(idChat);
                if (chatActivo._Recepcionista == null) {

                    chatActivo._Recepcionista = recepcionista;
                    _abmChat.Actualizar(chatActivo);
                    
                }

                ViewBag.UsuarioRecibe = chatActivo._Paciente.NombreUsuario;
                ViewBag.ChatCargar = chatActivo;
                chatsListado = chatsRecepcionista.Concat(chatsSinAsistencia);

            }
            return View("Chat", chatsListado);

        }

        [RecepcionistaAdminLogueado]
        [HttpGet]
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
