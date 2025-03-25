using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.NotificationServices.Domain.Entities;
using Library.EventContracts.Events.NotificationEvents.Enums;

namespace BreezyDrive.NotificationServices.Application.DTOs.Request
{
    public class NotificationRequest : IMapFrom<Notification>
    {
        public Guid ReceiverId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public bool IsSeen { get; set; } = false;

        public NotificationType NotificationType { get; set; }
    }
}
