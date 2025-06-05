using BreezyDrive.ConversationServices.Application.DTOs.Requests;
using BreezyDrive.ConversationServices.Application.Interfaces;
using CoreApiResponse;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BreezyDrive.ConversationServices.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationMessageController : BaseController
    {
        private readonly IConversationMessageService _conversationMessageService;
        private readonly IMessageFileService _messageFileService;
        private readonly IRequestClient<CheckUserExistRequestEvent> _requestClient;

        public ConversationMessageController(
            IConversationMessageService conversationMessageService,
            IMessageFileService messageFileService,
            IRequestClient<CheckUserExistRequestEvent> requestClient)
        {
            _conversationMessageService = conversationMessageService;
            _messageFileService = messageFileService;
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
        public async Task<IActionResult> SendMessage([FromRoute] Guid conversationId, [FromForm] ConversationMessageRequest request)
        {
            var sendMessage = await _conversationMessageService.SendMessage(conversationId, request);
            return CustomResult("Gửi Thành Công", sendMessage);
        }

        //test rabbitmq
        [HttpGet("CheckIfUserExist/{id}")]
        public async Task<IActionResult> CheckIfUserExist(Guid id)
        {
            var response = await _requestClient.GetResponse<CheckUserExistResponse>(
                new CheckUserExistRequestEvent { UserId = id });

            return CustomResult("Success", response);
        }

        [SwaggerOperation(Summary = "Download message file")]
        [HttpGet("DownloadFile/{fileId}")]
        public async Task<IActionResult> DownloadFile(Guid fileId)
        {
            var fileBytes = await _messageFileService.DownloadFile(fileId);
            return File(fileBytes, "application/octet-stream");
        }

        [SwaggerOperation(Summary = "Delete message file")]
        [HttpDelete("DeleteFile/{fileId}")]
        public async Task<IActionResult> DeleteFile(Guid fileId)
        {
            await _messageFileService.DeleteFile(fileId);
            return CustomResult("File deleted successfully");
        }

        [SwaggerOperation(Summary = "Get message files")]
        [HttpGet("GetMessageFiles/{messageId}")]
        public async Task<IActionResult> GetMessageFiles(Guid messageId)
        {
            var files = await _messageFileService.GetMessageFiles(messageId);
            return CustomResult("Files retrieved successfully", files);
        }
    }
}
