using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.UserServices.Domain.Entities;

namespace BreezyDrive.UserServices.Application.DTOs.Response
{
    public class UserResponse : IMapFrom<Users>
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }
    }
}
