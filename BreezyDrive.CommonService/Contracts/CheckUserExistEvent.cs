namespace BreezyDrive.CommonService.Contracts;

public record CheckUserExistRequest
{
    public Guid UserId { get; init; }

}