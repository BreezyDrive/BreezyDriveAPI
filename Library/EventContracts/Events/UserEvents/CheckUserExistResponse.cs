namespace Library.EventContracts.Events.UserEvents;

public record CheckUserExistResponse()
{
    public bool IsUserExists { get; set; }
}