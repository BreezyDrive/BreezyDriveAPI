using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.Conversations.Entities
{
    [Table("Conversation")]
    public class Conversation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid UserId1 { get; set; }
        public Guid UserId2 { get; set; }
        public string LastMessage { get; set; }
        public bool IsOpen { get; set; }
        public Guid? CloseAccountId { get; set; }

        [ForeignKey("UserId")]
        public virtual Users UserId1 { get; set; }
        [ForeignKey("UserId")]
        public virtual Users UserId2 { get; set; }

    }
}
