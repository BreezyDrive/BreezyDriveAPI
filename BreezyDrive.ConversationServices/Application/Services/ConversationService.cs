using AutoMapper;
using BreezyDrive.Common.Domain.Interfaces;
using BreezyDrive.ConversationServices.Application.DTOs.Requests;
using BreezyDrive.ConversationServices.Application.DTOs.Responses;
using BreezyDrive.ConversationServices.Application.Interfaces;
using BreezyDrive.ConversationServices.Domain.Entities;
using BreezyDrive.Domain.Exceptions;

namespace BreezyDrive.ConversationServices.Application.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConversationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ConversationResponse>> GetAllConversations()
        {
            var conversation = await _unitOfWork.Repository<Conversation>().GetAsync();
            if (!conversation.Any())
            {
                throw new CustomExceptions.DataNotFoundException("Không tìm thấy dữ liệu");
            }
            var conversationResponse = _mapper.Map<List<ConversationResponse>>(conversation);
            return conversationResponse;
        }

        public async Task<ConversationResponse> GetConversationByID(Guid id)
        {
            var conversation = await _unitOfWork.Repository<Conversation>().GetByIdAsync(id);
            if (conversation == null)
            {
                throw new CustomExceptions.DataNotFoundException("Không tìm thấy dữ liệus");
            }

            var conversationResponse = _mapper.Map<ConversationResponse>(conversation);

            return conversationResponse;
        }

        public async Task<ConversationResponse> CreateConversation(ConversationRequest request)
        {
            return null;
        }

        public async Task<ConversationResponse> UpdateConversationById(Guid id, ConversationRequest request)
        {
            return null;
        }

        public async Task<ConversationResponse> DeleteConversation(Guid id, ConversationRequest request)
        {
            return null;
        }

        
    }
}
