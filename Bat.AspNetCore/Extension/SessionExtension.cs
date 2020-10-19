using Bat.Core;
using Microsoft.AspNetCore.Http;

namespace Bat.AspNetCore
{
    public static class SessionExtension
    {
        public static bool Set(this ISession session, string key, string value)
        {
            try
            {
                session.SetString(key, value);
                return true;
            }
            catch
            { return false; }
        }

        public static bool Set(this ISession session, string key, object value)
        {
            try
            {
                session.SetString(key, value.SerializeToJson());
                return true;
            }
            catch
            { return false; }
        }

        public static string Get(this ISession session, string key)
        {
            return session.GetString(key);
        }

        public static T Get<T>(this ISession session, string key) where T : class
        {
            return session.GetString(key).DeSerializeJson<T>();
        }
    }
}