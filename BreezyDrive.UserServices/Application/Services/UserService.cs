using AutoMapper;
using BreezyDrive.Common.Domain.Interfaces;
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

        public async Task<List<UserResponse>> GetUsers()
        {
            var users = _unitOfWork.Repository<Users>().GetAll();
            
            var userResponses = _mapper.Map<List<UserResponse>>(users);
            return userResponses;
        }

        public async Task<UserResponse> CreateUser()
        {
            //var role = _unitOfWork.Repository<Roles>().Get(r => r.Name.Equals("Admin")).FirstOrDefault();

            var role = new Roles
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Name = "User",
            };
            _unitOfWork.Repository<Roles>().Insert(role);
            var newuser = new Users
            {
                RoleId = role.Id,
                UserName = "a",
                DrivingLicense = "a",
                Phone = "a",
                Email = "a",
                Point = 1,
                TotalReservation = 1,
                CreateAt = DateTime.Now,
            };

            _unitOfWork.Repository<Users>().Insert(newuser);
            await _unitOfWork.SaveAsync();

            var userResponse = _mapper.Map<UserResponse>(newuser);
            return userResponse;
        }
    }
}

