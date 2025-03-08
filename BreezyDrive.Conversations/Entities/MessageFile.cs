using BreezyDrive.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.Conversations.Entities
{
    [Table("MessageFile")]
    public class MessageFile : IEntities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid MessageId { get; set; }
        public Guid FiledId { get; set; }
    }
}
