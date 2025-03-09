using BreezyDrive.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.ConversationServices.Domain.Entities
{
    [Table("ConversationMessage")]
    public class ConversationMessage : BaseEntities
    {
        public Guid ConverationId { get; set; }

        public Guid SenderId { get; set; }

        public DateTime CreateTime { get; set; }

        public string Content { get; set; }

        public bool IsSeen { get; set; }

        public Guid ReplyToMessageId { get; set; }

    }
}
