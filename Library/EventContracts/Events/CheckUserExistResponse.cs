namespace Library.EventContracts.Events;

public record CheckUserExistResponse()
{
    public bool IsUserExists { get; set; }
}