using LogicaNegocio.DTO;
using Microsoft.AspNetCore.SignalR;

namespace AppTeleton.Hubs
{
    public class ActualizarListadoHub:Hub
    {
        //Actualiza en tiempo real el listado de citas de recepcionistas y administradores
        public async Task Send(IEnumerable<CitaMedicaDTO> citas) {

            await Clients.All.SendAsync("ActualizarListado", citas);
        }
    }
}
