using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bat.EntityFrameworkCore.Tools;

public class StringEncryptorConverter : ValueConverter<string, string>
{
    public StringEncryptorConverter()
        : base(x => AesEncryption.Encrypt(x.ToString()), x => AesEncryption.Decrypt(x))
    { }
}