using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BreezyDrive.Users.Entities
{
    [Table("Favorites")]
    public class Favorites
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public Guid CarId { get; set; }

        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
    }
}
