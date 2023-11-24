using System.Text;

namespace Bat.Core;

public class HashGenerator
{
    public static string Hash(string key)
	{
		var salt = "M3hr@nN0r0uz!";
		var keyBytes = Encoding.Unicode.GetBytes(key);
		var saltBytes = Encoding.Unicode.GetBytes(salt);
		var dataBytes = new byte[saltBytes.Length + keyBytes.Length];
		Buffer.BlockCopy(saltBytes, 0, dataBytes, 0, saltBytes.Length);
		Buffer.BlockCopy(keyBytes, 0, dataBytes, saltBytes.Length, keyBytes.Length);
		return Convert.ToBase64String(System.Security.Cryptography.SHA256.HashData(dataBytes));
	}

	public static string Hash(string key, string salt)
    {
        var keyBytes = Encoding.Unicode.GetBytes(key);
        var saltBytes = Encoding.Unicode.GetBytes(salt);
        var dataBytes = new byte[saltBytes.Length + keyBytes.Length];
        Buffer.BlockCopy(saltBytes, 0, dataBytes, 0, saltBytes.Length);
        Buffer.BlockCopy(keyBytes, 0, dataBytes, saltBytes.Length, keyBytes.Length);
		return Convert.ToBase64String(System.Security.Cryptography.SHA256.HashData(dataBytes));
	}

    public static bool IsCorrectHash(string hashedKey, HashAlgorithm hashAlgorithm = HashAlgorithm.SHA256)
    {
		return hashAlgorithm switch
		{
			HashAlgorithm.MD5 => (hashedKey.Length == 24 && hashedKey.Contains('=')),
			HashAlgorithm.SHA1 => (hashedKey.Length == 28 && hashedKey.Contains('=')),
			HashAlgorithm.SHA256 => (hashedKey.Length == 44 && hashedKey.Contains('=')),
			_ => (hashedKey.Length == 44 && hashedKey.Contains('=')),
		};
	}

	public static bool VerifyHash(string key, string hashedKey, HashAlgorithm hashAlgorithm = HashAlgorithm.SHA256)
    {
        if (!IsCorrectHash(hashedKey, hashAlgorithm)) return false;

        return Hash(key) == hashedKey;
    }

    public static bool VerifyHash(string key, string hashedKey, string salt, HashAlgorithm hashAlgorithm = HashAlgorithm.SHA256)
    {
        if (!IsCorrectHash(hashedKey, hashAlgorithm)) return false;

        return Hash(key, salt) == hashedKey;
    }
}