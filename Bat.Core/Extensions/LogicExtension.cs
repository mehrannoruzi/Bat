using System;
using System.Linq;
using System.Collections.Generic;

namespace Bat.Core
{
    public static class LogicExtension
    {
        public static bool IsNull(this string str) => string.IsNullOrWhiteSpace(str);

        public static bool IsNotNull(this string str) => !string.IsNullOrWhiteSpace(str);

        public static bool IsNull(this object obj) => obj == null;

        public static bool IsNotNull(this object obj) => obj != null;

        public static bool IsNull(this Guid? token) => token == null || token == Guid.Empty;

        public static bool IsNotNull(this Guid token) => !IsNull(token);

        public static bool CanBeCastTo<T>(this string value)
        {
            var type = typeof(T);
            if (!type.IsEnum) return false;

            var canBeCast = Enum.GetNames(type).Contains(value);
            if (!canBeCast) return false;

            return true;
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items) action(item);
        }

        public async static void ForEach<T>(this IAsyncEnumerable<T> items, Action<T> action)
        {
            await foreach (var item in items) action(item);
        }

        public static void ForEach<T>(this List<T> items, Action<T> action)
        {
            foreach (var item in items) action(item);
        }
    }
}