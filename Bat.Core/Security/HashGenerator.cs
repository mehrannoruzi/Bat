using System.Text;

namespace Bat.Core;

public class HashGenerator
{
    public static string Hash(string key, HashAlgorithm hashAlgorithm = HashAlgorithm.SHA256)
    {
        var salt = "M3hr@nN0r0uz!";
        var bytes = Encoding.Unicode.GetBytes(key);
        var src = Encoding.Unicode.GetBytes(salt);
        var dst = new byte[src.Length + bytes.Length];
        Buffer.BlockCopy(src, 0, dst, 0, src.Length);
        Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
        var algorithm = System.Security.Cryptography.HashAlgorithm.Create(hashAlgorithm.ToString());
        var inarray = algorithm.ComputeHash(dst);
        return Convert.ToBase64String(inarray);
    }

    public static string Hash(string key, string salt, HashAlgorithm hashAlgorithm = HashAlgorithm.SHA256)
    {
        var bytes = Encoding.Unicode.GetBytes(key);
        var src = Encoding.Unicode.GetBytes(salt);
        var dst = new byte[src.Length + bytes.Length];
        Buffer.BlockCopy(src, 0, dst, 0, src.Length);
        Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
        var algorithm = System.Security.Cryptography.HashAlgorithm.Create(hashAlgorithm.ToString());
        var inarray = algorithm.ComputeHash(dst);
        return Convert.ToBase64String(inarray);
    }

    public static bool IsCorrectHash(string hashedKey, HashAlgorithm hashAlgorithm = HashAlgorithm.SHA256)
    {
        switch (hashAlgorithm)
        {
            case HashAlgorithm.MD5:
                return (hashedKey.Length == 24 && hashedKey.Contains("="));

            case HashAlgorithm.SHA1:
                return (hashedKey.Length == 28 && hashedKey.Contains("="));

            case HashAlgorithm.SHA256:
                return (hashedKey.Length == 44 && hashedKey.Contains("="));

            case HashAlgorithm.SHA384:
                return (hashedKey.Length == 64);

            case HashAlgorithm.SHA512:
                return (hashedKey.Length == 88 && hashedKey.Contains("="));

            default:
                return (hashedKey.Length == 44 && hashedKey.Contains("="));
        }

    }

    public static bool VerifyHash(string key, string hashedKey, HashAlgorithm hashAlgorithm = HashAlgorithm.SHA256)
    {
        if (!IsCorrectHash(hashedKey, hashAlgorithm)) return false;

        return Hash(key, hashAlgorithm) == hashedKey;
    }

    public static bool VerifyHash(string key, string hashedKey, string salt, HashAlgorithm hashAlgorithm = HashAlgorithm.SHA256)
    {
        if (!IsCorrectHash(hashedKey, hashAlgorithm)) return false;

        return Hash(key, salt, hashAlgorithm) == hashedKey;
    }
}