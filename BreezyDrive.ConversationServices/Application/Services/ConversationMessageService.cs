using AutoMapper;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.ConversationServices.Application.DTOs.Responses;
using BreezyDrive.ConversationServices.Application.Interfaces;
using BreezyDrive.ConversationServices.Domain.Entities;

namespace BreezyDrive.ConversationServices.Application.Services
{
    public class ConversationMessageService : IConversationMessageService
    {
        private readonly IMongoRepository<ConversationMessage> _conversationMessageRepository;
        private readonly IMapper _mapper;

        public ConversationMessageService(IMongoUnitOfWork unitOfWork, IMapper mapper)
        {
            _conversationMessageRepository = unitOfWork.Repository<ConversationMessage>("ConversationMessages");
            _mapper = mapper;
        }

        public async Task<List<ConversationMessageResponse>> GetAllConversationMessages()
        {
            var conversationList = await _conversationMessageRepository.GetAllAsync();

            if (!conversationList.Any())
            {
                throw new CustomExceptions.DataNotFoundException("Không tìm thấy dữ liệu");
            }

            var filtered = conversationList
                .OrderByDescending(n => n.CreateTime)
                .ToList();

            return _mapper.Map<List<ConversationMessageResponse>>(conversationList);
        }
    }
}
