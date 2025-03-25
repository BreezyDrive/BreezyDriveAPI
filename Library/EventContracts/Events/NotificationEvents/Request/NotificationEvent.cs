using Library.EventContracts.Events.NotificationEvents.Enums;

namespace Library.EventContracts.Events.NotificationEvents.Request
{
    public class NotificationEvent
    {
        public Guid ReceiverId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public NotificationType NotificationType { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public bool IsSeen { get; set; } = false;
    }
}
