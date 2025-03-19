using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreezyDrive.AuthenticationServices.Application.DTOs.Request;

namespace AuthentificationServices.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> Login(LoginRequest loginRequest);
        Task<string> LoginGoogle(GoogleLoginRequest googleLoginRequest);
    }
}
