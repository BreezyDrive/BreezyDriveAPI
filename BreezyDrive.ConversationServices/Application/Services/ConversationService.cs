using AutoMapper;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.ConversationServices.Application.DTOs.Requests;
using BreezyDrive.ConversationServices.Application.DTOs.Responses;
using BreezyDrive.ConversationServices.Application.Interfaces;
using BreezyDrive.ConversationServices.Domain.Entities;
using BreezyDrive.ConversationServices.Infrastructure.Persistance;

namespace BreezyDrive.ConversationServices.Application.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IMongoUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ConversationDbContext _dbContext;

        public ConversationService(IMongoUnitOfWork unitOfWork, IMapper mapper, ConversationDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<object> InitializeDatabase()
        {
            try
            {
                // Verify the connection and collections
                var conversations = _dbContext.Conversations;
                var messages = _dbContext.ConversationMessages;
                var files = _dbContext.MessageFiles;

                return new
                {
                    Status = "Success",
                    Message = "MongoDB database and collections initialized successfully",
                    Collections = new[]
                    {
                        "Conversations",
                        "ConversationMessages",
                        "MessageFiles"
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to initialize database: {ex.Message}");
            }
        }

        public async Task<List<ConversationResponse>> GetAllConversations()
        {
            var conversationRepository = _unitOfWork.Repository<Conversation>("Conversations");
            var conversations = await conversationRepository.GetAllAsync();

            if (!conversations.Any())
            {
                throw new CustomExceptions.DataNotFoundException("Không tìm thấy dữ liệu");
            }

            return _mapper.Map<List<ConversationResponse>>(conversations);
        }

        public Task<ConversationResponse> GetConversationByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ConversationResponse> CreateConversation(ConversationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ConversationResponse> DeleteConversation(Guid id, ConversationRequest request)
        {
            throw new NotImplementedException();
        }
        
        

        public Task<ConversationResponse> UpdateConversationById(Guid id, ConversationRequest request)
        {
            throw new NotImplementedException();
        }

        //public async Task<List<ConversationResponse>> GetAllConversations()
        //{
        //    var conversationRepository = _unitOfWork.Repository<Conversation>("Conversations");
        //    var conversations = await conversationRepository.GetAllAsync();

        //    if (!conversations.Any())
        //    {
        //        throw new CustomExceptions.DataNotFoundException("Không tìm thấy dữ liệu");
        //    }

        //    return _mapper.Map<List<ConversationResponse>>(conversations);
        //}

        //public async Task<ConversationResponse> GetConversationByID(Guid id)
        //{
        //    var conversationRepository = _unitOfWork.Repository<Conversation>("Conversations");
        //    var conversation = await conversationRepository.GetByIdAsync(id);

        //    if (conversation == null)
        //    {
        //        throw new CustomExceptions.DataNotFoundException("Không tìm thấy dữ liệu");
        //    }

        //    return _mapper.Map<ConversationResponse>(conversation);
        //}

        //public async Task<ConversationResponse> CreateConversation(ConversationRequest request)
        //{
        //    var conversationRepository = _unitOfWork.Repository<Conversation>("Conversations");
        //    var conversation = _mapper.Map<Conversation>(request);

        //    await conversationRepository.InsertAsync(conversation);

        //    return _mapper.Map<ConversationResponse>(conversation);
        //}

        //public async Task<ConversationResponse> UpdateConversationById(Guid id, ConversationRequest request)
        //{
        //    var conversationRepository = _unitOfWork.Repository<Conversation>("Conversations");
        //    var existingConversation = await conversationRepository.GetByIdAsync(id);

        //    if (existingConversation == null)
        //    {
        //        throw new CustomExceptions.DataNotFoundException("Không tìm thấy dữ liệu");
        //    }

        //    _mapper.Map(request, existingConversation);
        //    await conversationRepository.UpdateAsync(existingConversation);

        //    return _mapper.Map<ConversationResponse>(existingConversation);
        //}

        //public async Task<ConversationResponse> DeleteConversation(Guid id, ConversationRequest request)
        //{
        //    var conversationRepository = _unitOfWork.Repository<Conversation>("Conversations");
        //    var existingConversation = await conversationRepository.GetByIdAsync(id);

        //    if (existingConversation == null)
        //    {
        //        throw new CustomExceptions.DataNotFoundException("Không tìm thấy dữ liệu");
        //    }

        //    await conversationRepository.DeleteAsync(existingConversation);

        //    return _mapper.Map<ConversationResponse>(existingConversation);
        //}
    }
}
