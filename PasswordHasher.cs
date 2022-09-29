using System.Security.Cryptography;
using System.Text;

namespace LabTest
{
    public static class PasswordHasher
    {
        public static string ComputeSha512Hash(string plainPassword)
        {
            var plainPasswordBytes = Encoding.ASCII.GetBytes(plainPassword);

            var sha512CryptoServiceProvider = new SHA512CryptoServiceProvider();
            var hashBytes = sha512CryptoServiceProvider.ComputeHash(plainPasswordBytes);

            var hash = Encoding.ASCII.GetString(hashBytes);

            return hash;
        }
    }
}
