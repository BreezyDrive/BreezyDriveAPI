using BreezyDrive.UserServices.Application.DTOs.Request;
using BreezyDrive.UserServices.Application.DTOs.Response;

namespace BreezyDrive.UserServices.Application.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleResponse>> GetAllRoles();
        Task<RoleResponse> GetRoleByGuid(Guid id);
        Task<RoleResponse> GetRoleByName(string name);
        Task<bool> CreateRole(RoleRequest roleRequest);
        Task<bool> UpdateRole(RoleRequest roleRequest);
        Task<bool> DeleteRoleByGuid(Guid id);
        Task<bool> DeleteRoleByName(string name);
    }
}
