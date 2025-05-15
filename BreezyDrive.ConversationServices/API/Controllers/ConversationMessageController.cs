using BreezyDrive.ConversationServices.Application.DTOs.Requests;
using BreezyDrive.ConversationServices.Application.Interfaces;
using BreezyDrive.ConversationServices.Application.Messaging;
using CoreApiResponse;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BreezyDrive.ConversationServices.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationMessageController : BaseController
    {
        private readonly IConversationMessageService _conversationMessageService;
        private readonly IRequestClient<CheckUserExistRequest> _requestClient;
        public ConversationMessageController(IConversationMessageService conversationMessageService, IRequestClient<CheckUserExistRequest> requestClient)
        {
            _conversationMessageService = conversationMessageService;
            _requestClient = requestClient;
        }

        // Hiện tại done
        [SwaggerOperation(Summary = "Get All List Conversation For System")]
        [HttpGet("GetAllConversationMessages")]
        public async Task<IActionResult> GetAllConversationMessages()
        {
            var converstion = await _conversationMessageService.GetAllConversationMessages();
            return CustomResult("Lấy dữ liệu thành công", converstion);
        }

        [SwaggerOperation(Summary = "SendMessage to conversation")]
        [HttpPost("SendMessage/{conversationId}")]
        public async Task<IActionResult> SendMessage([FromRoute] Guid conversationId, [FromBody] ConversationMessageRequest request)
        {
            var sendMessage = await _conversationMessageService.SendMessage(conversationId, request);
            return CustomResult("Gửi Thành Công", sendMessage);
        }

        //test rabbitmq
        [HttpGet("CheckIfUserExist/{id}")]
        public async Task<IActionResult> CheckIfUserExist(Guid id)
        {
            var response = await _requestClient.GetResponse<CheckUserExistResponse>(
                new CheckUserExistRequest { UserId = id });

            return CustomResult("Success", response);
        }
    }
}
