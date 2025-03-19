using BreezyDrive.CommonService.Infrastuctures.Messaging;
using BreezyDrive.UserServices.Application.DTOs.Request;
using BreezyDrive.UserServices.Application.Interfaces;
using Library.EventContracts.Events.UserEvents.Request;

namespace BreezyDrive.UserServices.Application.Messaging
{
    public class RegisterGoogleHandler : IMessageHandler<RegisterGoogleRequestEvent, bool>
    {
        private readonly IUserService _userService;
        private readonly ILogger<RegisterGoogleRequestEvent> _logger;

        public RegisterGoogleHandler(IUserService userService, ILogger<RegisterGoogleRequestEvent> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<bool> HandleMessageAsync(RegisterGoogleRequestEvent message)
        {
            var userRequest = new CreateUserRequest
            {
                FullName = message.FullName,
                Email = message.Email,
                Password = message.Password,
            };

            var newUser = await _userService.CreateUser(userRequest);

            return true;
        }
    }
}
