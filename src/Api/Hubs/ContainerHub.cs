using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs
{
    public class ContainerHub : Hub
    {
        public async Task UpdateContainer(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
