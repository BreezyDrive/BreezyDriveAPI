using AutoMapper;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.ConversationServices.Application.DTOs.Requests;
using BreezyDrive.ConversationServices.Application.DTOs.Responses;
using BreezyDrive.ConversationServices.Application.Interfaces;
using BreezyDrive.ConversationServices.Domain.Entities;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;
using MassTransit;

namespace BreezyDrive.ConversationServices.Application.Services
{
    public class ConversationMessageService : IConversationMessageService
    {
        private readonly IMongoRepository<ConversationMessage> _conversationMessageRepository;
        private readonly IMongoRepository<Conversation> _conversationRepository;
        private readonly IMapper _mapper;
        private readonly IRequestClient<CheckUserExistRequest> _requestClient;
        private readonly IMessageFileService _messageFileService;
        private readonly IConversationHubService _hubService;

        public ConversationMessageService(
            IMongoUnitOfWork unitOfWork, 
            IMapper mapper,
            IRequestClient<CheckUserExistRequest> requestClient,
            IMessageFileService messageFileService,
            IConversationHubService hubService)
        {
            _conversationMessageRepository = unitOfWork.Repository<ConversationMessage>("ConversationMessages");
            _conversationRepository = unitOfWork.Repository<Conversation>("Conversations");
            _mapper = mapper;
            _requestClient = requestClient;
            _messageFileService = messageFileService;
            _hubService = hubService;
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

        public async Task<ConversationMessageResponse> SendMessage(Guid conversationId, ConversationMessageRequest request)
        {
            // Check if sender exists using RabbitMQ
            var userCheckResponse = await _requestClient.GetResponse<CheckUserExistResponse>(
                new CheckUserExistRequest { UserId = request.SenderId });

            if (!userCheckResponse.Message.IsUserExists)
            {
                throw new CustomExceptions.DataNotFoundException("Người gửi không tồn tại");
            }

            // Lấy repository cho Conversation
            var conversation = await _conversationRepository.GetByIdAsync(conversationId.ToString());
            if (conversation == null ||
                (conversation.UserId1 != request.SenderId && conversation.UserId2 != request.SenderId))
            {
                throw new CustomExceptions.DataNotFoundException("Không có quyền gửi tin nhắn");
            }

            // Parse and validate ReplyToMessageId if provided
            if (request.ReplyToMessageId.HasValue)
            {
                var replyMessage = await _conversationMessageRepository.GetByIdAsync(request.ReplyToMessageId.Value.ToString());
                if (replyMessage == null || replyMessage.ConversationId != conversationId)
                {
                    throw new CustomExceptions.DataNotFoundException("Tin nhắn trả lời không tồn tại hoặc không thuộc cuộc trò chuyện này");
                }
            }

            // Tạo message mới
            var message = new ConversationMessage
            {
                ConversationId = conversationId,
                SenderId = request.SenderId,
                Content = request.Content,
                CreateTime = DateTimeOffset.Now,
                IsSeen = false,
                ReplyToMessageId = request.ReplyToMessageId
            };
            await _conversationMessageRepository.InsertAsync(message);

            // Upload files if any
            if (request.Files != null && request.Files.Any())
            {
                foreach (var file in request.Files)
                {
                    await _messageFileService.UploadFile(message.Id, file);
                }
            }

            // Cập nhật LastMessage của conversation
            conversation.LastMessage = request.Content;
            await _conversationRepository.UpdateAsync(conversationId.ToString(), conversation);

            var response = _mapper.Map<ConversationMessageResponse>(message);
            response.Files = await _messageFileService.GetMessageFiles(message.Id);

            // Gửi realtime qua hub service
            var receiverId = conversation.UserId1 == request.SenderId ? conversation.UserId2 : conversation.UserId1;
            await _hubService.SendMessageToUserAsync(conversationId, request.SenderId, request.Content, message.Id, message.CreateTime, receiverId);

            return response;
        }
    }
}
