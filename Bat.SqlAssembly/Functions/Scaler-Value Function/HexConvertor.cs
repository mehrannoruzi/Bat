using System;
using System.Text;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace Bat.Sql
{
    public partial class HexConvertor
    {
        /// <summary>
        /// Convert string to hex 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [SqlFunction]
        public static SqlString ConvertStringToHex(string text)
        {
            byte[] bytes = Encoding.BigEndianUnicode.GetBytes(text);
            var hexString = BitConverter.ToString(bytes);
            //hexString = hexString.Replace("-", " ");

            return hexString.Replace("-", "").ToLower();
        }

        /// <summary>
        /// Convert hex string to orginal string 
        /// </summary>
        /// <param name="hexText"></param>
        /// <returns></returns>
        [SqlFunction]
        public static SqlString ConvertHexToString(string hexText)
        {
            var bytes = new byte[hexText.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexText.Substring(i * 2, 2), 16);
            }

            return Encoding.BigEndianUnicode.GetString(bytes);
        }
    }
}