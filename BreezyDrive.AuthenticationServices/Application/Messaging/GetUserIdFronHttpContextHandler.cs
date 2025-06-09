using BreezyDrive.AuthenticationServices.Domain.Interfaces;
using BreezyDrive.CommonService.Infrastuctures.Messaging;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;

namespace BreezyDrive.AuthenticationServices.Application.Messaging
{
    public class GetUserIdFronHttpContextHandler : IMessageHandler<GetUserIdFromHttpContextRequestEvent, GetUserIdFromHttpContextResponseEvent>
    {
        private readonly IAuthentication _authentication;

        public GetUserIdFronHttpContextHandler(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        public Task<GetUserIdFromHttpContextResponseEvent> HandleMessageAsync(GetUserIdFromHttpContextRequestEvent message)
        {
            try
            {
                var userId = _authentication.GetUserIdFromToken(message.JwtToken);

                return Task.FromResult(new GetUserIdFromHttpContextResponseEvent
                {
                    IsSuccess = true,
                    UserId = userId
                });
            }
            catch (Exception)
            {
                return Task.FromResult(new GetUserIdFromHttpContextResponseEvent
                {
                    IsSuccess = false
                });
            }
        }
    }
}