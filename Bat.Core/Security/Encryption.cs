namespace Bat.Core
{
    public enum HashAlgoritm : byte
    {
        MD4 = 0,
        MD5 = 1,
        SHA1 = 2
    };

    public enum EncryptKeySize : int
    {
        KeySize128 = 128,
        KeySize256 = 256,
        KeySize512 = 512,
        KeySize1024 = 1024
    };


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
            try
            {
                return Rijndael.Encrypt(PlainText, "!@#$%^^%$#@!", "!@#$%^", "MD5", 1, "XYZxyzAZSawsTRCE", 128);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Encrypts specified PlainText as symmetric key algorithm
        /// and returns a Encrypted result.
        /// </summary>
        /// <param name="PlainText">A PlainText String To Encrypt</param>
        /// <param name="Algoritm">Set Key Hash Algoritm</param>
        /// <param name="KeySize">Set Key Size Of Encryption</param>
        /// <returns></returns>
        public static string Encrypt(string PlainText, HashAlgoritm Algoritm = HashAlgoritm.MD5,
            EncryptKeySize KeySize = EncryptKeySize.KeySize128)
        {
            try
            {
                string KeyAlgoritm = "MD5";
                switch (Algoritm)
                {
                    case HashAlgoritm.MD4:
                        KeyAlgoritm = "MD4";
                        break;
                    case HashAlgoritm.MD5:
                        KeyAlgoritm = "MD5";
                        break;
                    case HashAlgoritm.SHA1:
                        KeyAlgoritm = "SHA1";
                        break;
                }
                return Rijndael.Encrypt(PlainText, "!@#$%^^%$#@!", "!@#$%^", KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)KeySize);
            }
            catch
            {
                return null;
            }
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
            HashAlgoritm Algoritm = HashAlgoritm.MD5,
            EncryptKeySize KeySize = EncryptKeySize.KeySize128)
        {
            try
            {
                string KeyAlgoritm = "MD5";
                switch (Algoritm)
                {
                    case HashAlgoritm.MD4:
                        KeyAlgoritm = "MD4";
                        break;
                    case HashAlgoritm.MD5:
                        KeyAlgoritm = "MD5";
                        break;
                    case HashAlgoritm.SHA1:
                        KeyAlgoritm = "SHA1";
                        break;
                }
                return Rijndael.Encrypt(PlainText, EncryptKey, "!@#$%^", KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)KeySize);
            }
            catch
            {
                return null;
            }
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
            string Salt, HashAlgoritm Algoritm = HashAlgoritm.MD5,
            EncryptKeySize KeySize = EncryptKeySize.KeySize128)
        {
            try
            {
                string KeyAlgoritm = "MD5";
                switch (Algoritm)
                {
                    case HashAlgoritm.MD4:
                        KeyAlgoritm = "MD4";
                        break;
                    case HashAlgoritm.MD5:
                        KeyAlgoritm = "MD5";
                        break;
                    case HashAlgoritm.SHA1:
                        KeyAlgoritm = "SHA1";
                        break;
                }
                return Rijndael.Encrypt(PlainText, EncryptKey, Salt, KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)KeySize);
            }
            catch
            {
                return null;
            }
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
            string Salt, string InitVector, HashAlgoritm Algoritm = HashAlgoritm.MD5,
            EncryptKeySize KeySize = EncryptKeySize.KeySize128)
        {
            try
            {
                string KeyAlgoritm = "MD5";
                switch (Algoritm)
                {
                    case HashAlgoritm.MD4:
                        KeyAlgoritm = "MD4";
                        break;
                    case HashAlgoritm.MD5:
                        KeyAlgoritm = "MD5";
                        break;
                    case HashAlgoritm.SHA1:
                        KeyAlgoritm = "SHA1";
                        break;
                }
                return Rijndael.Encrypt(PlainText, EncryptKey, Salt, KeyAlgoritm, 1, InitVector, (int)KeySize);
            }
            catch
            {
                return null;
            }
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
            return Rijndael.Decrypt(CipherText.Replace(' ', '+'), "!@#$%^^%$#@!", "!@#$%^", "MD5", 1, "XYZxyzAZSawsTRCE", 128);
        }



        /// <summary>
        ///  Decrypts specified CipherText as symmetric key algorithm.
        ///  and returns a Decrypted result.
        /// </summary>
        /// <param name="CipherText">A CipherText As a Base 64-encoded String</param>
        /// <param name="Algoritm">Set Key Hash Algoritm</param>
        /// <param name="KeySize">Set Key Size Of Encryption</param>
        /// <returns>Decrypted string Value.</returns>
        public static string Decrypt(string CipherText, HashAlgoritm Algoritm = HashAlgoritm.MD5,
            EncryptKeySize KeySize = EncryptKeySize.KeySize128)
        {
            string KeyAlgoritm = "MD5";
            switch (Algoritm)
            {
                case HashAlgoritm.MD4:
                    KeyAlgoritm = "MD4";
                    break;
                case HashAlgoritm.MD5:
                    KeyAlgoritm = "MD5";
                    break;
                case HashAlgoritm.SHA1:
                    KeyAlgoritm = "SHA1";
                    break;
            }
            return Rijndael.Decrypt(CipherText.Replace(' ', '+'), "!@#$%^^%$#@!", "!@#$%^", KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)KeySize);
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
            HashAlgoritm Algoritm = HashAlgoritm.MD5,
            EncryptKeySize KeySize = EncryptKeySize.KeySize128)
        {
            try
            {
                string KeyAlgoritm = "MD5";
                switch (Algoritm)
                {
                    case HashAlgoritm.MD4:
                        KeyAlgoritm = "MD4";
                        break;
                    case HashAlgoritm.MD5:
                        KeyAlgoritm = "MD5";
                        break;
                    case HashAlgoritm.SHA1:
                        KeyAlgoritm = "SHA1";
                        break;
                }
                return Rijndael.Decrypt(CipherText.Replace(' ', '+'), EncryptKey, "!@#$%^", KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)KeySize);
            }
            catch
            {
                return null;
            }
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
            string Salt, HashAlgoritm Algoritm = HashAlgoritm.MD5,
            EncryptKeySize KeySize = EncryptKeySize.KeySize128)
        {
            try
            {
                string KeyAlgoritm = "MD5";
                switch (Algoritm)
                {
                    case HashAlgoritm.MD4:
                        KeyAlgoritm = "MD4";
                        break;
                    case HashAlgoritm.MD5:
                        KeyAlgoritm = "MD5";
                        break;
                    case HashAlgoritm.SHA1:
                        KeyAlgoritm = "SHA1";
                        break;
                }
                return Rijndael.Decrypt(CipherText.Replace(' ', '+'), EncryptKey, Salt, KeyAlgoritm, 1, "XYZxyzAZSawsTRCE", (int)KeySize);
            }
            catch
            {
                return null;
            }
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
            string Salt, string InitVector, HashAlgoritm Algoritm = HashAlgoritm.MD5,
            EncryptKeySize KeySize = EncryptKeySize.KeySize128)
        {
            try
            {
                string KeyAlgoritm = "MD5";
                switch (Algoritm)
                {
                    case HashAlgoritm.MD4:
                        KeyAlgoritm = "MD4";
                        break;
                    case HashAlgoritm.MD5:
                        KeyAlgoritm = "MD5";
                        break;
                    case HashAlgoritm.SHA1:
                        KeyAlgoritm = "SHA1";
                        break;
                }
                return Rijndael.Decrypt(CipherText.Replace(' ', '+'), EncryptKey, Salt, KeyAlgoritm, 1, InitVector, (int)KeySize);
            }
            catch
            {
                return null;
            }
        }

    }
}
