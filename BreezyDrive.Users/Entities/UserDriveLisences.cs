using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BreezyDrive.Users.Entities
{
    [Table("UserDriveLisences")]
    public class UserDriveLisences
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public int Number { get; set; }

        public string Front { get; set; }

        public string Back { get; set; }

        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
    }
}
