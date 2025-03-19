namespace Library.EventContracts.Events.UserEvents.Response;

public record CheckUserExistResponse()
{
    public bool IsUserExists { get; set; }
}