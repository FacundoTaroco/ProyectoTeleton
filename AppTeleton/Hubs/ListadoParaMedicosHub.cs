using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.SignalR;

namespace AppTeleton.Hubs
{
    public class ListadoParaMedicosHub:Hub
    {
        //Actualiza en tiempo real los listados de medicos
        public async Task Send(IEnumerable<CitaMedicaDTO> citas)
        {

            await Clients.All.SendAsync("ActualizarListado", citas);
        }
    }
}
