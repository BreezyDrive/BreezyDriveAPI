using BreezyDrive.UserServices.Application.DTOs.Request;
using BreezyDrive.UserServices.Application.DTOs.Response;

namespace BreezyDrive.UserServices.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllUsers();
        Task<UserResponse> GetUserById(Guid id);
        Task<bool> Register(CreateUserRequest createUserRequest);
        Task<string> Login(LoginRequest loginRequest);
        Task<string> LoginGoogle(GoogleLoginRequest googleLoginRequest);
    }
}
