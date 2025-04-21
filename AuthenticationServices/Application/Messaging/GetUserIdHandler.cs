using BreezyDrive.AuthenticationServices.Domain.Interfaces;
using BreezyDrive.CommonService.Infrastuctures.Messaging;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;

namespace BreezyDrive.AuthenticationServices.Application.Messaging
{
    public class GetUserIdHandler : IMessageHandler<GetUserIdRequestEvent, GetUserIdResponseEvent>
    {
        private readonly IAuthentication _authentication;

        public GetUserIdHandler(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        public Task<GetUserIdResponseEvent> HandleMessageAsync(GetUserIdRequestEvent message)
        {
            try
            {
                var userId = _authentication.GetUserIdFromToken(message.JwtToken);

                return Task.FromResult(new GetUserIdResponseEvent
                {
                    IsSuccess = true,
                    UserId = userId
                });
            }
            catch (Exception)
            {
                return Task.FromResult(new GetUserIdResponseEvent
                {
                    IsSuccess = false
                });
            }
        }
    }
}