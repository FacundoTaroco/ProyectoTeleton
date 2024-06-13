using Microsoft.AspNetCore.SignalR;
namespace AppTeleton.Hubs
{
    public class HubConectado:Hub
    {
        public async Task SendMessage(string user, string message) { 

            await Clients.All.SendAsync("MensajeRecibido", user, message); 

        }


    }
}
