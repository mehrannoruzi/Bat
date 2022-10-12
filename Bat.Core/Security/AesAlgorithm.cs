using System.Text;
using System.Security.Cryptography;

namespace Bat.Core;

public class AesAlgorithm
{
    public static string Encrypt(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
    {
        var initVectorBytes = Encoding.ASCII.GetBytes(initVector);
        var saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
        var password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
        var keyBytes = password.GetBytes(keySize / 8);
        var symmetricKey = Aes.Create();
        symmetricKey.Key = keyBytes;
        symmetricKey.IV = initVectorBytes;
        var encryptor = symmetricKey.CreateEncryptor();
        byte[] cipherTextBytes;
        using (var memoryStream = new MemoryStream())
        {
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            using (var streamWriter = new StreamWriter(cryptoStream))
            {
                streamWriter.Write(plainText);
            }
            cipherTextBytes = memoryStream.ToArray();
        }
        return Convert.ToBase64String(cipherTextBytes);
    }

    public static string Decrypt(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
    {
        var initVectorBytes = Encoding.ASCII.GetBytes(initVector);
        var saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
        var cipherTextBytes = Convert.FromBase64String(cipherText);
        var password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
        var keyBytes = password.GetBytes(keySize / 8);
        var symmetricKey = Aes.Create();
        symmetricKey.Key = keyBytes;
        symmetricKey.IV = initVectorBytes;
        var decryptor = symmetricKey.CreateDecryptor();
        using var memoryStream = new MemoryStream(cipherTextBytes);
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream);
        return streamReader.ReadToEnd();
    }

}