using AutoMapper;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.ConversationServices.Application.Interfaces;
using BreezyDrive.ConversationServices.Domain.Entities;

namespace BreezyDrive.ConversationServices.Application.Services
{
    public class ConversationMessageService : IConversationMessageService
    {
        private readonly IMongoUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConversationMessageService(IMongoUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //public async Task<ConversationMessage> CreateMessage(ConversationMessage message)
        //{
        //    var messageRepository = _unitOfWork.Repository<ConversationMessage>("ConversationMessages");
        //    await messageRepository.InsertAsync(message);
        //    return message;
        //}

        //public async Task<List<ConversationMessage>> GetMessagesByConversationId(Guid conversationId)
        //{
        //    var messageRepository = _unitOfWork.Repository<ConversationMessage>("ConversationMessages");
        //    var messages = await messageRepository.GetAllAsync();
        //    return messages.Where(m => m.ConverationId == conversationId).ToList();
        //}

        //public async Task<ConversationMessage> UpdateMessage(ConversationMessage message)
        //{
        //    var messageRepository = _unitOfWork.Repository<ConversationMessage>("ConversationMessages");
        //    await messageRepository.UpdateAsync(message);
        //    return message;
        //}

        //public async Task DeleteMessage(Guid messageId)
        //{
        //    var messageRepository = _unitOfWork.Repository<ConversationMessage>("ConversationMessages");
        //    var message = await messageRepository.GetByIdAsync(messageId);
        //    if (message != null)
        //    {
        //        await messageRepository.DeleteAsync(message);
        //    }
        //}
    }
}
