using BreezyDrive.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.ConversationServices.Domain.Entities
{
    [Table("MessageFile")]
    public class MessageFile : BaseEntities
    {
        public Guid MessageId { get; set; }

        public Guid FiledId { get; set; }
    }
}
