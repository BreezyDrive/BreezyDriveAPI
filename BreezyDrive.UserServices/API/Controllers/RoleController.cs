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
            var roles = _roleService.GetAllRoles();
            return CustomResult("Lấy dữ liệu thành công", roles);
        }
        
        [HttpGet("GetRoleByGuid")]
        public async Task<IActionResult> GetRoleByGuid(Guid id)
        {
            var role = _roleService.GetRoleByGuid(id);
            return CustomResult("Lấy dữ liệu thành công", role);
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(RoleRequest roleRequest)
        {
            var role = await _roleService.CreateRole(roleRequest);
            return CustomResult("Tạo role thành công.", role);
        }
    }
}
