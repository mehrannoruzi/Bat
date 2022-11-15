using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bat.EntityFrameworkCore.Tools;

public class GuidEncryptorConverter : ValueConverter<Guid, string>
{
    public GuidEncryptorConverter()
        : base(x => AesEncryption.Encrypt(x.ToString()), x => Guid.Parse(AesEncryption.Decrypt(x)))
    { }
}