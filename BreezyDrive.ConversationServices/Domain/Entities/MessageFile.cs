using BreezyDrive.CommonService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.ConversationServices.Domain.Entities
{
    [Table("MessageFile")]
    public class MessageFile : BaseEntities
    {
        [BsonRepresentation(BsonType.String)]
        public Guid MessageId { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string FiledId { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public long FileSize { get; set; }

        public string FileUrl { get; set; }

        //public DateTimeOffset UploadTime { get; set; }

        //public MessageFile()
        //{
        //    UploadTime = DateTimeOffset.Now;
        //}
    }
}
