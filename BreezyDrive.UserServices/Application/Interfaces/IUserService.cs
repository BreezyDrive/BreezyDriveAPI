using BreezyDrive.UserServices.Application.DTOs.Request;
using BreezyDrive.UserServices.Application.DTOs.Response;

namespace BreezyDrive.UserServices.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllUsers();
        Task<bool> Register(CreateUserRequest createUserRequest);
    }
}
