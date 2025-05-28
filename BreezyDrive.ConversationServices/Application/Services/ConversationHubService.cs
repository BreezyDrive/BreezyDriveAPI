using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using BreezyDrive.ConversationServices.Application.Hubs;
using BreezyDrive.ConversationServices.Application.Interfaces;

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
        }
    }
} 