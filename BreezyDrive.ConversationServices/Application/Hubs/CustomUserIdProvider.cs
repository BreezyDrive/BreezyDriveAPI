using Microsoft.AspNetCore.SignalR;

namespace BreezyDrive.ConversationServices.Application.Hubs
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            var claims = connection.User?.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
            //Console.WriteLine($"Claims: {string.Join(", ", claims)}");

            var userId = connection.User?.FindFirst("id")?.Value;

            //Console.WriteLine($"CustomUserIdProvider: Retrieved UserId = {userId}");

            return userId;
        }

    }

}
