using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using AppTeleton.Models.Filtros;
using LogicaAplicacion.CasosUso.ChatCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaNegocio.Entidades;

namespace AppTeleton.Controllers
{
    [PacienteRecepcionistaLogueado]
    public class ChatController : Controller
    {

        private GetChats _getChats;
        private GetPacientes _getPacientes;
        public ChatController(GetChats getChats, GetPacientes getPacientes) { 
            _getChats = getChats;
            _getPacientes = getPacientes;
        }

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
    }
}
