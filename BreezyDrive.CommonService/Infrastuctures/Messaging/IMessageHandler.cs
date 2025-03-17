namespace BreezyDrive.CommonService.Infrastuctures.Messaging;

public interface IMessageHandler <TMessage, TResponse>
{
    Task<TResponse> HandleMessageAsync(TMessage message);

}