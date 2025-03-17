namespace Library.EventContracts.Events;

public record CheckUserExistRequest
{
    public Guid UserId { get; init; }

}