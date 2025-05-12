using BreezyDrive.ConversationServices.Application.Interfaces;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BreezyDrive.ConversationServices.API.Controllers
{
    public class ConversationMessageController : BaseController
    {

        private readonly IConversationMessageService _conversationMessageService;
        public ConversationMessageController(IConversationMessageService conversationMessageService)
        {
            _conversationMessageService = conversationMessageService;
        }

        // Hiện tại done
        [SwaggerOperation(Summary = "Get All List Conversation For System")]
        [HttpGet("GetAllConversationMessages")]
        public async Task<IActionResult> GetAllConversationMessages()
        {
            var converstion = await _conversationMessageService.GetAllConversationMessages();
            return CustomResult("Lấy dữ liệu thành công", converstion);
        }
    }
}
