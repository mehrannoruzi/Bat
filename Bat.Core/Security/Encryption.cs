namespace Bat.Core;

/// <summary>
/// Sample :
/// String cipherText = Encryption.Encrypt(_SampleString);
/// String PlaneText = Encryption.Decrypt(_EncryptedString);
/// </summary>
public class Encryption
{
    /// <summary>
    /// Encrypts specified plainText as symmetric key algorithm
    /// and returns a Encrypted result.
    /// </summary>
    /// <param name="plainText">A plainText String To Encrypt</param>
    /// <returns>Return String cipherText</returns>
    public static string Encrypt(string plainText)
    {
        return RijndaelAlgorithm.Encrypt(plainText, "!@#$%^^%$#@!", "!@#$%^", "MD5", 1, "XYZxyzAZSawsTRCE", 128);
    }

    /// <summary>
    /// Encrypts specified plainText as symmetric key algorithm
    /// and returns a Encrypted result.
    /// </summary>
    /// <param name="plainText">A plainText String To Encrypt</param>
    /// <param name="algoritm">Set Key Hash Algoritm</param>
    /// <param name="keySize">Set Key Size Of Encryption</param>
    /// <returns></returns>
    public static string Encrypt(string plainText, HashAlgorithm algoritm = HashAlgorithm.MD5,
        EncryptKeySize keySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (algoritm)
        {
            case HashAlgorithm.MD4:
                KeyAlgoritm = "MD4";
                break;
            case HashAlgorithm.MD5:
                KeyAlgoritm = "MD5";
                break;
            case HashAlgorithm.SHA1:
                KeyAlgoritm = "SHA1";
                break;
            case HashAlgorithm.SHA256:
                KeyAlgoritm = "SHA256";
                break;
            case HashAlgorithm.SHA384:
                KeyAlgoritm = "SHA384";
                break;
            case HashAlgorithm.SHA512:
                KeyAlgoritm = "SHA512";
                break;
        }

        return RijndaelAlgorithm.Encrypt(plainText, "!@#$%^^%$#@!", "!@#$%^", KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)keySize);
    }

    /// <summary>
    /// Encrypts specified plainText as symmetric key algorithm
    /// and returns a Encrypted Value.
    /// </summary>
    /// <param name="plainText">A plainText String To Encrypt</param>
    /// <param name="EncryptKey">The Key Of Symmetric Encryption</param>
    /// <param name="algoritm">Set Key Hash Algoritm</param>
    /// <param name="keySize">Set Key Size Of Encryption</param>
    /// <returns>Encrypted Value Formatted As a Base 64-encoded String.</returns>
    public static string Encrypt(string plainText, string encryptKey,
        HashAlgorithm algoritm = HashAlgorithm.MD5,
        EncryptKeySize keySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (algoritm)
        {
            case HashAlgorithm.MD4:
                KeyAlgoritm = "MD4";
                break;
            case HashAlgorithm.MD5:
                KeyAlgoritm = "MD5";
                break;
            case HashAlgorithm.SHA1:
                KeyAlgoritm = "SHA1";
                break;
            case HashAlgorithm.SHA256:
                KeyAlgoritm = "SHA256";
                break;
            case HashAlgorithm.SHA384:
                KeyAlgoritm = "SHA384";
                break;
            case HashAlgorithm.SHA512:
                KeyAlgoritm = "SHA512";
                break;
        }

        return RijndaelAlgorithm.Encrypt(plainText, encryptKey, "!@#$%^", KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)keySize);
    }

    /// <summary>
    /// /// Encrypts specified plainText as symmetric key algorithm
    /// and returns a Encrypted result.
    /// </summary>
    /// <param name="plainText">A plainText String To Encrypt</param>
    /// <param name="EncryptKey">The Key Of Symmetric Encryption</param>
    /// <param name="Salt">A String To Combine With EncryptKey To Symmetric Encryption</param>
    /// <param name="algoritm">Set Key Hash Algoritm</param>
    /// <param name="keySize">Set Key Size Of Encryption</param>
    /// <returns>Encrypted Value Formatted As a Base 64-encoded String.</returns>
    public static string Encrypt(string plainText, string encryptKey,
        string Salt, HashAlgorithm algoritm = HashAlgorithm.MD5,
        EncryptKeySize keySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (algoritm)
        {
            case HashAlgorithm.MD4:
                KeyAlgoritm = "MD4";
                break;
            case HashAlgorithm.MD5:
                KeyAlgoritm = "MD5";
                break;
            case HashAlgorithm.SHA1:
                KeyAlgoritm = "SHA1";
                break;
            case HashAlgorithm.SHA256:
                KeyAlgoritm = "SHA256";
                break;
            case HashAlgorithm.SHA384:
                KeyAlgoritm = "SHA384";
                break;
            case HashAlgorithm.SHA512:
                KeyAlgoritm = "SHA512";
                break;
        }

        return RijndaelAlgorithm.Encrypt(plainText, encryptKey, Salt, KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)keySize);
    }

    /// <summary>
    /// Encrypts specified plainText as symmetric key algorithm
    /// and returns a Encrypted result.
    /// </summary>
    /// <param name="plainText">A plainText String To Encrypt</param>
    /// <param name="EncryptKey">The Key Of Symmetric Encryption</param>
    /// <param name="Salt">A String To Combine With EncryptKey To Symmetric Encryption</param>
    /// <param name="InitVector">A String To Encrypt The First Block Of plainText</param>
    /// <param name="algoritm">Set Key Hash Algoritm</param>
    /// <param name="keySize">Set Key Size Of Encryption</param>
    /// <returns>Encrypted Value Formatted As a Base 64-encoded String.</returns>
    public static string Encrypt(string plainText, string encryptKey,
        string Salt, string InitVector, HashAlgorithm algoritm = HashAlgorithm.MD5,
        EncryptKeySize keySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (algoritm)
        {
            case HashAlgorithm.MD4:
                KeyAlgoritm = "MD4";
                break;
            case HashAlgorithm.MD5:
                KeyAlgoritm = "MD5";
                break;
            case HashAlgorithm.SHA1:
                KeyAlgoritm = "SHA1";
                break;
            case HashAlgorithm.SHA256:
                KeyAlgoritm = "SHA256";
                break;
            case HashAlgorithm.SHA384:
                KeyAlgoritm = "SHA384";
                break;
            case HashAlgorithm.SHA512:
                KeyAlgoritm = "SHA512";
                break;
        }

        return RijndaelAlgorithm.Encrypt(plainText, encryptKey, Salt, KeyAlgoritm, 1, InitVector, (int)keySize);
    }



    /// <summary>
    ///  Decrypts specified cipherText as symmetric key algorithm.
    ///  and returns a Decrypted result.
    /// </summary>
    /// <param name="cipherText">A cipherText As a Base 64-encoded String</param>
    /// <param name="Algoritm">Set Key Hash Algoritm</param>
    /// <param name="KeySize">Set Key Size Of Encryption</param>
    /// <returns>Decrypted string Value.</returns>
    public static string Decrypt(string cipherText)
    {
        return RijndaelAlgorithm.Decrypt(cipherText.Replace(' ', '+'), "!@#$%^^%$#@!", "!@#$%^", "MD5", 1, "XYZxyzAZSawsTRCE", 128);
    }

    /// <summary>
    ///  Decrypts specified cipherText as symmetric key algorithm.
    ///  and returns a Decrypted result.
    /// </summary>
    /// <param name="cipherText">A cipherText As a Base 64-encoded String</param>
    /// <param name="algoritm">Set Key Hash Algoritm</param>
    /// <param name="keySize">Set Key Size Of Encryption</param>
    /// <returns>Decrypted string Value.</returns>
    public static string Decrypt(string cipherText, HashAlgorithm algoritm = HashAlgorithm.MD5,
        EncryptKeySize keySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (algoritm)
        {
            case HashAlgorithm.MD4:
                KeyAlgoritm = "MD4";
                break;
            case HashAlgorithm.MD5:
                KeyAlgoritm = "MD5";
                break;
            case HashAlgorithm.SHA1:
                KeyAlgoritm = "SHA1";
                break;
            case HashAlgorithm.SHA256:
                KeyAlgoritm = "SHA256";
                break;
            case HashAlgorithm.SHA384:
                KeyAlgoritm = "SHA384";
                break;
            case HashAlgorithm.SHA512:
                KeyAlgoritm = "SHA512";
                break;
        }

        return RijndaelAlgorithm.Decrypt(cipherText.Replace(' ', '+'), "!@#$%^^%$#@!", "!@#$%^", KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)keySize);
    }

    /// <summary>
    ///  Decrypts specified cipherText as symmetric key algorithm.
    ///  and returns a Decrypted result.
    /// </summary>
    /// <param name="cipherText">A cipherText As a Base 64-encoded String</param>
    /// <param name="EncryptKey">The Key Of Symmetric Encryption</param>
    /// <param name="algoritm">Set Key Hash Algoritm</param>
    /// <param name="keySize">Set Key Size Of Encryption</param>
    /// <returns>Decrypted string Value.</returns>
    public static string Decrypt(string cipherText, string encryptKey,
        HashAlgorithm algoritm = HashAlgorithm.MD5,
        EncryptKeySize keySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (algoritm)
        {
            case HashAlgorithm.MD4:
                KeyAlgoritm = "MD4";
                break;
            case HashAlgorithm.MD5:
                KeyAlgoritm = "MD5";
                break;
            case HashAlgorithm.SHA1:
                KeyAlgoritm = "SHA1";
                break;
            case HashAlgorithm.SHA256:
                KeyAlgoritm = "SHA256";
                break;
            case HashAlgorithm.SHA384:
                KeyAlgoritm = "SHA384";
                break;
            case HashAlgorithm.SHA512:
                KeyAlgoritm = "SHA512";
                break;
        }

        return RijndaelAlgorithm.Decrypt(cipherText.Replace(' ', '+'), encryptKey, "!@#$%^", KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)keySize);
    }

    /// <summary>
    ///  Decrypts specified cipherText as symmetric key algorithm.
    ///  and returns a Decrypted result.
    /// </summary>
    /// <param name="cipherText">A cipherText As a Base 64-encoded String</param>
    /// <param name="EncryptKey">The Key Of Symmetric Encryption</param>
    /// <param name="Salt">A String To Combine With EncryptKey To Symmetric Encryption</param>
    /// <param name="algoritm">Set Key Hash Algoritm</param>
    /// <param name="keySize">Set Key Size Of Encryption</param>
    /// <returns>Decrypted string Value.</returns>
    public static string Decrypt(string cipherText, string encryptKey,
        string Salt, HashAlgorithm algoritm = HashAlgorithm.MD5,
        EncryptKeySize keySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (algoritm)
        {
            case HashAlgorithm.MD4:
                KeyAlgoritm = "MD4";
                break;
            case HashAlgorithm.MD5:
                KeyAlgoritm = "MD5";
                break;
            case HashAlgorithm.SHA1:
                KeyAlgoritm = "SHA1";
                break;
            case HashAlgorithm.SHA256:
                KeyAlgoritm = "SHA256";
                break;
            case HashAlgorithm.SHA384:
                KeyAlgoritm = "SHA384";
                break;
            case HashAlgorithm.SHA512:
                KeyAlgoritm = "SHA512";
                break;
        }

        return RijndaelAlgorithm.Decrypt(cipherText.Replace(' ', '+'), encryptKey, Salt, KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)keySize);
    }

    /// <summary>
    ///  Decrypts specified cipherText as symmetric key algorithm.
    ///  and returns a Decrypted result.
    /// </summary>
    /// <param name="cipherText">A cipherText As a Base 64-encoded String</param>
    /// <param name="EncryptKey">The Key Of Symmetric Encryption</param>
    /// <param name="Salt">A String To Combine With EncryptKey To Symmetric Encryption</param>
    /// <param name="InitVector">A String To Encrypt The First Block Of plainText</param>
    /// <param name="algoritm">Set Key Hash Algoritm</param>
    /// <param name="keySize">Set Key Size Of Encryption</param>
    /// <returns>Decrypted string Value.</returns>
    public static string Decrypt(string cipherText, string encryptKey,
        string Salt, string InitVector, HashAlgorithm algoritm = HashAlgorithm.MD5,
        EncryptKeySize keySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (algoritm)
        {
            case HashAlgorithm.MD4:
                KeyAlgoritm = "MD4";
                break;
            case HashAlgorithm.MD5:
                KeyAlgoritm = "MD5";
                break;
            case HashAlgorithm.SHA1:
                KeyAlgoritm = "SHA1";
                break;
            case HashAlgorithm.SHA256:
                KeyAlgoritm = "SHA256";
                break;
            case HashAlgorithm.SHA384:
                KeyAlgoritm = "SHA384";
                break;
            case HashAlgorithm.SHA512:
                KeyAlgoritm = "SHA512";
                break;
        }

        return RijndaelAlgorithm.Decrypt(cipherText.Replace(' ', '+'), encryptKey, Salt, KeyAlgoritm, 1, InitVector, (int)keySize);
    }
}