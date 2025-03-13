using BreezyDrive.UserServices.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace BreezyDrive.UserServices.Infrastructure.Identity
{
    public class Hash : IHashing
    {
        public string SHA512Hash(string text)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] input = Encoding.ASCII.GetBytes(text);
            byte[] hashBytes = sha512.ComputeHash(input);
            return Convert.ToHexString(hashBytes);
        }
    }
}
