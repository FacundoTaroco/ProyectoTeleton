using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;


namespace LogicaAplicacion.Servicios
{
    public class UserActivityHub : Hub
    {
        public async Task UserClicked()
        {
            // Notificar a todos los clientes que el usuario ha hecho clic
            await Clients.All.SendAsync("ReceiveUserClicked");
        }
    }
}
