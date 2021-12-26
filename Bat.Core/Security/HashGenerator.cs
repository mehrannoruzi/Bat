using System;
using System.Text;
using System.Security.Cryptography;

namespace Bat.Core
{
    public enum HashAlgorithms
    {
        MD5 = 1,
        SHA1 = 2,
        SHA256 = 3,
        SHA384 = 4,
        SHA512 = 5
    }

    public class HashGenerator
    {
        public static string Hash(string key, HashAlgorithms hashAlgorithm = HashAlgorithms.SHA256)
        {
            string salt = "Hill@v@$";
            byte[] bytes = Encoding.Unicode.GetBytes(key);
            byte[] src = Encoding.Unicode.GetBytes(salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create(hashAlgorithm.ToString());
            byte[] inarray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inarray);
        }

        public static string Hash(string key, string salt, HashAlgorithms hashAlgorithm = HashAlgorithms.SHA256)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(key);
            byte[] src = Encoding.Unicode.GetBytes(salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create(hashAlgorithm.ToString());
            byte[] inarray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inarray);
        }

        public static bool IsCorrectHash(string hashedKey, HashAlgorithms hashAlgorithm = HashAlgorithms.SHA256)
        {
            switch (hashAlgorithm)
            {
                case HashAlgorithms.MD5:
                    return (hashedKey.Length == 24 && hashedKey.Contains("="));

                case HashAlgorithms.SHA1:
                    return (hashedKey.Length == 28 && hashedKey.Contains("="));

                case HashAlgorithms.SHA256:
                    return (hashedKey.Length == 44 && hashedKey.Contains("="));

                case HashAlgorithms.SHA384:
                    return (hashedKey.Length == 64);

                case HashAlgorithms.SHA512:
                    return (hashedKey.Length == 88 && hashedKey.Contains("="));

                default:
                    return (hashedKey.Length == 44 && hashedKey.Contains("="));
            }

        }

        public static bool VerifyHash(string key, string hashedKey, HashAlgorithms hashAlgorithm = HashAlgorithms.SHA256)
        {
            if (!IsCorrectHash(hashedKey, hashAlgorithm)) return false;

            return Hash(key, hashAlgorithm) == hashedKey;
        }

        public static bool VerifyHash(string key, string hashedKey, string salt, HashAlgorithms hashAlgorithm = HashAlgorithms.SHA256)
        {
            if (!IsCorrectHash(hashedKey, hashAlgorithm)) return false;

            return Hash(key, salt, hashAlgorithm) == hashedKey;
        }

    }
}