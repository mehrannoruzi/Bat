namespace Bat.Core;

public class AesEncryption
{
    public static string Encrypt(string PlainText)
    {
        return AesAlgorithm.Encrypt(PlainText, "!@#$%^^%$#@!", "!@#$%^", "MD5", 1, "XYZxyzAZSawsTRCE", 128);
    }

    public static string Encrypt(string PlainText, HashAlgorithm Algoritm = HashAlgorithm.MD5,
        EncryptKeySize KeySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (Algoritm)
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

        return AesAlgorithm.Encrypt(PlainText, "!@#$%^^%$#@!", "!@#$%^", KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)KeySize);
    }

    public static string Encrypt(string PlainText, string EncryptKey,
        HashAlgorithm Algoritm = HashAlgorithm.MD5,
        EncryptKeySize KeySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (Algoritm)
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

        return AesAlgorithm.Encrypt(PlainText, EncryptKey, "!@#$%^", KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)KeySize);
    }

    public static string Encrypt(string PlainText, string EncryptKey,
        string Salt, HashAlgorithm Algoritm = HashAlgorithm.MD5,
        EncryptKeySize KeySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (Algoritm)
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

        return AesAlgorithm.Encrypt(PlainText, EncryptKey, Salt, KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)KeySize);
    }

    public static string Encrypt(string PlainText, string EncryptKey,
        string Salt, string InitVector, HashAlgorithm Algoritm = HashAlgorithm.MD5,
        EncryptKeySize KeySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (Algoritm)
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

        return AesAlgorithm.Encrypt(PlainText, EncryptKey, Salt, KeyAlgoritm, 1, InitVector, (int)KeySize);
    }



    public static string Decrypt(string CipherText)
    {
        return AesAlgorithm.Decrypt(CipherText.Replace(' ', '+'), "!@#$%^^%$#@!", "!@#$%^", "MD5", 1, "XYZxyzAZSawsTRCE", 128);
    }

    public static string Decrypt(string CipherText, HashAlgorithm Algoritm = HashAlgorithm.MD5,
        EncryptKeySize KeySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (Algoritm)
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

        return AesAlgorithm.Decrypt(CipherText.Replace(' ', '+'), "!@#$%^^%$#@!", "!@#$%^", KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)KeySize);
    }

    public static string Decrypt(string CipherText, string EncryptKey,
        HashAlgorithm Algoritm = HashAlgorithm.MD5,
        EncryptKeySize KeySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (Algoritm)
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

        return AesAlgorithm.Decrypt(CipherText.Replace(' ', '+'), EncryptKey, "!@#$%^", KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)KeySize);
    }

    public static string Decrypt(string CipherText, string EncryptKey,
        string Salt, HashAlgorithm Algoritm = HashAlgorithm.MD5,
        EncryptKeySize KeySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (Algoritm)
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

        return AesAlgorithm.Decrypt(CipherText.Replace(' ', '+'), EncryptKey, Salt, KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)KeySize);
    }

    public static string Decrypt(string CipherText, string EncryptKey,
        string Salt, string InitVector, HashAlgorithm Algoritm = HashAlgorithm.MD5,
        EncryptKeySize KeySize = EncryptKeySize.KeySize128)
    {
        string KeyAlgoritm = "MD5";
        switch (Algoritm)
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

        return AesAlgorithm.Decrypt(CipherText.Replace(' ', '+'), EncryptKey, Salt, KeyAlgoritm, 1, InitVector, (int)KeySize);
    }
}