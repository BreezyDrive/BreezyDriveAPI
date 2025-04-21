using BreezyDrive.CommonService.Infrastuctures.Messaging;
using BreezyDrive.UserServices.Application.DTOs.Request;
using BreezyDrive.UserServices.Application.Interfaces;
using Library.EventContracts.Events.CommonResponse;
using Library.EventContracts.Events.UserEvents.Request;

namespace BreezyDrive.UserServices.Application.Messaging
{
    public class RegisterGoogleHandler : IMessageHandler<RegisterGoogleRequestEvent, EventSuccessResponse>
    {
        private readonly IUserService _userService;

        public RegisterGoogleHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<EventSuccessResponse> HandleMessageAsync(RegisterGoogleRequestEvent message)
        {
            var userRequest = new CreateUserRequest
            {
                FullName = message.FullName,
                Email = message.Email,
                Password = message.Password,
            };

            await _userService.CreateUser(userRequest);

            return new EventSuccessResponse{IsSuccess = true};
        }
    }
}
