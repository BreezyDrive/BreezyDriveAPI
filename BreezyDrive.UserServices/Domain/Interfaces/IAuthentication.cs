using BreezyDrive.UserServices.Domain.Entities;

namespace BreezyDrive.UserServices.Domain.Interfaces
{
    public interface IAuthentication
    {
        string GenerateJWTToken(Users users);
        long GetUserIdFromHttpContext(HttpContext httpContext);
    }
}
