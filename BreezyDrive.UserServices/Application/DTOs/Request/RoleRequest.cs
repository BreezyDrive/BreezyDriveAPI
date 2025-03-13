using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.UserServices.Domain.Entities;

namespace BreezyDrive.UserServices.Application.DTOs.Request
{
    public class RoleRequest : IMapFrom<Roles>
    {
        public string Name { get; set; }
    }
}
