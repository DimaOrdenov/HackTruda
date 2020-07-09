﻿using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace HackTruda.API
{
    public class ChatHub : Hub
    {
        public Task SendMessage(int userId, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", userId, message);
        }
    }
}