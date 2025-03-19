namespace Library.EventContracts.Events.UserEvents.Request;

public record CheckUserExistRequest
{
    public Guid UserId { get; init; }

}