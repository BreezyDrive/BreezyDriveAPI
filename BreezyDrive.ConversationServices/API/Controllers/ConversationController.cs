using BreezyDrive.ConversationServices.Application.DTOs.Requests;
using BreezyDrive.ConversationServices.Application.Interfaces;
using CoreApiResponse;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// Get All Conversation For System
        /// </summary>
        /// <returns></returns>
        ///

        [HttpGet("GetAllConversation")]
        public async Task<IActionResult> GetAllConversations()
        {
            var conversations = await _conversationService.GetAllConversations();
            return CustomResult("Lấy dữ liệu thành công", conversations);
        }

        /// <summary>
        /// Get Conversation Id For System
        /// </summary>
        /// <returns></returns>
        ///

        [HttpGet("GetConversation/{id}")]
        public async Task<IActionResult> GetConversationByID(Guid id)
        {
            var conversations = await _conversationService.GetConversationByID(id);
            return CustomResult("Lấy dữ liệu thành công", conversations);
        }

        /// <summary>
        /// Create Conversation For System
        /// </summary>
        /// <returns></returns>
        ///

        [HttpPost("CreateConversation")]
        public async Task<IActionResult> CreateConversation(ConversationRequest request)
        {
            var conversations = await _conversationService.CreateConversation(request);
            return CustomResult("Tạo dữ liệu thành công", conversations);
        }

        /// <summary>
        /// Update Conversation Ids For System
        /// </summary>
        /// <returns></returns>
        ///

        [HttpPatch("UpdateConversation/{id}")]
        public async Task<IActionResult> UpdateConversationById(Guid id, ConversationRequest request)
        {
            var conversations = await _conversationService.UpdateConversationById(id, request);
            return CustomResult("Cập nhật dữ liệu thành công", conversations);
        }

        /// <summary>
        /// Thu hồi Conversation Id For System
        /// </summary>
        /// <returns></returns>
        ///

        [HttpPatch("DeleteConversation/{id}")]
        public async Task<IActionResult> DeleteConversation (Guid id, ConversationRequest request)
        {
            var conversations = await _conversationService.DeleteConversation(id, request);
            return CustomResult("Cập nhật dữ liệu thành công", conversations);
        }
    }
}
