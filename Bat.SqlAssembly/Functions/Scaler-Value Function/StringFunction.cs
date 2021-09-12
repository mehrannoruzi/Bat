using System;
using System.Text;
using System.Linq;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text.RegularExpressions;

namespace Bat.Sql
{
    public partial class StringFunction
    {
        [SqlFunction]
        public static SqlBoolean IsNullOrEmpty(string text)
        {
            var isNothing = string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);
            return isNothing;
        }

        [SqlFunction]
        public static SqlString RemoveHtml(string text)
        {
            var result = Regex.Replace(text, "<.*?>", string.Empty, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return result;
        }

        [SqlFunction]
        public static SqlString ReplaceAll(string text, string regex, string replacement, int regexOptions)
        {
            return Regex.Replace(text, regex, replacement, (RegexOptions)regexOptions);
        }

        [SqlFunction]
        public static SqlString ToBinary(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            var result = string.Join("", bytes.Select(@byte => Convert.ToString(@byte, 2).PadLeft(8, '0')));
            return result;
        }

        /// <summary>
        /// Substring value from 0 character to lenght parameter
        /// </summary>
        /// <param name="value">string value</param>
        /// <param name="lenght">cut string lenght</param>
        /// <param name="appendString">append string to end of substring value</param>
        /// <returns>cuted string</returns>
        [SqlFunction]
        public static SqlString CustomSubstring(string value, int lenght, string defaultValue = "", string appendString = "...")
        {
            if (string.IsNullOrEmpty(value)) return defaultValue;
            if (lenght >= value.Length) return value;

            return (value.Substring(0, lenght) + appendString);
        }

        [SqlFunction]
        public static SqlString CustomMask(string input, byte maskMode, char maskWith, int maskLength, int maxMaskLength)
        {
            if (string.IsNullOrEmpty(input) || input.Length < maskLength || maskLength <= 0) return input;
            if (maxMaskLength == 0)
            {
                switch (maskMode)
                {
                    case 1:
                        return new string(maskWith, maskLength) + input.Substring(maskLength, input.Length - maskLength);

                    case 2:
                        var inputStep = input.Length / 2;
                        var maskStep = maskLength / 2;
                        return input.Substring(0, inputStep - maskStep) + new string(maskWith, maskLength) + input.Substring(inputStep + maskStep + 1, input.Length - (inputStep + maskStep + 1));

                    case 3:
                        return input.Substring(0, input.Length - maskLength) + new string(maskWith, maskLength);
                }
            }
            else
            {
                switch (maskMode)
                {
                    case 1:
                        return new string(maskWith, maxMaskLength) + input.Substring(maskLength, input.Length - maskLength);

                    case 2:
                        var inputStep = input.Length / 2;
                        var maskStep = maskLength / 2;
                        return input.Substring(0, inputStep - maskStep) + new string(maskWith, maxMaskLength) + input.Substring(inputStep + maskStep + 1, input.Length - (inputStep + maskStep + 1));

                    case 3:
                        return input.Substring(0, input.Length - maskLength) + new string(maskWith, maxMaskLength);
                }
            }

            return input;
        }

    }
}
