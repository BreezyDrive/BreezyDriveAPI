using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BreezyDrive.Common.Domain.Entities;

namespace BreezyDrive.Users.Entities
{
    [Table("Favorites")]
    public class Favorites : IEntities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid CarId { get; set; }

        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
    }
}
