using System;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;

namespace Bat.Core
{
    public static class SerializationExtension
    {
        /// <summary>
        /// اضافه کردن روت به فایل ایکس ام ال
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static string AddXmlRoot(this string nodeName, string xmlString) => "<" + nodeName + ">" + xmlString + "</" + nodeName + ">";

        /// <summary>
        /// تبدیل آبجکت ایکس ام ال به رشته
        /// </summary>
        /// <typeparam name="T">نوع آبجکت ایکس ام ال</typeparam>
        /// <param name="xmlObject">آبجکت ایکس ام ال</param>
        /// <returns></returns>
        public static string SerializeToXml<T>(this T xmlObject)
        {
            if (xmlObject == null) throw new ArgumentNullException("xmlObject");

            var serializer = new XmlSerializer(typeof(T));
            var outStream = new StringWriter();
            serializer.Serialize(outStream, xmlObject);
            return outStream.ToString();
        }

        /// <summary>
        /// تبدیل رشته به آبجکت ایکس ام ال
        /// </summary>
        /// <typeparam name="T">نوع آبجکت ایکس ام ال</typeparam>
        /// <param name="xml">رشته ایکس ام ال</param>
        /// <returns></returns>
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

        /// <summary>
        /// تبدیل رشته ایکس ام ال به استریم
        /// </summary>
        /// <param name="xmlString">رشته ایکس ام ال</param>
        /// <returns></returns>
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

        /// <summary>
        /// تبدیل رشته ایکس ام ال به نوع داده ای مورد نظر
        /// </summary>
        /// <typeparam name="T">نوع داده مورد نظر</typeparam>
        /// <param name="xmlString">رشته ایکس ام ال</param>
        /// <returns></returns>
        public static T ParseXml<T>(this string xmlString) where T : class
        {
            if (string.IsNullOrEmpty(xmlString)) throw new ArgumentNullException("xmlString");

            var reader = XmlReader.Create(xmlString.Trim().DeSerializeXml(), new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document });
            return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
        }



        /// <summary>
        /// اضافه کردن کاراکتر براکت به اول و آخر رشته
        /// </summary>
        /// <param name="JsonString">رشته ای از نوع جی سان</param>
        /// <returns></returns>
        public static string AddJsonArrayRoot(string JsonString) => "[" + JsonString + "]";

        /// <summary>
        /// تبدیل آبجکت جی سان به رشته
        /// </summary>
        /// <param name="jsonObject">آبجکت جی سان</param>
        /// <returns></returns>
        public static string SerializeToJson(this object jsonObject)
        {
            if (jsonObject == null) return string.Empty;
            return JsonConvert.SerializeObject(jsonObject);
        }

        /// <summary>
        /// تبدیل آبجکت جی سان به رشته
        /// </summary>
        /// <param name="jsonObject">آبجکت جی سان</param>
        /// <param name="depth">تعداد سلسله مراتب تو در تو</param>
        /// <returns></returns>
        public static string SerializeToJson(this object jsonObject, int depth)
        {
            if (jsonObject == null) return  string.Empty;

            var setting = new JsonSerializerSettings { MaxDepth = depth };
            return JsonConvert.SerializeObject(jsonObject, setting);
        }

        /// <summary>
        /// تبدیل آبجکت جی سان به رشته
        /// </summary>
        /// <param name="jsonObject">آبجکت جی سان</param>
        /// <param name="settings">تنظیمات جی سان</param>
        /// <returns></returns>
        public static string SerializeToJson(this object jsonObject, JsonSerializerSettings settings)
        {
            if (jsonObject == null) return string.Empty;

            return JsonConvert.SerializeObject(jsonObject, settings);
        }

        /// <summary>
        /// تبدیل رشته به آبجکت جی سان
        /// </summary>
        /// <typeparam name="T">نوع آبجکت جی سان</typeparam>
        /// <param name="json">آبجکت جی سان</param>
        /// <returns>دیسریالایز شده استرینگ</returns>
        public static T DeSerializeJson<T>(this string json)
        {
            if (string.IsNullOrEmpty(json)) return default;

            return JsonConvert.DeserializeObject<T>(json as string);
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

    }
}
