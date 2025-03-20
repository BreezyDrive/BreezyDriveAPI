using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BreezyDrive.CommonService.Domain.Entities;

namespace BreezyDrive.UserServices.Domain.Entities
{
    [Table("Users")]
    public class Users : BaseEntities
    {
        public Guid RoleId { get; set; }

        public string Avatar { get; set; }

        public string? FullName { get; set; }

        public string? Password { get; set; }

        public string? DrivingLicense { get; set; }

        public string? Phone { get; set; }

        public bool IsPhoneVerification { get; set; }

        public DateOnly? Dob { get; set; }

        public string? Sex { get; set; }

        public string? Email { get; set; }

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
