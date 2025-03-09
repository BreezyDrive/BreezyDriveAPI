using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BreezyDrive.Common.Domain.Entities;

namespace BreezyDrive.Users.Domain.Entities
{
    [Table("UserIdentifications")]
    public class UserIdentifications : BaseEntities
    {
        public Guid UserId { get; set; }

        public int Number { get; set; }

        public string FullName { get; set; }

        public DateOnly Dob { get; set; }

        public string Sex { get; set; }

        public string Front { get; set; }

        public string Back { get; set; }

        [ForeignKey("UserId")]
        public virtual User Users { get; set; }
    }
}
