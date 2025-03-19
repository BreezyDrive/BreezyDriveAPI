using AutoMapper;
using BreezyDrive.Common.Domain.Interfaces;
using BreezyDrive.ConversationServices.Application.Interfaces;

namespace BreezyDrive.ConversationServices.Application.Services
{
    public class ConversationMessageService : IConversationMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConversationMessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    }
}
