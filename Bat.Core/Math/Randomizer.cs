using System;
using System.Linq;
using System.Security.Cryptography;

namespace Bat.Core
{
    public static class Randomizer
    {
        public static string GetRandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(
                 Enumerable.Repeat(chars, length)
                           .Select(s => s[random.Next(length)])
                           .ToArray());
        }

        public static string GetRandomString(int length, string pattern)
        {
            var random = new Random();
            return new string(
                 Enumerable.Repeat(pattern, length)
                           .Select(s => s[random.Next(length)])
                           .ToArray());
        }

        public static int GetRandomInteger(int length)
        {
            var chars = "123456789";
            var random = new Random();
            return int.Parse(new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(length)]).ToArray()));
        }

        public static string GetUniqueKey(int length = 5)
        {
            string guidResult = string.Empty;
            while (guidResult.Length < length)
                guidResult += Guid.NewGuid().ToString().GetHashCode().ToString("x");

            return guidResult.Substring(0, length);
        }

        public static string GetUniqueKey2(int length = 5)
        {
            var randomNumber = new byte[length];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}