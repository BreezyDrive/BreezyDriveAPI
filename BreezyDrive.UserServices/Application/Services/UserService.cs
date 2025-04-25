using AutoMapper;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.UserServices.Application.DTOs.Request;
using BreezyDrive.UserServices.Application.DTOs.Response;
using BreezyDrive.UserServices.Application.Interfaces;
using BreezyDrive.UserServices.Domain.Entities;
using MassTransit;
using Library.EventContracts.Events.NotificationEvents.Enums;
using Library.EventContracts.Events.NotificationEvents.Request;

namespace BreezyDrive.UserServices.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
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
            var notificationEvent = new NotificationEvent
            {
                ReceiverId = Guid.Parse(user.Id.ToString()),
                Description = "get successful",
                Name = "successful",
                NotificationType = NotificationType.Message,
                CreateDate = DateTimeOffset.UtcNow,
                IsSeen = false
            };

            Console.WriteLine($"Đang publish NotificationEvent: {notificationEvent.Description}, Name: {notificationEvent.Name}, NotiType : {notificationEvent.NotificationType}, ReceiverId: {notificationEvent.ReceiverId}");

            await _publishEndpoint.Publish(notificationEvent);
            var userResponse = _mapper.Map<UserResponse>(user);
            return userResponse;
        }

        public async Task<UserResponse> CreateUser(CreateUserRequest createUserRequest)
        {
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
            newuser.IsPhoneVerification = false;
            newuser.Point = 0;
            newuser.TotalReservation = 0;
            newuser.CreateAt = DateTime.Now;

            _unitOfWork.Repository<Users>().Insert(newuser);
            await _unitOfWork.SaveAsync();

            var userResponse = new UserResponse();
            return userResponse;
        }

        public async Task<bool> isUserExists(Guid userId)
        {
            return _unitOfWork.Repository<Users>().Exists(u => u.Id == userId);
        }

        public async Task<UserResponse> CheckPhonePassword(string phone, string password)
        {
            IEnumerable<Users> check = _unitOfWork.Repository<Users>().Get(x =>
                x.Phone.Equals(phone)
                && x.Password.Equals(password));

            if (check == null || !check.Any())
            {
                throw new CustomExceptions.InvalidDataException("Tài khoản hoặc mật khẩu không đúng.");
            }

            var user = check.FirstOrDefault();
            var userResponse = _mapper.Map<UserResponse>(user);
            return userResponse;
        }

        public async Task<UserResponse> CheckGoogleEmail(string email)
        {
            IEnumerable<Users> check = _unitOfWork.Repository<Users>().Get(x =>
                x.Email.Equals(email));
            if (check == null || !check.Any())
            {
                throw new CustomExceptions.InvalidDataException("Email không tồn tại.");
            }
            var user = check.FirstOrDefault();
            var userResponse = _mapper.Map<UserResponse>(user);
            return userResponse;
        }
    }
}

