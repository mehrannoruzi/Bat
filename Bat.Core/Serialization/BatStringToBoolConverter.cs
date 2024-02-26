namespace Bat.Core;

public class BatStringToBoolConverter : JsonConverter<object>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(bool) == typeToConvert;
    }

    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType is JsonTokenType.True)
            return true;

        if (reader.TokenType is JsonTokenType.False)
            return false;

        if (reader.TokenType is JsonTokenType.String)
            return bool.Parse(reader.GetString());

        using JsonDocument document = JsonDocument.ParseValue(ref reader);
        return document.RootElement.Clone().ToString();
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        writer.WriteBooleanValue(bool.Parse(value.ToString()));
    }
}