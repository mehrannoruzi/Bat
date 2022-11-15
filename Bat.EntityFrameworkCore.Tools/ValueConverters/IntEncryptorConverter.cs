using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bat.EntityFrameworkCore.Tools;

public class IntEncryptorConverter : ValueConverter<int, string>
{
    public IntEncryptorConverter()
        : base(x => AesEncryption.Encrypt(x.ToString()), x => int.Parse(AesEncryption.Decrypt(x)))
    { }
}