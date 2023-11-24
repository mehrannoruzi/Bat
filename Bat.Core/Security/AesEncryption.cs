namespace Bat.Core;

public class AesEncryption
{
    public static string Encrypt(string plainText)
    {
        return AesAlgorithm.Encrypt(plainText, "!@#$%^^%$#@!", "!@#$%^", "MD5", 1, "XYZxyzAZSawsTRCE", 128);
    }

    public static string Encrypt(string plainText, HashAlgorithm algoritm = HashAlgorithm.MD5,
        EncryptKeySize keySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (algoritm)
        {
            case HashAlgorithm.MD5:
                KeyAlgoritm = "MD5";
                break;
            case HashAlgorithm.SHA1:
                KeyAlgoritm = "SHA1";
                break;
            case HashAlgorithm.SHA256:
                KeyAlgoritm = "SHA256";
                break;
        }

        return AesAlgorithm.Encrypt(plainText, "!@#$%^^%$#@!", "!@#$%^", KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)keySize);
    }

    public static string Encrypt(string plainText, string encryptKey,
        HashAlgorithm algoritm = HashAlgorithm.MD5,
        EncryptKeySize keySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (algoritm)
        {
            case HashAlgorithm.MD5:
                KeyAlgoritm = "MD5";
                break;
            case HashAlgorithm.SHA1:
                KeyAlgoritm = "SHA1";
                break;
            case HashAlgorithm.SHA256:
                KeyAlgoritm = "SHA256";
                break;
        }

        return AesAlgorithm.Encrypt(plainText, encryptKey, "!@#$%^", KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)keySize);
    }

    public static string Encrypt(string plainText, string encryptKey,
        string salt, HashAlgorithm algoritm = HashAlgorithm.MD5,
        EncryptKeySize keySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (algoritm)
        {
            case HashAlgorithm.MD5:
                KeyAlgoritm = "MD5";
                break;
            case HashAlgorithm.SHA1:
                KeyAlgoritm = "SHA1";
                break;
            case HashAlgorithm.SHA256:
                KeyAlgoritm = "SHA256";
                break;
        }

        return AesAlgorithm.Encrypt(plainText, encryptKey, salt, KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)keySize);
    }

    public static string Encrypt(string plainText, string encryptKey,
        string salt, string initVector, HashAlgorithm algoritm = HashAlgorithm.MD5,
        EncryptKeySize keySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (algoritm)
        {
            case HashAlgorithm.MD5:
                KeyAlgoritm = "MD5";
                break;
            case HashAlgorithm.SHA1:
                KeyAlgoritm = "SHA1";
                break;
            case HashAlgorithm.SHA256:
                KeyAlgoritm = "SHA256";
                break;
        }

        return AesAlgorithm.Encrypt(plainText, encryptKey, salt, KeyAlgoritm, 1, initVector, (int)keySize);
    }



    public static string Decrypt(string cipherText)
    {
        return AesAlgorithm.Decrypt(cipherText.Replace(' ', '+'), "!@#$%^^%$#@!", "!@#$%^", "MD5", 1, "XYZxyzAZSawsTRCE", 128);
    }

    public static string Decrypt(string cipherText, HashAlgorithm algoritm = HashAlgorithm.MD5,
        EncryptKeySize keySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (algoritm)
        {
            case HashAlgorithm.MD5:
                KeyAlgoritm = "MD5";
                break;
            case HashAlgorithm.SHA1:
                KeyAlgoritm = "SHA1";
                break;
            case HashAlgorithm.SHA256:
                KeyAlgoritm = "SHA256";
                break;
        }

        return AesAlgorithm.Decrypt(cipherText.Replace(' ', '+'), "!@#$%^^%$#@!", "!@#$%^", KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)keySize);
    }

    public static string Decrypt(string cipherText, string encryptKey,
        HashAlgorithm algoritm = HashAlgorithm.MD5,
        EncryptKeySize keySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (algoritm)
        {
            case HashAlgorithm.MD5:
                KeyAlgoritm = "MD5";
                break;
            case HashAlgorithm.SHA1:
                KeyAlgoritm = "SHA1";
                break;
            case HashAlgorithm.SHA256:
                KeyAlgoritm = "SHA256";
                break;
        }

        return AesAlgorithm.Decrypt(cipherText.Replace(' ', '+'), encryptKey, "!@#$%^", KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)keySize);
    }

    public static string Decrypt(string cipherText, string encryptKey,
        string salt, HashAlgorithm algoritm = HashAlgorithm.MD5,
        EncryptKeySize keySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (algoritm)
        {
            case HashAlgorithm.MD5:
                KeyAlgoritm = "MD5";
                break;
            case HashAlgorithm.SHA1:
                KeyAlgoritm = "SHA1";
                break;
            case HashAlgorithm.SHA256:
                KeyAlgoritm = "SHA256";
                break;
        }

        return AesAlgorithm.Decrypt(cipherText.Replace(' ', '+'), encryptKey, salt, KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)keySize);
    }

    public static string Decrypt(string cipherText, string encryptKey,
        string salt, string initVector, HashAlgorithm algoritm = HashAlgorithm.MD5,
        EncryptKeySize keySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (algoritm)
        {
           case HashAlgorithm.MD5:
                KeyAlgoritm = "MD5";
                break;
            case HashAlgorithm.SHA1:
                KeyAlgoritm = "SHA1";
                break;
            case HashAlgorithm.SHA256:
                KeyAlgoritm = "SHA256";
                break;
        }

        return AesAlgorithm.Decrypt(cipherText.Replace(' ', '+'), encryptKey, salt, KeyAlgoritm, 1, initVector, (int)keySize);
    }
}