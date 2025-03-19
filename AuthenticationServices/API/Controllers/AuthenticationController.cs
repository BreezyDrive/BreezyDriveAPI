using BreezyDrive.AuthenticationServices.Application.DTOs.Request;
using BreezyDrive.AuthenticationServices.Application.Interfaces;
using CoreApiResponse;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.AuthenticationServices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest registerRequest)
        {
            return CustomResult("Đăng ký thành công.", await _authenticationService.Register(registerRequest));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var token = await _authenticationService.Login(loginRequest);
            return CustomResult("Đăng nhập thành công", token);
        }

        [HttpPost("LoginGoogle")]
        public async Task<IActionResult> LoginGoogle([FromBody] GoogleLoginRequest googleLoginRequest)
        {
            var token = await _authenticationService.LoginGoogle(googleLoginRequest);
            return CustomResult("Đăng nhập thành công", token);
        }
    }
}
