using BreezyDrive.AuthenticationServices.Application.DTOs.Request;
using Library.EventContracts.Events.UserEvents.Response;
using MassTransit;
using Microsoft.AspNetCore.Http;

namespace BreezyDrive.CommonService.Domain.Interfaces
{
    public interface IAuthentication
    {
        string GenerateJWTToken(GetUserResponseEvent users);
        long GetUserIdFromHttpContext(HttpContext httpContext);
    }
}
