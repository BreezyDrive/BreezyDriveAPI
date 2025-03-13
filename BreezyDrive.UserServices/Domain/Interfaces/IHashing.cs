namespace BreezyDrive.UserServices.Domain.Interfaces
{
    public interface IHashing
    {
        string SHA512Hash(string text);
    }
}
