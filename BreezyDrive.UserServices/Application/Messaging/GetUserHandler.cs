using BreezyDrive.CommonService.Infrastuctures.Messaging;
using BreezyDrive.UserServices.Application.Interfaces;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;

namespace BreezyDrive.UserServices.Application.Messaging
{
    public class GetUserHandler : IMessageHandler<GetUserRequestEvent, GetUserResponseEvent>
    {
        private readonly IUserService _userService;
        private readonly ILogger<GetUserHandler> _logger;

        public GetUserHandler(IUserService userService, ILogger<GetUserHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<GetUserResponseEvent> HandleMessageAsync(GetUserRequestEvent message)
        {
            var checkAccount = string.IsNullOrEmpty(message.Email)
            ? await _userService.CheckPhonePassword(message.Phone, message.Password)
            : await _userService.CheckGoogleEmail(message.Email);

            return new GetUserResponseEvent
            {
                Id = checkAccount.Id,
                RoleId = checkAccount.RoleId,
                FullName = checkAccount.FullName,
                Email = checkAccount.Email,
                Phone = checkAccount.Phone,
                Avatar = checkAccount.Avatar
            };
        }
    }
}
