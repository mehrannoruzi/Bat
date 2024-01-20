using System.Xml;
using System.Text.Json;
using System.Xml.Serialization;

namespace Bat.Core;

public static class SerializationExtension
{
    public static string AddXmlRoot(this string nodeName, string xmlString) => "<" + nodeName + ">" + xmlString + "</" + nodeName + ">";

    public static string SerializeToXml<T>(this T xmlObject)
    {
        if (xmlObject == null) throw new ArgumentNullException(nameof(xmlObject));

        var serializer = new XmlSerializer(typeof(T));
        var outStream = new StringWriter();
        serializer.Serialize(outStream, xmlObject);
        return outStream.ToString();
    }

    public static T DeSerializeXml<T>(this string xml)
    {
        if (string.IsNullOrEmpty(xml)) throw new ArgumentNullException(nameof(xml));

        T returnedXmlClass = default;
        try
        {
            using TextReader reader = new StringReader(xml);
            returnedXmlClass = (T)new XmlSerializer(typeof(T)).Deserialize(reader);
        }
        catch (Exception ex)
        {
            throw new Exception("String passed is not XML or can't to Deserialize to your passed object.", ex);
        }

        return returnedXmlClass;
    }

    public static Stream DeSerializeXml(this string xmlString)
    {
        if (string.IsNullOrEmpty(xmlString)) throw new ArgumentNullException(nameof(xmlString));

        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(@xmlString);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }

    public static T ParseXml<T>(this string xmlString) where T : class
    {
        if (string.IsNullOrEmpty(xmlString)) throw new ArgumentNullException(nameof(xmlString));

        var reader = XmlReader.Create(xmlString.Trim().DeSerializeXml(), new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document });
        return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
    }



    public static string AddJsonArrayRoot(string JsonString) => "[" + JsonString + "]";

    public static string SerializeToJson(this object jsonObject)
    {
        if (jsonObject == null) return string.Empty;

        return JsonSerializer.Serialize(jsonObject);
    }

    public static string SerializeToJson<T>(this T jsonObject)
    {
        if (jsonObject == null) return string.Empty;

        return JsonSerializer.Serialize(jsonObject);
    }

    public static string SerializeToJson(this object jsonObject, int depth)
    {
        if (jsonObject == null) return string.Empty;

        var options = new JsonSerializerOptions { MaxDepth = depth };
        return JsonSerializer.Serialize(jsonObject, options);
    }

    public static string SerializeToJson<T>(this T jsonObject, int depth)
    {
        if (jsonObject == null) return string.Empty;

        var options = new JsonSerializerOptions { MaxDepth = depth };
        return JsonSerializer.Serialize(jsonObject, options);
    }

    public static string SerializeToJson(this object jsonObject, JsonSerializerOptions options)
    {
        if (jsonObject == null) return string.Empty;
        
        return JsonSerializer.Serialize(jsonObject, options);
    }

    public static string SerializeToJson<T>(this T jsonObject, JsonSerializerOptions options)
    {
        if (jsonObject == null) return string.Empty;

        return JsonSerializer.Serialize(jsonObject, options);
    }

    public static T DeSerializeJson<T>(this string json)
    {
        if (string.IsNullOrWhiteSpace(json)) return default;

        return JsonSerializer.Deserialize<T>(json);
    }

    public static T DeSerializeJson<T>(this string json, int depth)
    {
        if (string.IsNullOrWhiteSpace(json)) return default;

        var options = new JsonSerializerOptions { MaxDepth = depth };
        return JsonSerializer.Deserialize<T>(json, options);
    }

    public static T DeSerializeJson<T>(this string json, JsonSerializerOptions options)
    {
        if (string.IsNullOrWhiteSpace(json)) return default;

        return JsonSerializer.Deserialize<T>(json, options);
    }

    public static dynamic DeSerializeJsonToDynamic(this string json)
    {
        if (string.IsNullOrWhiteSpace(json)) return null;

        return JsonSerializer.Deserialize<dynamic>(json);
    }

    public static dynamic DeSerializeJsonToDynamic(this string json, int depth)
    {
        if (string.IsNullOrWhiteSpace(json)) return default;

        var options = new JsonSerializerOptions { MaxDepth = depth };
        return JsonSerializer.Deserialize<dynamic>(json, options);
    }

    public static dynamic DeSerializeJsonToDynamic(this string json, JsonSerializerOptions options)
    {
        if (string.IsNullOrWhiteSpace(json)) return string.Empty;

        return JsonSerializer.Deserialize<dynamic>(json, options);
    }

    public static JsonElement DeSerializeJson(this string json)
    {
        if (string.IsNullOrWhiteSpace(json)) return default;

        return JsonSerializer.Deserialize<JsonElement>(json);
    }

    public static JsonElement DeSerializeJsonToJsonElement(this string json, int depth)
    {
        if (string.IsNullOrWhiteSpace(json)) return default;

        var options = new JsonSerializerOptions { MaxDepth = depth };
        return JsonSerializer.Deserialize<JsonElement>(json, options);
    }

    public static JsonElement DeSerializeJsonToJsonElement(this string json, JsonSerializerOptions options)
    {
        if (string.IsNullOrWhiteSpace(json)) return default;

        return JsonSerializer.Deserialize<JsonElement>(json, options);
    }

}