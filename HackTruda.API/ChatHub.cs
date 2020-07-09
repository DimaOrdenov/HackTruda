using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace HackTruda.API
{
    public class ChatHub : Hub
    {
        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}