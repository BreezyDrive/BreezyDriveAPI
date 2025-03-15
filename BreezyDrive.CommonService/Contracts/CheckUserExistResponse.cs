namespace BreezyDrive.CommonService.Contracts;

public record CheckUserExistResponse()
{
    public bool IsUserExists { get; set; }
}