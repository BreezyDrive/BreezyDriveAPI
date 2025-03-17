namespace Library.EventContracts.Events.UserEvents;

public record CheckUserExistRequest
{
    public Guid UserId { get; init; }

}