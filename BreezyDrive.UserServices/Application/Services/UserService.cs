using AutoMapper;
using Google.Apis.Auth;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.UserServices.Application.DTOs.Request;
using BreezyDrive.UserServices.Application.DTOs.Response;
using BreezyDrive.UserServices.Application.Interfaces;
using BreezyDrive.UserServices.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Drawing;
using BreezyDrive.UserServices.Domain.Interfaces;
using Azure.Core;
using Microsoft.AspNetCore.Identity;

namespace BreezyDrive.UserServices.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHashing _hashing;
        private readonly IAuthentication _authentication;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IHashing hashing, IAuthentication authentication, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hashing = hashing;
            _authentication = authentication;
            _configuration = configuration;
        }

        public async Task<List<UserResponse>> GetAllUsers()
        {
            var users = _unitOfWork.Repository<Users>().GetAll();
            if (!users.Any())
            {
                throw new CustomExceptions.DataNotFoundException("Không tìm thấy người dùng nào.");
            }

            var userResponses = _mapper.Map<List<UserResponse>>(users);
            return userResponses;
        }
        
        public async Task<UserResponse> GetUserById(Guid id)
        {
            var user = _unitOfWork.Repository<Users>().GetById(id);
            if (user == null)
            {
                throw new CustomExceptions.DataNotFoundException("Không tìm thấy người dùng nào.");
            }

            var userResponse = _mapper.Map<UserResponse>(user);
            return userResponse;
        }

        public async Task<bool> Register(CreateUserRequest createUserRequest)
        {
            if (createUserRequest.ConfirmPassword.ToUpper() != createUserRequest.Password.ToUpper())
            {
                throw new CustomExceptions.InvalidDataException("Mật khẩu không trùng khớp.");
            }

            var duplicatePhone = _unitOfWork.Repository<Users>().Get(ep => ep.Phone == createUserRequest.Phone
                                                                    && ep.IsPhoneVerification == true).FirstOrDefault();
            if (duplicatePhone != null)
            {
                throw new CustomExceptions.DataExistException("Số điện thoại này đã tồn tại.");
            }

            var existingRole = _unitOfWork.Repository<Roles>().Get(r => r.Name.Equals("User")).FirstOrDefault();
            if (existingRole == null)
            {
                var newRole = new Roles
                {
                    Name = "User"
                };

                _unitOfWork.Repository<Roles>().Insert(newRole);
                await _unitOfWork.SaveAsync();

                existingRole = newRole;
            }

            var newuser = _mapper.Map<Users>(createUserRequest);
            newuser.RoleId = existingRole.Id;
            newuser.Avatar = "https://icons.veryicon.com/png/o/miscellaneous/standard/avatar-15.png";
            newuser.Password = _hashing.SHA512Hash(createUserRequest.Password);
            newuser.IsPhoneVerification = false;
            newuser.Point = 0;
            newuser.TotalReservation = 0;
            newuser.CreateAt = DateTime.Now;
            /*var newuser = new Users
            {
                RoleId = existingRole.Id,
                FullName = createUserRequest.FullName,
                Password = createUserRequest.Password,
                Phone = createUserRequest.Phone,
                IsPhoneVerification = false,
                Point = 0,
                TotalReservation = 0,
                CreateAt = DateTime.Now,
            };*/

            _unitOfWork.Repository<Users>().Insert(newuser);
            await _unitOfWork.SaveAsync();

            return true;
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
            var newuser = _mapper.Map<Users>(registerRequest);
            newuser.RoleId = _unitOfWork.Repository<Roles>().Get(r => r.Name == "User").Select(r => r.Id).FirstOrDefault();
            newuser.Password = _hashing.SHA512Hash(registerRequest.Password);
            _unitOfWork.Repository<Users>().Insert(newuser);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> isUserExists(Guid userId)
        {
            return _unitOfWork.Repository<Users>().Exists(u => u.Id == userId);
        }
    }
}

