using BreezyDrive.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.Conversations.Domain.Entities
{
    [Table("Conversation")]
    public class Conversation : BaseEntities
    {
        public Guid UserId1 { get; set; }

        public Guid UserId2 { get; set; }

        public string LastMessage { get; set; }

        public bool IsOpen { get; set; }

        public Guid? CloseAccountId { get; set; }
    }
}
