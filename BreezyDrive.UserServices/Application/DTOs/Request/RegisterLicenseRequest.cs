using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.UserServices.Domain.Entities;

namespace BreezyDrive.UserServices.Application.DTOs.Request
{
    public class RegisterLicenseRequest : IMapFrom<UserDriveLicenses>
    {
        public Guid UserId { get; set; }

        public IFormFile Front { get; set; }

        public string Number { get; set; }

        public string FullName { get; set; }

        public string Dob { get; set; }
    }
}
