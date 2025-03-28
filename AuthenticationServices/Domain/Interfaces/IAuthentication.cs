using System.IdentityModel.Tokens.Jwt;
using Library.EventContracts.Events.UserEvents.Response;

namespace BreezyDrive.AuthenticationServices.Domain.Interfaces
{
    public interface IAuthentication
    {
        string GenerateJwtToken(GetUserResponseEvent users);
        Guid GetUserIdFromToken(string jwtToken);
    }
}
