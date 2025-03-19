using BreezyDrive.CommonService.Infrastuctures.Messaging;
using BreezyDrive.UserServices.Application.Interfaces;
using Library.EventContracts.Events;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;

namespace BreezyDrive.UserServices.Application.Messaging;

public class CheckUserExistHandler : IMessageHandler<CheckUserExistRequest, CheckUserExistResponse>
{
    private readonly IUserService _userService;
    private readonly ILogger<CheckUserExistHandler> _logger;

    public CheckUserExistHandler(IUserService userService, ILogger<CheckUserExistHandler> logger)
    {
        _userService = userService;
        _logger = logger;
    }
    
    public async Task<CheckUserExistResponse> HandleMessageAsync(CheckUserExistRequest message)
    {
        _logger.LogInformation("Handling CheckUserExistRequest for user {UserId}", message.UserId);

        var user = await _userService.isUserExists(message.UserId);
        
        _logger.LogWarning("User {UserId} not found", message.UserId);

        return new CheckUserExistResponse { IsUserExists = user };

    }
}