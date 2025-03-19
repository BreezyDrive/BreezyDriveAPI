using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreezyDrive.AuthenticationServices.Application.DTOs.Request;

namespace BreezyDrive.AuthenticationServices.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> Register(RegisterRequest registerRequest);
        Task<string> Login(LoginRequest loginRequest);
        Task<string> LoginGoogle(GoogleLoginRequest googleLoginRequest);
    }
}
