using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.UserServices.Domain.Entities;

namespace BreezyDrive.UserServices.Application.DTOs.Response
{
    public class UserResponse : IMapFrom<Users>
    {
        public Guid Id { get; set; }

        public Guid RoleId { get; set; }

        public string Avatar { get; set; }

        public string FullName { get; set; }

        public string DrivingLicense { get; set; }

        public string Phone { get; set; }

        public bool IsPhoneVerification { get; set; }

        public string Email { get; set; }

        public int Point { get; set; }

        public int TotalReservation { get; set; }

        public DateTimeOffset CreateAt { get; set; }
    }
}
