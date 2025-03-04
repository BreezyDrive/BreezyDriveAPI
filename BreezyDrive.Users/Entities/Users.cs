using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BreezyDrive.Users.Entities
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public int RoleId { get; set; }

        public string UserName { get; set; }

        public string DrivingLicense { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int Point { get; set; }

        public int TotalReservation { get; set; }

        public DateTimeOffset CreateAt { get; set; }

        [ForeignKey("RoleId")]
        public virtual Roles Role { get; set; }

        public Users()
        {
            CreateAt = DateTimeOffset.Now;
        }
    }
}
