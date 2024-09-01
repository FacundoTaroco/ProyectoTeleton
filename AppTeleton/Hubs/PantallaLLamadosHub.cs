using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.SignalR;

namespace AppTeleton.Hubs
{
    public class PantallaLLamadosHub:Hub
    {
        //Actualiza en tiempo real la pantalla de llamados medicos
        public async Task Send(LLamado llamado)
        {

            await Clients.All.SendAsync("NuevoLlamado", llamado);
        }
    }
}
