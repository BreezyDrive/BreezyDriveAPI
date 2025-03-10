using BreezyDrive.UserServices.Application.DTOs.Response;

namespace BreezyDrive.UserServices.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetUsers();
        Task<UserResponse> CreateUser();
    }
}
