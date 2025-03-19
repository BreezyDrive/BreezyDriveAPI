using AutoMapper;
using BreezyDrive.CommonService.Infrastuctures.Messaging;
using BreezyDrive.UserServices.Application.DTOs.Request;
using BreezyDrive.UserServices.Application.Interfaces;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;

namespace BreezyDrive.UserServices.Application.Messaging
{
    public class CreateUserHandler : IMessageHandler<RegisterRequestEvent, bool>
    {
        private readonly IUserService _userService;
        private readonly ILogger<CreateUserHandler> _logger;

        public CreateUserHandler(IUserService userService, ILogger<CreateUserHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<bool> HandleMessageAsync(RegisterRequestEvent message)
        {
            var userRequest = new CreateUserRequest
            {
                FullName = message.FullName,
                Email = message.Email,
                Password = message.Password,
                Phone = message.Phone,
            };

            var newUser = await _userService.CreateUser(userRequest);
            return true;
        }
    }
}
