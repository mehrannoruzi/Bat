using System;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Serialization;

namespace Bat.Core
{
    public static class SerializationExtension
    {
        public static string AddXmlRoot(this string nodeName, string xmlString) => "<" + nodeName + ">" + xmlString + "</" + nodeName + ">";

        public static string SerializeToXml<T>(this T xmlObject)
        {
            if (xmlObject == null) throw new ArgumentNullException("xmlObject");

            var serializer = new XmlSerializer(typeof(T));
            var outStream = new StringWriter();
            serializer.Serialize(outStream, xmlObject);
            return outStream.ToString();
        }

        public static T DeSerializeXml<T>(this string xml)
        {
            if (string.IsNullOrEmpty(xml)) throw new ArgumentNullException("xml");

            T returnedXmlClass = default(T);
            try
            {
                using (TextReader reader = new StringReader(xml))
                {
                    returnedXmlClass = (T)new XmlSerializer(typeof(T)).Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("String passed is not XML or can't to Deserialize to your passed object.", ex);
            }

            return returnedXmlClass;
        }

        public static Stream DeSerializeXml(this string xmlString)
        {
            if (string.IsNullOrEmpty(xmlString)) throw new ArgumentNullException("xmlString");

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(@xmlString);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static T ParseXml<T>(this string xmlString) where T : class
        {
            if (string.IsNullOrEmpty(xmlString)) throw new ArgumentNullException("xmlString");

            var reader = XmlReader.Create(xmlString.Trim().DeSerializeXml(), new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document });
            return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
        }



        public static string AddJsonArrayRoot(string JsonString) => "[" + JsonString + "]";

        public static string SerializeToJson(this object jsonObject)
        {
            if (jsonObject == null) return string.Empty;
            return JsonConvert.SerializeObject(jsonObject);
        }

        public static string SerializeToJson(this object jsonObject, int depth)
        {
            if (jsonObject == null) return string.Empty;

            var setting = new JsonSerializerSettings { MaxDepth = depth };
            return JsonConvert.SerializeObject(jsonObject, setting);
        }

        public static string SerializeToJson(this object jsonObject, JsonSerializerSettings settings)
        {
            if (jsonObject == null) return string.Empty;

            return JsonConvert.SerializeObject(jsonObject, settings);
        }

        public static T DeSerializeJson<T>(this string json)
        {
            if (string.IsNullOrEmpty(json)) return default;

            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T DeSerializeJson<T>(this string json, int depth)
        {
            if (string.IsNullOrEmpty(json)) throw new ArgumentNullException("json");

            var setting = new JsonSerializerSettings { MaxDepth = depth };
            return JsonConvert.DeserializeObject<T>(json as string, setting);
        }

        public static T DeSerializeJson<T>(this string json, JsonSerializerSettings settings)
        {
            if (string.IsNullOrEmpty(json)) throw new ArgumentNullException("json");

            return JsonConvert.DeserializeObject<T>(json as string, settings);
        }

        public static dynamic DeSerializeJson(this string json)
        {
            if (string.IsNullOrEmpty(json)) return null;

            return JToken.Parse(json);
        }

        public static dynamic DeSerializeJson(this string json, JsonLoadSettings settings)
        {
            if (string.IsNullOrEmpty(json)) return string.Empty;

            return JToken.Parse(json, settings);
        }

    }
}