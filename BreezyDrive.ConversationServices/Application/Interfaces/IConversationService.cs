using BreezyDrive.ConversationServices.Application.DTOs.Requests;
using BreezyDrive.ConversationServices.Application.DTOs.Responses;
using System.Net;

namespace BreezyDrive.ConversationServices.Application.Interfaces
{
    public interface IConversationService
    {
        Task<ConversationResponse> GetConversationByID(Guid id);
        Task<ConversationResponse> CreateConversation(ConversationRequest request);
        Task<ConversationResponse> UpdateConversationById(Guid id, ConversationRequest request);
        Task<ConversationResponse> DeleteConversation(Guid id, ConversationRequest request);
        Task<object> InitializeDatabase();
    }
}
