using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BreezyDrive.CommonService.Infrastuctures.Messaging;

public class GenericConsumer <TMessage, TResponse> : IConsumer<TMessage> where TMessage : class
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<GenericConsumer<TMessage, TResponse>> _logger;

    public GenericConsumer(IServiceScopeFactory serviceScopeFactory, ILogger<GenericConsumer<TMessage, TResponse>> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }
    
    
    public async Task Consume(ConsumeContext<TMessage> context)
    {
        _logger.LogInformation("Received message of type {MessageType}", typeof(TMessage).Name);

        using var scope = _serviceScopeFactory.CreateScope();
        var handler = scope.ServiceProvider.GetService<IMessageHandler<TMessage, TResponse>>();

        if (handler != null)
        {
            var response = await handler.HandleMessageAsync(context.Message);

            if (response != null)
            {
                await context.RespondAsync(response);
                _logger.LogInformation("Response sent successfully for message type {MessageType}", typeof(TMessage).Name);
            }
            else
            {
                _logger.LogWarning("Handler returned null response for message type {MessageType}", typeof(TMessage).Name);
            }
            
        }
        else
        {
            _logger.LogWarning("No handler found for message type {MessageType}", typeof(TMessage).Name);
        }    
    }
}