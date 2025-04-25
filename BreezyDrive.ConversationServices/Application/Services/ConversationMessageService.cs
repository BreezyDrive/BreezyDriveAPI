using AutoMapper;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.ConversationServices.Application.Interfaces;
using BreezyDrive.ConversationServices.Infrastructure.Repositories;

namespace BreezyDrive.ConversationServices.Application.Services
{
    public class ConversationMessageService : IConversationMessageService
    {
        private readonly IMongoUnitiOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConversationMessageService(IMongoUnitiOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    }
}
