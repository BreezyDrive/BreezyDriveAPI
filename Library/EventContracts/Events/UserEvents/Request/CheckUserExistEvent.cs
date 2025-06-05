namespace Library.EventContracts.Events.UserEvents.Request;

public record CheckUserExistRequestEvent
{
    public Guid UserId { get; init; }

}