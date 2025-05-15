using BreezyDrive.CommonService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.ConversationServices.Domain.Entities
{
    [Table("ConversationMessage")]
    public class ConversationMessage : BaseEntities
    {
        [BsonRepresentation(BsonType.String)]
        public Guid ConversationId { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid SenderId { get; set; }

        public DateTimeOffset CreateTime { get; set; }

        public string Content { get; set; }

        public bool IsSeen { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid? ReplyToMessageId { get; set; }

        public List<MessageFile> Files { get; set; } = new List<MessageFile>();

        public ConversationMessage()
        {
            CreateTime = DateTimeOffset.Now;
        }
    }
}
