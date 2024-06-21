using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using AppTeleton.Models.Filtros;
using LogicaAplicacion.CasosUso.ChatCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaNegocio.Entidades;
using LogicaAplicacion.Servicios;
using LogicaNegocio.EntidadesWit.Entrenamiento;
using AppTeleton.Models;
using LogicaNegocio.EntidadesWit;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;

namespace AppTeleton.Controllers
{
    
    public class ChatController : Controller
    {

        private GetChats _getChats;
        private GetPacientes _getPacientes;
        private ChatBotService _chatBotService;
        private GetRespuestasEquivocadas _getRespuestasEquivocadas;
        private ABRespuestasEquivocadas _abRespuestasEquivocadas;
        public ChatController(ABRespuestasEquivocadas abrespuestasMal, GetRespuestasEquivocadas respuestasMal ,GetChats getChats, GetPacientes getPacientes, ChatBotService chatbot) { 
            _getChats = getChats;
            _getPacientes = getPacientes;
            _chatBotService = chatbot;
            _getRespuestasEquivocadas = respuestasMal;
            _abRespuestasEquivocadas = abrespuestasMal;
        }
        [PacienteRecepcionistaLogueado]
        [HttpGet]
        public IActionResult Chat()
        {
            string usuario = HttpContext.Session.GetString("USR");
            ViewBag.Usuario = usuario;

            if (HttpContext.Session.GetString("TIPO") == "PACIENTE") {
                Paciente paciente = _getPacientes.GetPacientePorUsuario(usuario);
                if (_getChats.PacienteTieneChatAbierto(paciente.Id)) {
                    ViewBag.ChatCargar = _getChats.GetChatAbiertoDePaciente(paciente.Id);
                }
            }
            return View();
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
              
                UtteranceDTO utterance = new UtteranceDTO();    
                utterance.text = input;
                utterance.intent = intentname;
                utterance.traits = new List<UtteranceTrait>();
                utterance.entities = new List<UtteranceEntity>();   

                List<UtteranceDTO> utterances = new List<UtteranceDTO> { utterance };

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
