using Microsoft.AspNetCore.SignalR;

namespace BreezyDrive.NotificationServices.Application.Hubs
{
    public class NotificationHub : Hub
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


       /* public async Task SendNotification(Guid ReceiverId, string Name, string Description, NotificationType NotificationType)
        {
            Console.WriteLine($"📢 Gửi thông báo '{NotificationType}' tới User {ReceiverId}: {Description}");
            await Clients.User(ReceiverId.ToString()).SendAsync("ReceiveNotification", Name, Description, NotificationType.ToString());
        }*/
    }
}
