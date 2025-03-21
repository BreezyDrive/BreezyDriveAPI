using Library.EventContracts.Events.UserEvents.Response;

namespace BreezyDrive.AuthenticationServices.Domain.Interfaces
{
    public interface IAuthentication
    {
        string GenerateJwtToken(GetUserResponseEvent users);
        long GetUserIdFromHttpContext(HttpContext httpContext);
    }
}
