using BreezyDrive.CommonService.Infrastuctures.Messaging;
using BreezyDrive.UserServices.Application.DTOs.Request;
using BreezyDrive.UserServices.Application.Interfaces;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;

namespace BreezyDrive.UserServices.Application.Messaging
{
    public class RegisterGoogleHandler : IMessageHandler<RegisterGoogleRequestEvent, EventSuccessResponse>
    {
        private readonly IUserService _userService;
        private readonly ILogger<RegisterGoogleRequestEvent> _logger;

        public RegisterGoogleHandler(IUserService userService, ILogger<RegisterGoogleRequestEvent> logger)
        {
            _userService = userService;
            _logger = logger;
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
