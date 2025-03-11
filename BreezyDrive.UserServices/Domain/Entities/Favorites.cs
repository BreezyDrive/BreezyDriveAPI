using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BreezyDrive.Common.Domain.Entities;

namespace BreezyDrive.UserServices.Domain.Entities
{
    [Table("Favorites")]
    public class Favorites : BaseEntities
    {
        public Guid UserId { get; set; }

        public Guid CarId { get; set; }

        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
    }
}
