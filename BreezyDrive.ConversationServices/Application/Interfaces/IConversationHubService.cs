using System;
using System.Threading.Tasks;

namespace BreezyDrive.ConversationServices.Application.Interfaces
{
    public interface IConversationHubService
    {
        Task SendMessageToUserAsync(Guid conversationId, Guid senderId, string content, Guid messageId, DateTimeOffset createTime, Guid receiverId);
    }
} 