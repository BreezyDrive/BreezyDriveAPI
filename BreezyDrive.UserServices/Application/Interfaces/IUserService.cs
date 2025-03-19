using BreezyDrive.UserServices.Application.DTOs.Request;
using BreezyDrive.UserServices.Application.DTOs.Response;

namespace BreezyDrive.UserServices.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllUsers();
        Task<UserResponse> GetUserById(Guid id);
        Task<UserResponse> CreateUser(CreateUserRequest createUserRequest);
        Task<bool> isUserExists(Guid userId);
        Task<UserResponse> CheckPhonePassword(string phone, string password);
        Task<UserResponse> CheckGoogleEmail(string email);
    }
}
