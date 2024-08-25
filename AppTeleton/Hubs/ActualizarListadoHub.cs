using LogicaNegocio.DTO;
using Microsoft.AspNetCore.SignalR;

namespace AppTeleton.Hubs
{
    public class ActualizarListadoHub:Hub
    {

        public async Task Send(IEnumerable<CitaMedicaDTO> citas) {

            await Clients.All.SendAsync("ActualizarListado", citas);
        }
    }
}
