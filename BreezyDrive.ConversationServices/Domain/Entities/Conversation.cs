using BreezyDrive.CommonService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.ConversationServices.Domain.Entities
{
    [Table("Conversation")]
    public class Conversation : BaseEntities
    {
        [BsonRepresentation(BsonType.String)]
        public Guid UserId1 { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid UserId2 { get; set; }

        public string LastMessage { get; set; }

        public bool IsOpen { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid? CloseAccountId { get; set; }

        public List<ConversationMessage> Messages { get; set; } = new List<ConversationMessage>();
    }
}
