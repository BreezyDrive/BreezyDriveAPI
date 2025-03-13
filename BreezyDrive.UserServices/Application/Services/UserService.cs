using AutoMapper;
using BreezyDrive.Common.Domain.Interfaces;
using BreezyDrive.Domain.Exceptions;
using BreezyDrive.UserServices.Application.DTOs.Request;
using BreezyDrive.UserServices.Application.DTOs.Response;
using BreezyDrive.UserServices.Application.Interfaces;
using BreezyDrive.UserServices.Domain.Entities;

namespace BreezyDrive.UserServices.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        public async Task<bool> Register(CreateUserRequest createUserRequest)
        {
            if (createUserRequest.Password.ToUpper() != createUserRequest.ConfirmPassword.ToUpper())
            {
                throw new CustomExceptions.InvalidDataException("Mật khẩu không trùng khớp.");
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

            var newuser = new Users
            {
                RoleId = existingRole.Id,
                FullName = createUserRequest.FullName,
                Password = createUserRequest.Password,
                Phone = createUserRequest.Phone,
                IsPhoneVerification = false,
                Point = 0,
                TotalReservation = 0,
                CreateAt = DateTime.Now,
            };

            _unitOfWork.Repository<Users>().Insert(newuser);
            await _unitOfWork.SaveAsync();

            return true;
        }
    }
}

