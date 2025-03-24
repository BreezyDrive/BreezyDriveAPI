using BreezyDrive.NotificationServices.Application.Interfaces;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.NotificationServices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : BaseController
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("get-notification-by-receiverId/{ReceiverId}")]
        public async Task<IActionResult> GetNotification(Guid ReceiverId)
        {
            var notification = await _notificationService.GetNotificationByUser(ReceiverId);
            return CustomResult("Tải dữ liệu thành công.", notification);
        }

        [HttpPut("update-isseen/{id}")]
        public async Task<IActionResult> UpdateIsSeenStatus(Guid id)
        {

             await _notificationService.UpdateIsSeenStatus(id);
             return CustomResult("Cập nhật trạng thái thông báo thành công.", null);
        }

        [HttpPut("update-all-isseen/{receiverId}")]
        public async Task<IActionResult> UpdateAllIsSeenStatus(Guid receiverId)
        {
             await _notificationService.UpdateAllIsSeenStatus(receiverId);
             return CustomResult("Cập nhật trạng thái tất cả thông báo thành công.", null);

        }

    }

}
