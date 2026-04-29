using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BLL
{
    public static class PasswordHasher90DI
    {
        public static string HashPassword90DI(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(16);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 350_000, HashAlgorithmName.SHA256, 32);
            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public static bool Verify90DI(string password, string storedHash)
        {
            var parts = storedHash.Split('.');
            var salt = Convert.FromBase64String(parts[0]);
            var expectedHash = Convert.FromBase64String(parts[1]);

            var newHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 350_000, HashAlgorithmName.SHA256, 32);

            return CryptographicOperations.FixedTimeEquals(expectedHash, newHash);
        }
    }
}
