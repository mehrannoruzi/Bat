namespace Bat.Core;

public class BatNumberToStringConverter : JsonConverter<string>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(string) == typeToConvert;
    }

    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType is JsonTokenType.Number)
        {
            return reader.TryGetInt64(out long l)
                ? l.ToString()
                : reader.GetDouble().ToString();
        }

        if (reader.TokenType is JsonTokenType.String)
            return reader.GetString();

        using JsonDocument document = JsonDocument.ParseValue(ref reader);
        return document.RootElement.Clone().ToString();
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}