﻿namespace Bat.Core;

/// <summary>
/// Sample :
/// String CipherText = Encryption.Encrypt(_SampleString);
/// String PlaneText = Encryption.Decrypt(_EncryptedString);
/// </summary>
public class Encryption
{
    /// <summary>
    /// Encrypts specified PlainText as symmetric key algorithm
    /// and returns a Encrypted result.
    /// </summary>
    /// <param name="PlainText">A PlainText String To Encrypt</param>
    /// <returns>Return String CipherText</returns>
    public static string Encrypt(string PlainText)
    {
        return RijndaelAlgorithm.Encrypt(PlainText, "!@#$%^^%$#@!", "!@#$%^", "MD5", 1, "XYZxyzAZSawsTRCE", 128);
    }

    /// <summary>
    /// Encrypts specified PlainText as symmetric key algorithm
    /// and returns a Encrypted result.
    /// </summary>
    /// <param name="PlainText">A PlainText String To Encrypt</param>
    /// <param name="Algoritm">Set Key Hash Algoritm</param>
    /// <param name="KeySize">Set Key Size Of Encryption</param>
    /// <returns></returns>
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

        return RijndaelAlgorithm.Encrypt(PlainText, "!@#$%^^%$#@!", "!@#$%^", KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)KeySize);
    }

    /// <summary>
    /// Encrypts specified PlainText as symmetric key algorithm
    /// and returns a Encrypted Value.
    /// </summary>
    /// <param name="PlainText">A PlainText String To Encrypt</param>
    /// <param name="EncryptKey">The Key Of Symmetric Encryption</param>
    /// <param name="Algoritm">Set Key Hash Algoritm</param>
    /// <param name="KeySize">Set Key Size Of Encryption</param>
    /// <returns>Encrypted Value Formatted As a Base 64-encoded String.</returns>
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

        return RijndaelAlgorithm.Encrypt(PlainText, EncryptKey, "!@#$%^", KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)KeySize);
    }

    /// <summary>
    /// /// Encrypts specified PlainText as symmetric key algorithm
    /// and returns a Encrypted result.
    /// </summary>
    /// <param name="PlainText">A PlainText String To Encrypt</param>
    /// <param name="EncryptKey">The Key Of Symmetric Encryption</param>
    /// <param name="Salt">A String To Combine With EncryptKey To Symmetric Encryption</param>
    /// <param name="Algoritm">Set Key Hash Algoritm</param>
    /// <param name="KeySize">Set Key Size Of Encryption</param>
    /// <returns>Encrypted Value Formatted As a Base 64-encoded String.</returns>
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

        return RijndaelAlgorithm.Encrypt(PlainText, EncryptKey, Salt, KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)KeySize);
    }

    /// <summary>
    /// Encrypts specified PlainText as symmetric key algorithm
    /// and returns a Encrypted result.
    /// </summary>
    /// <param name="PlainText">A PlainText String To Encrypt</param>
    /// <param name="EncryptKey">The Key Of Symmetric Encryption</param>
    /// <param name="Salt">A String To Combine With EncryptKey To Symmetric Encryption</param>
    /// <param name="InitVector">A String To Encrypt The First Block Of PlainText</param>
    /// <param name="Algoritm">Set Key Hash Algoritm</param>
    /// <param name="KeySize">Set Key Size Of Encryption</param>
    /// <returns>Encrypted Value Formatted As a Base 64-encoded String.</returns>
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

        return RijndaelAlgorithm.Encrypt(PlainText, EncryptKey, Salt, KeyAlgoritm, 1, InitVector, (int)KeySize);
    }



    /// <summary>
    ///  Decrypts specified CipherText as symmetric key algorithm.
    ///  and returns a Decrypted result.
    /// </summary>
    /// <param name="CipherText">A CipherText As a Base 64-encoded String</param>
    /// <param name="Algoritm">Set Key Hash Algoritm</param>
    /// <param name="KeySize">Set Key Size Of Encryption</param>
    /// <returns>Decrypted string Value.</returns>
    public static string Decrypt(string CipherText)
    {
        return RijndaelAlgorithm.Decrypt(CipherText.Replace(' ', '+'), "!@#$%^^%$#@!", "!@#$%^", "MD5", 1, "XYZxyzAZSawsTRCE", 128);
    }

    /// <summary>
    ///  Decrypts specified CipherText as symmetric key algorithm.
    ///  and returns a Decrypted result.
    /// </summary>
    /// <param name="CipherText">A CipherText As a Base 64-encoded String</param>
    /// <param name="Algoritm">Set Key Hash Algoritm</param>
    /// <param name="KeySize">Set Key Size Of Encryption</param>
    /// <returns>Decrypted string Value.</returns>
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

        return RijndaelAlgorithm.Decrypt(CipherText.Replace(' ', '+'), "!@#$%^^%$#@!", "!@#$%^", KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)KeySize);
    }

    /// <summary>
    ///  Decrypts specified CipherText as symmetric key algorithm.
    ///  and returns a Decrypted result.
    /// </summary>
    /// <param name="CipherText">A CipherText As a Base 64-encoded String</param>
    /// <param name="EncryptKey">The Key Of Symmetric Encryption</param>
    /// <param name="Algoritm">Set Key Hash Algoritm</param>
    /// <param name="KeySize">Set Key Size Of Encryption</param>
    /// <returns>Decrypted string Value.</returns>
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

        return RijndaelAlgorithm.Decrypt(CipherText.Replace(' ', '+'), EncryptKey, "!@#$%^", KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)KeySize);
    }

    /// <summary>
    ///  Decrypts specified CipherText as symmetric key algorithm.
    ///  and returns a Decrypted result.
    /// </summary>
    /// <param name="CipherText">A CipherText As a Base 64-encoded String</param>
    /// <param name="EncryptKey">The Key Of Symmetric Encryption</param>
    /// <param name="Salt">A String To Combine With EncryptKey To Symmetric Encryption</param>
    /// <param name="Algoritm">Set Key Hash Algoritm</param>
    /// <param name="KeySize">Set Key Size Of Encryption</param>
    /// <returns>Decrypted string Value.</returns>
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

        return RijndaelAlgorithm.Decrypt(CipherText.Replace(' ', '+'), EncryptKey, Salt, KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)KeySize);
    }

    /// <summary>
    ///  Decrypts specified CipherText as symmetric key algorithm.
    ///  and returns a Decrypted result.
    /// </summary>
    /// <param name="CipherText">A CipherText As a Base 64-encoded String</param>
    /// <param name="EncryptKey">The Key Of Symmetric Encryption</param>
    /// <param name="Salt">A String To Combine With EncryptKey To Symmetric Encryption</param>
    /// <param name="InitVector">A String To Encrypt The First Block Of PlainText</param>
    /// <param name="Algoritm">Set Key Hash Algoritm</param>
    /// <param name="KeySize">Set Key Size Of Encryption</param>
    /// <returns>Decrypted string Value.</returns>
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

        return RijndaelAlgorithm.Decrypt(CipherText.Replace(' ', '+'), EncryptKey, Salt, KeyAlgoritm, 1, InitVector, (int)KeySize);
    }
}