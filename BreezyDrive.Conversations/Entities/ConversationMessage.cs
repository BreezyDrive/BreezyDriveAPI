using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.Conversations.Entities
{
    [Table("ConversationMessage")]
    public class ConversationMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid ConverationId { get; set; }
        public Guid SenderId { get; set; }
        public DateTime CreateTime { get; set; }
        public string Content { get; set; }
        public bool IsSeen { get; set; }
        public Guid Reply_To_MessageId { get; set; }

    }
}
