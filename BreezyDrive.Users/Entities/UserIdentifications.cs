using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BreezyDrive.Users.Entities
{
    [Table("UserIdentifications")]
    public class UserIdentifications
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public int Number { get; set; }

        public string FullName { get; set; }

        public DateOnly Dob { get; set; }

        public string Sex { get; set; }

        public string Front { get; set; }

        public string Back { get; set; }

        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
    }
}
