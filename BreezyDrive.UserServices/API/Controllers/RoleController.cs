using BreezyDrive.UserServices.Application.DTOs.Request;
using BreezyDrive.UserServices.Application.Interfaces;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.UserServices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService) 
        {
            _roleService = roleService;
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRoles();
            return CustomResult("Lấy dữ liệu thành công", roles);
        }
        
        [HttpGet("GetRoleByGuid")]
        public async Task<IActionResult> GetRoleByGuid(Guid id)
        {
            var role = await _roleService.GetRoleByGuid(id);
            return CustomResult("Lấy dữ liệu thành công", role);
        }

        [HttpGet("GetRoleByName")]
        public async Task<IActionResult> GetRoleByName(string name)
        {
            var role = await _roleService.GetRoleByName(name);
            return CustomResult("Lấy dữ liệu thành công", role);
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(RoleRequest roleRequest)
        {
            var role = await _roleService.CreateRole(roleRequest);
            return CustomResult("Tạo role thành công.", role);
        }

        [HttpPatch("UpdateRole")]
        public async Task<IActionResult> UpdateRole(Guid id, RoleRequest roleRequest)
        {
            var role = await _roleService.UpdateRole(id, roleRequest);
            return CustomResult("Cập nhật role thành công.", role);
        }

        [HttpDelete("DeleteRoleByGuid")]
        public async Task<IActionResult> DeleteRoleByGuid(Guid id)
        {
            await _roleService.DeleteRoleByGuid(id);
            return CustomResult("Xóa role thành công.");
        }

        [HttpDelete("DeleteRoleByName")]
        public async Task<IActionResult> DeleteRoleByName(string name)
        {
            await _roleService.DeleteRoleByName(name);
            return CustomResult("Xóa role thành công.");
        }
    }
}
