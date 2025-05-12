using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations.Schema;
using Library.EventContracts.Events.NotificationEvents.Enums;
using BreezyDrive.CommonService.Domain.Entities;

namespace BreezyDrive.NotificationServices.Domain.Entities
{
    [Table("Notifications")]
    public class Notification : BaseEntities
    {
        [BsonRepresentation(BsonType.String)]
        public Guid ReceiverId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public bool IsSeen { get; set; } = false;

        public NotificationType NotificationType { get; set; }

        public Notification()
        {
            CreateDate = DateTimeOffset.Now;
        }
    }
}
