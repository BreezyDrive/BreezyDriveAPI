using AutoMapper;
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
        private readonly IMongoRepository<Conversation> _conversationRepository;
        private readonly IMapper _mapper;
        private readonly ConversationDbContext _dbContext;

        public ConversationService(IMongoUnitOfWork unitOfWork, IMapper mapper, ConversationDbContext dbContext)
        {
            _conversationRepository = unitOfWork.Repository<Conversation>("Conversations");
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

        public async Task<ConversationResponse> CreateConversation(ConversationRequest request)
        {
            //var existConversation = await _unitOfWork.Repository<Conversation>("Conversations");
            var existConversation = await _conversationRepository.GetAllAsync();

            var conversationCheck = existConversation
                .Where(n => n.UserId1 == request.UserId1 && n.UserId2 == request.UserId2
                    || n.UserId2 == request.UserId1 && n.UserId1 == request.UserId2)
                .FirstOrDefault();

            var newConversation = new Conversation
            {
                UserId1 = request.UserId1,
                UserId2 = request.UserId2,
            };

            await _conversationRepository.InsertAsync(newConversation);
            return _mapper.Map<ConversationResponse>(newConversation);

        }


        public async Task<ConversationResponse> GetConversationByID(Guid id)
        {
            var conversationList = await _conversationRepository.GetAllAsync();
            var conversatioId = conversationList
                .Where(c => c.Id == id)
                .FirstOrDefault();

            return _mapper.Map<ConversationResponse>(conversatioId);
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
