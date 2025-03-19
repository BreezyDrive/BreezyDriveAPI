using BreezyDrive.CommonService.Infrastuctures.Messaging;
using BreezyDrive.UserServices.Application.Interfaces;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;

namespace BreezyDrive.UserServices.Application.Messaging
{
    public class CheckGoogleEmailHandler : IMessageHandler<CheckGoogleExistRequestEvent, CheckGoogleExistResponseEvent>
    {
        private readonly IUserService _userService;
        private readonly ILogger<CheckGoogleEmailHandler> _logger;

        public CheckGoogleEmailHandler(IUserService userService, ILogger<CheckGoogleEmailHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<CheckGoogleExistResponseEvent> HandleMessageAsync(CheckGoogleExistRequestEvent message)
        {
            var checkGoogleEmail = await _userService.CheckGoogleEmail(message.Email);

            var checkGoogleEmailResponse = new CheckGoogleExistResponseEvent
            {
                Id = checkGoogleEmail.Id,
                RoleId = checkGoogleEmail.RoleId,
                FullName = checkGoogleEmail.FullName,
                Email = checkGoogleEmail.Email,
                Phone = checkGoogleEmail.Phone,
                Avatar = checkGoogleEmail.Avatar
            };

            return checkGoogleEmailResponse;
        }
    }
}
