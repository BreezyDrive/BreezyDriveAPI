using BreezyDrive.UserServices.Application.DTOs.Request;
using BreezyDrive.UserServices.Application.DTOs.Response;

namespace BreezyDrive.UserServices.Application.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleResponse>> GetAllRoles();
        Task<RoleResponse> GetRoleByGuid(Guid id);
        Task<bool> CreateRole(RoleRequest roleRequest);
    }
}
