using BreezyDrive.CommonService.Infrastuctures.Messaging;
using Library.EventContracts.Events;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;
using MassTransit;

namespace BreezyDrive.ConversationServices.Application.Messaging;

public class CheckUserExistHandler : IMessageHandler<CheckUserExistRequestEvent, CheckUserExistResponse>
{
    private readonly IRequestClient<CheckUserExistRequestEvent> _requestClient;
    private readonly ILogger<CheckUserExistHandler> _logger;

    public CheckUserExistHandler(IRequestClient<CheckUserExistRequestEvent> requestClient, ILogger<CheckUserExistHandler> logger)
    {
        _requestClient = requestClient;
        _logger = logger;
    }
    
    public async Task<CheckUserExistResponse> HandleMessageAsync(CheckUserExistRequestEvent message)
    {
        _logger.LogInformation("Sending CheckUserExistRequestEvent for user {UserId}", message.UserId);

        try
        {
            // Send request and wait for response
            var response = await _requestClient.GetResponse<CheckUserExistResponse>(message);
            
            _logger.LogInformation("Received response for user {UserId}: {Exists}", 
                message.UserId, response.Message.IsUserExists);

            return response.Message;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking user existence for {UserId}", message.UserId);
            throw;
        }
    }
}