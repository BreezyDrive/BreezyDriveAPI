using BreezyDrive.CommonService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.ConversationServices.Domain.Entities
{
    [Table("MessageFile")]
    public class MessageFile : BaseEntities
    {
        [BsonRepresentation(BsonType.String)]
        public Guid MessageId { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid FiledId { get; set; }
    }
}
