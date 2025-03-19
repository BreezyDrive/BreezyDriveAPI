using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.UserServices.Domain.Entities;

namespace BreezyDrive.UserServices.Application.DTOs.Request
{
    public class CreateUserRequest : IMapFrom<Users>
    {
        public string? Phone { get; set; }

        public string FullName { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }

        public string? Email { get; set; }
    }
}
