using BreezyDrive.CommonService.Infrastuctures.Messaging;
using BreezyDrive.NotificationServices.Application.DTOs.Request;
using BreezyDrive.NotificationServices.Application.DTOs.Response;
using BreezyDrive.NotificationServices.Application.Hubs;
using BreezyDrive.NotificationServices.Application.Interfaces;
using Library.EventContracts.Events.NotificationEvents.Request;
using Microsoft.AspNetCore.SignalR;

namespace BreezyDrive.NotificationServices.Application.Messaging
{
    public class NotificationHandler : IMessageHandler<NotificationEvent, NotificationResponseEvent>
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly INotificationService _notificationService; 

        public NotificationHandler(IHubContext<NotificationHub> hubContext, INotificationService notificationService)
        {
            _hubContext = hubContext;
            _notificationService = notificationService;
        }

        public async Task<NotificationResponseEvent> HandleMessageAsync(NotificationEvent message)
        {
            Console.WriteLine($"📩 Nhận notification: {message.Description}, {message.ReceiverId}");
            await _hubContext.Clients.User(message.ReceiverId.ToString())
                .SendAsync("ReceiveNotification", message.Name, message.Description, message.NotificationType.ToString(), message.CreateDate, message.IsSeen);

            var notificationRequest = new NotificationRequest
            {
                ReceiverId = message.ReceiverId,
                Name = message.Name,
                Description = message.Description,
                CreateDate = message.CreateDate,
                IsSeen = message.IsSeen,
                NotificationType = message.NotificationType
            };

            await _notificationService.CreateNotificationAsync(notificationRequest);

            return new NotificationResponseEvent
            {
                Name = "Notification",
                Description = message.Description,
                IsSeen = false,
                NotificationType = message.NotificationType
            };
        }
    }
}
