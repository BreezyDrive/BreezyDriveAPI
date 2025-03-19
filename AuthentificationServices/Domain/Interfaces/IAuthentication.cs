using BreezyDrive.UserServices.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BreezyDrive.CommonService.Domain.Interfaces
{
    public interface IAuthentication
    {
        string GenerateJWTToken(Users users);
        long GetUserIdFromHttpContext(HttpContext httpContext);
    }
}
