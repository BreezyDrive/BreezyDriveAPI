using BreezyDrive.UserServices.Application.DTOs.Request;
using BreezyDrive.UserServices.Application.Interfaces;
using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.UserServices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return CustomResult("Danh sách Users:", users);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var users = await _userService.GetUserById(id);
            return CustomResult("Danh sách Users:", users);
        }
    }
}
