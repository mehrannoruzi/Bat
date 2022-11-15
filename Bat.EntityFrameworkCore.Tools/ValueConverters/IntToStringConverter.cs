using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bat.EntityFrameworkCore.Tools;

public class IntToStringConverter : ValueConverter<int, string>
{
    public IntToStringConverter()
        : base(x => x.ToString(), x => int.Parse(x))
    { }
}