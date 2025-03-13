using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BreezyDrive.CommonService.Domain.Entities;

namespace BreezyDrive.UserServices.Domain.Entities
{
    [Table("UserDriveLisences")]
    public class UserDriveLisences : BaseEntities
    {
        public Guid UserId { get; set; }

        public int Number { get; set; }

        public string Front { get; set; }

        public string Back { get; set; }

        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
    }
}
