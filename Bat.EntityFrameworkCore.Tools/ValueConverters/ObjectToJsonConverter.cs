using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bat.EntityFrameworkCore.Tools;

public class ObjectToJsonConverter : ValueConverter<object, string>
{
    public ObjectToJsonConverter()
        : base(x => x.SerializeToJson(), x => x.DeSerializeJson())
    { }
}