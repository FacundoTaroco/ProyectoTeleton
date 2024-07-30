using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.SignalR;

namespace AppTeleton.Hubs
{
    public class PantallaLLamadosHub:Hub
    {
        public async Task Send(LLamado llamado)
        {

            await Clients.All.SendAsync("NuevoLlamado", llamado);
        }
    }
}
