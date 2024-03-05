namespace Bat.Core;

public static class SerializationExtension
{
    public static JsonSerializerOptions GetDefaultOption(int? depth = null)
    {
        var options = new JsonSerializerOptions()
        {
            IncludeFields = true,
            //WriteIndented = true,
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };
        options.Converters.Add(new BatStringToBoolConverter());
        options.Converters.Add(new BatNumberToStringConverter());

        if (depth is not null) options.MaxDepth = (int)depth;

        return options;
    }


    public static string AddJsonArrayRoot(string JsonString)
        => "[" + JsonString + "]";

    public static string SerializeToJson(this object jsonObject)
    {
        if (jsonObject is null) return string.Empty;

        return JsonSerializer.Serialize(jsonObject, GetDefaultOption());
    }

    public static string SerializeToJson<T>(this T jsonObject)
    {
        if (jsonObject is null) return string.Empty;

        return JsonSerializer.Serialize(jsonObject, GetDefaultOption());
    }

    public static string SerializeToJson(this object jsonObject, int depth)
    {
        if (jsonObject is null) return string.Empty;

        return JsonSerializer.Serialize(jsonObject, GetDefaultOption(depth));
    }

    public static string SerializeToJson<T>(this T jsonObject, int depth)
    {
        if (jsonObject is null) return string.Empty;

        return JsonSerializer.Serialize(jsonObject, GetDefaultOption(depth));
    }

    public static string SerializeToJson(this object jsonObject, JsonSerializerOptions options)
    {
        if (jsonObject is null) return string.Empty;

        return JsonSerializer.Serialize(jsonObject, options);
    }

    public static string SerializeToJson<T>(this T jsonObject, JsonSerializerOptions options)
    {
        if (jsonObject is null) return string.Empty;

        return JsonSerializer.Serialize(jsonObject, options);
    }

    public static T DeSerializeJson<T>(this string json)
    {
        if (string.IsNullOrWhiteSpace(json)) return default;

        return JsonSerializer.Deserialize<T>(json, GetDefaultOption());
    }

    public static T DeSerializeJson<T>(this string json, int depth)
    {
        if (string.IsNullOrWhiteSpace(json)) return default;

        return JsonSerializer.Deserialize<T>(json, GetDefaultOption(depth));
    }

    public static T DeSerializeJson<T>(this string json, JsonSerializerOptions options)
    {
        if (string.IsNullOrWhiteSpace(json)) return default;

        return JsonSerializer.Deserialize<T>(json, options);
    }

    public static dynamic DeSerializeJsonToDynamic(this string json)
    {
        if (string.IsNullOrWhiteSpace(json)) return null;

        return JsonSerializer.Deserialize<dynamic>(json, GetDefaultOption());
    }

    public static dynamic DeSerializeJsonToDynamic(this string json, int depth)
    {
        if (string.IsNullOrWhiteSpace(json)) return default;

        return JsonSerializer.Deserialize<dynamic>(json, GetDefaultOption(depth));
    }

    public static dynamic DeSerializeJsonToDynamic(this string json, JsonSerializerOptions options)
    {
        if (string.IsNullOrWhiteSpace(json)) return string.Empty;

        return JsonSerializer.Deserialize<dynamic>(json, options);
    }

    public static JsonElement DeSerializeJson(this string json)
    {
        if (string.IsNullOrWhiteSpace(json)) return default;

        return JsonSerializer.Deserialize<JsonElement>(json, GetDefaultOption());
    }

    public static JsonElement DeSerializeJsonToJsonElement(this string json, int depth)
    {
        if (string.IsNullOrWhiteSpace(json)) return default;

        return JsonSerializer.Deserialize<JsonElement>(json, GetDefaultOption(depth));
    }

    public static JsonElement DeSerializeJsonToJsonElement(this string json, JsonSerializerOptions options)
    {
        if (string.IsNullOrWhiteSpace(json)) return default;

        return JsonSerializer.Deserialize<JsonElement>(json, options);
    }

}