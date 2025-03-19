using BreezyDrive.AuthenticationServices.Application.DTOs.Request;
using BreezyDrive.AuthenticationServices.Application.Interfaces;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Domain.Interfaces;
using Google.Apis.Auth;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;
using MassTransit;
using Microsoft.IdentityModel.Tokens;

namespace BreezyDrive.AuthenticationServices.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHashing _hashing;
        private readonly IAuthentication _authentication;
        private readonly IConfiguration _configuration;
        private readonly IRequestClient<RegisterRequestEvent> _client;

        public AuthenticationService(IHashing hashing, IAuthentication authentication,
                                    IConfiguration configuration, IRequestClient<RegisterRequestEvent> client)
        {
            _hashing = hashing;
            _authentication = authentication;
            _configuration = configuration;
            _client = client;
        }

        public async Task<bool> Register(RegisterRequest registerRequest)
        {
            if (registerRequest.Email == null)
            {
                if (registerRequest.ConfirmPassword != registerRequest.Password)
                {
                    throw new CustomExceptions.InvalidDataException("Mật khẩu không trùng khớp.");
                }
            }

            var response = await _client.GetResponse<Task<bool>>(
                new RegisterRequestEvent
                {
                    Email = registerRequest.Email,
                    FullName = registerRequest.FullName,
                    Password = _hashing.SHA512Hash(registerRequest.Password),
                    Phone = registerRequest.Phone,
                });

            return true;
        }

        public async Task<string> Login(LoginRequest loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Phone) || string.IsNullOrEmpty(loginRequest.Password))
            {
                throw new CustomExceptions.InvalidDataException("Số điện thoại hoặc mật khẩu không được để trống.");
            }

            string hashedPass = _hashing.SHA512Hash(loginRequest.Password);

            var response = await _client.GetResponse<GetUserResponseEvent>(
                new GetUserRequestEvent
                {
                    Phone = loginRequest.Phone,
                    Password = hashedPass
                });

            string token = _authentication.GenerateJWTToken(response.Message);
            return token;
        }

        public async Task<string> LoginGoogle(GoogleLoginRequest googleLoginRequest)
        {
            var payload = await VerifyGoogleToken(googleLoginRequest.IdToken);
            if (payload == null)
            {
                throw new CustomExceptions.InvalidDataException("Token Google không hợp lệ.");
            }

            var response = await _client.GetResponse<CheckGoogleExistResponseEvent>(
                new CheckGoogleExistRequestEvent
                {
                    Email = payload.Email,
                });

            string token = "";

            if (response == null)
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
                    var newUserResponse = await _client.GetResponse<GetUserResponseEvent>(
                        new GetUserRequestEvent
                        {
                            Email = payload.Email
                        });
                    token = _authentication.GenerateJWTToken(newUserResponse.Message);
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
            var response = await _client.GetResponse<Task<bool>>(
                new RegisterGoogleRequestEvent
                {
                    Email = registerRequest.Email,
                    FullName = registerRequest.FullName,
                    Password = _hashing.SHA512Hash(registerRequest.Password),
                    Avatar = registerRequest.Avatar,
                });
            return true;
        }
    }
}
