using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Bat.Core
{
    public enum MaskMode
    {
        Start = 1,
        Middle = 2,
        End = 3
    }

    public class MaskOption
    {
        public MaskMode Mode { get; set; }
        public char MaskWith { get; set; }
        public int MaskLength { get; set; }
        public int MaxMaskLength { get; set; }
    }

    public static class StringExtensions
    {
        public static int ToInt(this string text)
        {
            int.TryParse(text, out int result);
            return result;
        }

        public static string ToBinary(this string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            var result = string.Join("", bytes.Select(@byte => Convert.ToString(@byte, 2).PadLeft(8, '0')));
            return result;
        }

        public static byte[] ToBytes(this string text) => Encoding.UTF8.GetBytes(text);

        public static string ToPersianAlphaNumeric(this string text) => Regex.Replace(text, "[^A-Za-z0-9]", string.Empty);

        public static string ToUTF8Number(this string text, string codePage = "iso-8859-1")
        {
            var unicodeString = Encoding.UTF8.GetString(Encoding.GetEncoding(codePage).GetBytes(text));
            var myString = unicodeString.ToArray();
            var validNumber = new StringBuilder().Clear();
            for (int i = 0; i < myString.Length; i++)
            {
                if (myString[i] >= 1776 && myString[i] <= 1785)
                    validNumber.Append((char)((myString[i] - 1776) + 48));
                else if (myString[i] >= 1632 && myString[i] <= 1641)
                    validNumber.Append((char)((myString[i] - 1632) + 48));
                else
                    validNumber.Append(myString[i]);
            }

            return validNumber.ToString();
        }



        public static string Fill(this string input, params string[] values) => string.Format(input, values);

        public static string CustomMask(this string input, MaskOption option)
        {
            if (string.IsNullOrEmpty(input) || input.Length < option.MaskLength || option.MaskLength <= 0) return input;
            if (option.MaxMaskLength == 0)
            {
                switch (option.Mode)
                {
                    case MaskMode.Start:
                        return new string(option.MaskWith, option.MaskLength) + input.Substring(option.MaskLength, input.Length - option.MaskLength);

                    case MaskMode.Middle:
                        var inputStep = input.Length / 2;
                        var maskStep = option.MaskLength / 2;
                        return input.Substring(0, inputStep - maskStep) + new string(option.MaskWith, option.MaskLength) + input.Substring(inputStep + maskStep + 1, input.Length - (inputStep + maskStep + 1));

                    case MaskMode.End:
                        return input.Substring(0, input.Length - option.MaskLength) + new string(option.MaskWith, option.MaskLength);
                }
            }
            else
            {
                switch (option.Mode)
                {
                    case MaskMode.Start:
                        return new string(option.MaskWith, option.MaxMaskLength) + input.Substring(option.MaskLength, input.Length - option.MaskLength);

                    case MaskMode.Middle:
                        var inputStep = input.Length / 2;
                        var maskStep = option.MaskLength / 2;
                        return input.Substring(0, inputStep - maskStep) + new string(option.MaskWith, option.MaxMaskLength) + input.Substring(inputStep + maskStep + 1, input.Length - (inputStep + maskStep + 1));

                    case MaskMode.End:
                        return input.Substring(0, input.Length - option.MaskLength) + new string(option.MaskWith, option.MaxMaskLength);
                }
            }

            return input;
        }

        /// <summary>
        /// Substring value from 0 character to lenght parameter
        /// </summary>
        /// <param name="value">string value</param>
        /// <param name="lenght">cut string lenght</param>
        /// <param name="appendString">append string to end of substring value</param>
        /// <returns>cuted string</returns>
        public static string CustomSubstring(this string value, int lenght, string defaultValue = "", string appendString = "...")
        {
            if (string.IsNullOrEmpty(value)) return defaultValue;
            if (lenght >= value.Length) return value;

            return (value.Substring(0, lenght) + appendString);
        }



        public static List<T> SplitCsv<T>(this string text, char seperator)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;

            List<T> result = new List<T>();
            List<string> tokens = text.Split(seperator).Select(i => i.Trim()).ToList();
            tokens.ForEach(t =>
            {
                try
                {
                    result.Add((T)Convert.ChangeType(t, typeof(T)));
                }
                catch
                { }
            });
            return result;
        }

        public static string RemoveHtml(this string text) => Regex.Replace(text, "<.*?>", string.Empty, RegexOptions.Multiline | RegexOptions.IgnoreCase);

        public static string ReplaceAll(this string text, string regex, string replacement, RegexOptions regexOptions) => Regex.Replace(text, regex, replacement, regexOptions);

        public static string ReplaceAll(this string text, Dictionary<string, string> values)
        {
            foreach (var item in values)
                text = text.Replace(item.Key, item.Value);

            return text;
        }
    }
}
