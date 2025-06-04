using Microsoft.AspNetCore.SignalR;

namespace BreezyDrive.ConversationServices.Application.Hubs
{
    public class ConversationHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier; // Lấy User Identifier từ CustomUserIdProvider
            Console.WriteLine($"NotificationHub: User connected with ID: {userId}");

            // Log thêm toàn bộ Claims để kiểm tra
            var claims = Context.User?.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
            Console.WriteLine($"NotificationHub: User Claims: {string.Join(", ", claims ?? new List<string>())}");

            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine($"User Disconnected: {Context.ConnectionId}, UserIdentifier: {Context.UserIdentifier}, Error: {exception?.Message}");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
