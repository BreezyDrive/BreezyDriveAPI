using BreezyDrive.ConversationServices.Application.DTOs.Responses;
using System.Net;

namespace BreezyDrive.ConversationServices.Application.Interfaces
{
    public interface IConversationMessageService
    {
        Task<List<ConversationMessageResponse>> GetAllConversationMessages();
    }
}
