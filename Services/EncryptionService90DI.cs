using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Services_90DI
{
    public class EncryptionService90DI
    {
        
            private readonly byte[] _key;

            public EncryptionService90DI(string base64Key)
            {
                _key = Convert.FromBase64String(base64Key);
            }

            public string Encrypt(string plainText)
            {
                var nonce = RandomNumberGenerator.GetBytes(12);
                var tag = new byte[16];
                var cipherText = new byte[Encoding.UTF8.GetByteCount(plainText)];

                using var aes = new AesGcm(_key, 16);
                aes.Encrypt(nonce, Encoding.UTF8.GetBytes(plainText), cipherText, tag);

                return $"{Convert.ToBase64String(nonce)}.{Convert.ToBase64String(cipherText)}.{Convert.ToBase64String(tag)}";
            }

        public string Decrypt(string encryptedText)
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
    }
}

