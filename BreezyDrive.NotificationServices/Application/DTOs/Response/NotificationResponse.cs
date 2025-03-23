using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.NotificationServices.Domain.Entities;
using Library.EventContracts.Events.NotificationEvents.Enums;

namespace BreezyDrive.NotificationServices.Application.DTOs.Response
{
    public class NotificationResponse : IMapFrom<Notification>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public bool IsSeen { get; set; } = false;

        public NotificationType NotificationType { get; set; }
    }
}
