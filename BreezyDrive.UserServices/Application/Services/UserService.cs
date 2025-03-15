using AutoMapper;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.UserServices.Application.DTOs.Request;
using BreezyDrive.UserServices.Application.DTOs.Response;
using BreezyDrive.UserServices.Application.Interfaces;
using BreezyDrive.UserServices.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Drawing;
using BreezyDrive.UserServices.Domain.Interfaces;

namespace BreezyDrive.UserServices.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHashing _hashing;
        private readonly IAuthentication _authentication;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IHashing hashing, IAuthentication authentication)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hashing = hashing;
            _authentication = authentication;
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
    }
}

