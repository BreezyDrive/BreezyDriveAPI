using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.Conversations.Entities
{
    [Table("MessageFile")]
    public class MessageFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid MessageId { get; set; }
        public Guid FiledId { get; set; }
    }
}
