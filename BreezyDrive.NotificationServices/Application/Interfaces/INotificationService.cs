using BreezyDrive.NotificationServices.Application.DTOs.Request;
using BreezyDrive.NotificationServices.Application.DTOs.Response;

namespace BreezyDrive.NotificationServices.Application.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationResponse>> GetNotificationByUser (Guid ReceiverId);
        Task<NotificationResponse> CreateNotificationAsync(NotificationRequest request);
        Task UpdateIsSeenStatus(Guid id);
        Task UpdateAllIsSeenStatus(Guid receiverId);
    }
}
