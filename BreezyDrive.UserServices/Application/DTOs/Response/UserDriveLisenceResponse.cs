using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.UserServices.Domain.Entities;

namespace BreezyDrive.UserServices.Application.DTOs.Response
{
    public class UserDriveLisenceResponse : IMapFrom<UserDriveLicenses>
    {
        public Guid UserId { get; set; }

        public int Number { get; set; }

        public string Front { get; set; }
    }
}
