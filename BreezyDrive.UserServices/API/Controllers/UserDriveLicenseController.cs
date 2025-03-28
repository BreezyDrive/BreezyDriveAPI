using BreezyDrive.UserServices.Application.DTOs.Request;
using BreezyDrive.UserServices.Application.Interfaces;
using BreezyDrive.UserServices.Application.Services;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.UserServices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDriveLicenseController : BaseController
    {
        private readonly IUserDriveLicenseService _userDriveLicenseService;

        public UserDriveLicenseController(IUserDriveLicenseService userDriveLicenseService)
        {
            _userDriveLicenseService = userDriveLicenseService;
        }

        [HttpGet("GetAllUserDriveLisence")]
        public async Task<IActionResult> GetAllUserDriveLisence()
        {
            return CustomResult("Danh sách bằng lái xe:", await _userDriveLicenseService.GetAllUserDriveLisence());
        }

        [HttpGet("GetUserDriveLisenceById/{id}")]
        public async Task<IActionResult> GetUserDriveLisenceById(Guid id)
        {
            return CustomResult("Bằng lái xe của người dùng:", await _userDriveLicenseService.GetUserDriveLisenceById(id));
        }

        [HttpPost("RegisterLicense")]
        public async Task<IActionResult> RegisterLicense([FromForm] RegisterLicenseRequest registerLicenseRequest)
        {
            return CustomResult("Đăng ký bằng lái xe thành công.", await _userDriveLicenseService.RegisterLicense(registerLicenseRequest));
        }
    }
}
