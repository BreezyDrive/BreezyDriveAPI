using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace BreezyDrive.ConversationServices.Application.Hubs
{
    public class ConversationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"User Connected: {Context.ConnectionId}, UserIdentifier: {Context.UserIdentifier}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine($"User Disconnected: {Context.ConnectionId}, UserIdentifier: {Context.UserIdentifier}, Error: {exception?.Message}");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
