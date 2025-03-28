using AutoMapper;
using BreezyDrive.CommonService.Infrastuctures.Messaging;
using BreezyDrive.UserServices.Application.DTOs.Request;
using BreezyDrive.UserServices.Application.Interfaces;
using Library.EventContracts.Events.CommonResponse;
using Library.EventContracts.Events.UserEvents.Request;

namespace BreezyDrive.UserServices.Application.Messaging
{
    public class CreateUserHandler : IMessageHandler<RegisterRequestEvent, EventSuccessResponse>
    {
        private readonly IUserService _userService;

        public CreateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<EventSuccessResponse> HandleMessageAsync(RegisterRequestEvent message)
        {
            var userRequest = new CreateUserRequest
            {
                FullName = message.FullName,
                Email = message.Email,
                Password = message.Password,
                Phone = message.Phone,
            };

            await _userService.CreateUser(userRequest);
            
            return new EventSuccessResponse{IsSuccess = true};
        }
    }
}
