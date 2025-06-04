using BreezyDrive.ConversationServices.Application.Hubs;
using BreezyDrive.ConversationServices.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace BreezyDrive.ConversationServices.Application.Services
{
    public class ConversationHubService : IConversationHubService
    {
        private readonly IHubContext<ConversationHub> _hubContext;

        public ConversationHubService(IHubContext<ConversationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessageToUserAsync(Guid conversationId, Guid senderId, string content, Guid messageId, DateTimeOffset createTime, Guid receiverId)
        {
            var messageData = new
            {
                conversationId,
                senderId,
                content,
                messageId,
                createTime
            };
            // Gửi cho cả sender và receiver
            await _hubContext.Clients.Users(new[] { senderId.ToString(), receiverId.ToString() })
                .SendAsync("ReceiveMessage", messageData);

            Console.WriteLine($"Received message from client: ConversationId={conversationId}, SenderId={senderId}, Content={content}");
        }
    }
} 