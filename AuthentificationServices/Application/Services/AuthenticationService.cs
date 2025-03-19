using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthentificationServices.Application.Interfaces;
using BreezyDrive.AuthenticationServices.Application.DTOs.Request;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.UserServices.Domain.Entities;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;

namespace AuthentificationServices.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashing _hashing;
        private readonly IAuthentication _authentication;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUnitOfWork unitOfWork, IHashing hashing, IAuthentication authentication, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _hashing = hashing;
            _authentication = authentication;
            _configuration = configuration;
        }


        public async Task<string> Login(LoginRequest loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Phone) || string.IsNullOrEmpty(loginRequest.Password))
            {
                throw new CustomExceptions.InvalidDataException("Số điện thoại hoặc mật khẩu không được để trống.");
            }

            string hashedPass = _hashing.SHA512Hash(loginRequest.Password);
            IEnumerable<Users> check = _unitOfWork.Repository<Users>().Get(x =>
                x.Phone.Equals(loginRequest.Phone)
                && x.Password.Equals(hashedPass));

            if (check == null || !check.Any())
            {
                throw new CustomExceptions.InvalidDataException("Tài khoản hoặc mật khẩu không đúng.");
            }

            Users users = check.First();
            string token = _authentication.GenerateJWTToken(users);
            return token;
        }

        public async Task<string> LoginGoogle(GoogleLoginRequest googleLoginRequest)
        {
            var payload = await VerifyGoogleToken(googleLoginRequest.IdToken);
            if (payload == null)
            {
                throw new CustomExceptions.InvalidDataException("Token Google không hợp lệ.");
            }

            var user = await _unitOfWork.Repository<Users>().GetFirstOrDefaultAsync(x => x.Email == payload.Email);
            string token = "";
            if (user == null)
            {
                var password = Guid.NewGuid().ToString();
                var registerRequest = new RegisterGoogleRequest
                {
                    FullName = payload.Name,
                    Email = payload.Email,
                    Password = password,
                    Avatar = payload.Picture,
                };

                if (await RegisterGoogle(registerRequest))
                {
                    var newUser = _unitOfWork.Repository<Users>().Get(nu => nu.Email == payload.Email).FirstOrDefault();
                    token = _authentication.GenerateJWTToken(newUser);
                    /*
                                        string refreshToken = _authentication.GenerateRefreshToken();
                                        await _authentication.SaveRefreshToken(newUser, refreshToken);
                                        return new LoginResponse { token = token, refreshToken = refreshToken };*/
                }
                else
                {
                    throw new CustomExceptions.InvalidDataException("Đăng nhập Google thất bại");
                }
            }

            return token;
        }

        private async Task<GoogleJsonWebSignature.Payload?> VerifyGoogleToken(string idToken)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new List<string> { _configuration["Google:ClientId"] }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
                return payload;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> RegisterGoogle(RegisterGoogleRequest registerRequest)
        {
            /*var newuser = _mapper.Map<Users>(registerRequest);
            newuser.RoleId = _unitOfWork.Repository<Roles>().Get(r => r.Name == "User").Select(r => r.Id).FirstOrDefault();
            newuser.Password = _hashing.SHA512Hash(registerRequest.Password);
            _unitOfWork.Repository<Users>().Insert(newuser);
            await _unitOfWork.SaveAsync();*/

            return true;
        }
    }
}
