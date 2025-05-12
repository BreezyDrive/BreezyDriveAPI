using BreezyDrive.ConversationServices.Application.DTOs.Requests;
using BreezyDrive.ConversationServices.Application.Interfaces;
using CoreApiResponse;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BreezyDrive.ConversationServices.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationController : BaseController
    {
        private readonly IConversationService _conversationService;
        public ConversationController (IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        [SwaggerOperation(Summary = "Get All Conversation For System")]
        [HttpGet("GetAllConversation")]
        public async Task<IActionResult> GetAllConversations()
        {
            var conversations = await _conversationService.GetAllConversations();
            return CustomResult("Lấy dữ liệu thành công", conversations);
        }
        
        
        [SwaggerOperation(Summary = "Get Conversation Id For System")]
        [HttpGet("GetConversation/{id}")]
        public async Task<IActionResult> GetConversationByID(Guid id)
        {
            var conversations = await _conversationService.GetConversationByID(id);
            return CustomResult("Lấy dữ liệu thành công", conversations);
        }
        

        [SwaggerOperation(Summary = "Create Conversation For System")]
        [HttpPost("CreateConversation")]
        public async Task<IActionResult> CreateConversation(ConversationRequest request)
        {
            var conversations = await _conversationService.CreateConversation(request);
            return CustomResult("Tạo dữ liệu thành công", conversations);
        }


        [SwaggerOperation(Summary = "Update Conversation Ids For System")]
        [HttpPatch("UpdateConversation/{id}")]
        public async Task<IActionResult> UpdateConversationById(Guid id, ConversationRequest request)
        {
            var conversations = await _conversationService.UpdateConversationById(id, request);
            return CustomResult("Cập nhật dữ liệu thành công", conversations);
        }

        //[SwaggerOperation(Summary = "Thu hồi Conversation Id For System")]
        //[HttpPatch("DeleteConversation/{id}")]
        //public async Task<IActionResult> DeleteConversation (Guid id, ConversationRequest request)
        //{
        //    var conversations = await _conversationService.DeleteConversation(id, request);
        //    return CustomResult("Cập nhật dữ liệu thành công", conversations);
        //}

         
        [SwaggerOperation(Summary = "Khởi tạo database MongoDB")]
        [HttpPost("initialize-database")]
        public async Task<IActionResult> InitializeDatabase()
        {
            try
            {
                var result = await _conversationService.InitializeDatabase();
                return CustomResult("Khởi tạo database thành công", result);
            }
            catch (Exception ex)
            {
                return CustomResult("Khởi tạo database thất bại", ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
