using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BreezyDrive.Common.Domain.Entities;

namespace BreezyDrive.Users.Domain.Entities
{
    [Table("UserDriveLisences")]
    public class UserDriveLisences : BaseEntities
    {
        public Guid UserId { get; set; }

        public int Number { get; set; }

        public string Front { get; set; }

        public string Back { get; set; }

        [ForeignKey("UserId")]
        public virtual User Users { get; set; }
    }
}
