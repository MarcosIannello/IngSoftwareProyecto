using System.Security.Cryptography;
using System.Text;

namespace Services_90DI
{
    public static class SecurityService_90DI
    {
        private static byte[] _key;

        public static void Initialize(string base64Key)
        {
            _key = Convert.FromBase64String(base64Key);
        }

        public static string Encrypt(string plainText)
        {
            var nonce = RandomNumberGenerator.GetBytes(12);
            var tag = new byte[16];
            var cipherText = new byte[Encoding.UTF8.GetByteCount(plainText)];

            using var aes = new AesGcm(_key, 16);
            aes.Encrypt(nonce, Encoding.UTF8.GetBytes(plainText), cipherText, tag);

            return $"{Convert.ToBase64String(nonce)}.{Convert.ToBase64String(cipherText)}.{Convert.ToBase64String(tag)}";
        }

        public static string Decrypt(string encryptedText)
        {
            var parts = encryptedText.Split('.');
            var nonce = Convert.FromBase64String(parts[0]);
            var cipherText = Convert.FromBase64String(parts[1]);
            var tag = Convert.FromBase64String(parts[2]);
            var plainText = new byte[cipherText.Length];

            using var aes = new AesGcm(_key, 16);
            aes.Decrypt(nonce, cipherText, tag, plainText);

            return Encoding.UTF8.GetString(plainText);
        }

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
