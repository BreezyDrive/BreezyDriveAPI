using System.Security.Cryptography;
using System.Text;
using BreezyDrive.CommonService.Domain.Interfaces;

namespace BreezyDrive.CommonService.Utils
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
