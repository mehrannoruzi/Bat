using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text.RegularExpressions;

namespace Bat.Sql
{
    public partial class NumberFunction
    {
        private static readonly Regex DetectNumberRegex = new Regex(@"^-*[0-9,\.]+$");

        private static bool IsNumber(string number)
        {
            return DetectNumberRegex.IsMatch(number);
        }

        private static string GetStepString(long step)
        {
            switch (step)
            {
                case 1000000000000:
                    return " ÊÑ?á?ÇÑÏ æ ";
                case 1000000000:
                    return " ã?á?ÇÑÏ æ ";
                case 1000000:
                    return " ã?á?æä æ ";
                case 1000:
                    return " åÒÇÑ æ ";

                default:
                    return string.Empty;
            }
        }

        private static string GetNumberString(long number)
        {
            long mod, step = 0;
            var result = string.Empty;

            if ((number / 1000000000000) >= 1)
                step = 1000000000000;
            else if ((number / 1000000000) >= 1)
                step = 1000000000;
            else if ((number / 1000000) >= 1)
                step = 1000000;
            else if ((number / 1000) >= 1)
                step = 1000;
            else
                return number.ToString();

            for (long i = step; i >= 1; i /= 1000)
            {
                if (number == 0) continue;
                mod = number % i;
                number = number / i;
                result += number + GetStepString(i);
                number = mod;
            }

            return result;
        }

        private static string GetSpecialNumberString(long number)
        {
            if (number < 1000000)
                return number.ToString("##,###") + " Ñ?Çá ";


            var result = number.ToString();
            if (result.EndsWith("000000000000"))
            {
                return (number / 1000000000000).ToString("##,###") + " ÊÑ?á?ÇÑÏ Ñ?Çá ";
            }
            else if (result.EndsWith("000000000"))
            {
                return (number / 1000000000).ToString("##,###") + " ã?á?ÇÑÏ Ñ?Çá ";
            }
            else if (result.EndsWith("000000"))
            {
                return (number / 1000000).ToString("##,###") + " ã?á?æä Ñ?Çá ";
            }
            else
                return number.ToString("##,###") + " Ñ?Çá ";
        }

        /// <summary>
        /// ãÊÏ ÊÈÏ?á ÚÏÏ Èå ÍÑæİ İÇÑÓ?
        /// </summary>
        /// <param name="number">ÚÏÏ æÑæÏ?</param>
        /// <param name="level">ÓØÍ ÊÈÏ?á </param>
        /// <returns>ÎÑæÌ? ÍÑæİ ÊÈÏ?á ÔÏå</returns>
        private static SqlString PrivateToText(long number, int level)
        {
            var numstr = new string[][]
            {
                new string[]{"0","1","2","3","4","5","6","7","8","9"},
                new string[]{"10","11","12","13","14","15","16","17","18","19","20","30","40","50","60","70","80","90"},
                new string[]{"","100","200","300","400","500","600","700","800","900"} ,
                new string[]{"åÒÇÑ"},
                new string[]{"ãíáíæä"},
                new string[]{"ãíáíÇÑÏ"},
                new string[]{"ÊÑíáíæä"},
                new string[]{"ÊÑíáíÇÑÏ"}
            };

            switch (level)
            {
                case 1:
                    if (number < 10)
                        return numstr[0][System.Convert.ToInt32(number)];

                    if (number % 100 < 20 && number % 100 > 9)
                    {
                        if (number < 20)
                            return numstr[1][System.Convert.ToInt32(number % 10)];
                        return PrivateToText(number / 100, 3) + " æ " + numstr[1][System.Convert.ToInt32(number % 10)];
                    }
                    return PrivateToText(number / 10, 2) + ((number % 10 != 0) ? " æ " + numstr[0][System.Convert.ToInt32(number % 10)] : "");

                case 2:
                    if (number < 10)
                        return numstr[1][System.Convert.ToInt32(number % 10 + 8)];

                    return PrivateToText(number / 10, 3) + ((number % 10 >= 2) ? " æ " + numstr[1][System.Convert.ToInt32(number % 10 + 8)] : "");
                case 3:
                    if (number < 10)
                        return numstr[2][System.Convert.ToInt32(number % 10)];

                    return PrivateToText(number / 10, 4) + ((number % 10 != 0) ? " æ " : "") + numstr[2][System.Convert.ToInt32(number % 10)];
                case 4:
                    if (number < 1000)
                        return PrivateToText(number, 1) + " " + numstr[3][0];

                    return PrivateToText(number / 1000, 5) + ((System.Convert.ToInt32(number % 1000) != 0) ? " æ " + PrivateToText(number % 1000, 1) + " " + numstr[3][0] : "");
                case 5:
                    if (number < 1000)
                        return PrivateToText(number, 1) + " " + numstr[4][0];
                    return PrivateToText(number / 1000, 6) + ((System.Convert.ToInt32(number % 1000) != 0) ? " æ " + PrivateToText(number % 1000, 1) + " " + numstr[4][0] : "");
                case 6:
                    if (number < 1000)
                        return PrivateToText(number, 1) + " " + numstr[5][0];
                    return PrivateToText(number / 1000, 7) + ((System.Convert.ToInt32(number % 1000) != 0) ? " æ " + PrivateToText(number % 1000, 1) + " " + numstr[5][0] : "");
                case 7:
                    if (number < 1000)
                        return PrivateToText(number, 1) + " " + numstr[6][0];
                    return PrivateToText(number / 1000, 8) + ((System.Convert.ToInt32(number % 1000) != 0) ? " æ " + PrivateToText(number % 1000, 1) + " " + numstr[6][0] : "");
                case 8:
                    if (number < 1000)
                        return PrivateToText(number, 1) + " " + numstr[7][0];
                    return "";
                default:
                    return "";
            }
        }



        /// <summary>
        /// ÈÑÏÇÔÊä ˜ÇÑÇ˜ÊÑ åÇ? . æ , ÇÒ Ñæ? ÚÏÏ
        /// </summary>
        /// <param name="number">ÚÏÏ ãæÑÏ äÙÑ</param>
        /// <returns></returns>
        [SqlFunction]
        public static SqlString GetPlainNumber(string number)
        {
            if (!IsNumber(number)) return number;

            var result = number;
            if (result.Contains(".")) result = result.Substring(0, result.IndexOf('.'));
            if (result.Contains(",")) result = result.Replace(",", "");
            return result;
        }

        /// <summary>
        /// ÊÈÏ?á ÚÏÏ Èå ÚÏÏ? ˜å 3˜ÇÑÇ˜ÊÑ 3˜ÇÑÇ˜ÊÑ ÌÏÇ ÔÏå ÇÓÊ
        /// </summary>
        /// <param name="number">ÚÏÏ ãæÑÏ äÙÑ</param>
        /// <returns></returns>
        [SqlFunction]
        public static SqlString Get3DigitSplitNumber(long number, char symbol = ',')
        {
            if (number == 0) return "0";
            else if (number < 1000 && number > -1000) return number.ToString();
            else return string.Format("{0:N0}", long.Parse(number.ToString().Replace(",", "")));
        }

        /// <summary>
        /// ÊÈÏ?á ÚÏÏ Èå ÍÑæİ ÈÇ İÑãÊ ÎÇÕ
        /// </summary>
        /// <param name="number">ÚÏÏ ãæÑÏ äÙÑ</param>
        /// <returns></returns>
        [SqlFunction]
        public static SqlString NormalizeNumber(long number)
        {
            try
            {
                if (!IsNumber(number.ToString())) return number.ToString();
                if (number.ToString().StartsWith("0")) return number.ToString();

                var result = GetPlainNumber(number.ToString());
                return GetSpecialNumberString(Int64.Parse(result.ToString()));

                #region Old Code
                //result = GetNumberString(Int64.Parse(result));
                //result = result.Substring(result.Length - 3).Contains("æ")
                //    ? result.Substring(0, result.Length - 3)
                //    : result;
                //return result + " Ñ?Çá ";



                //if (result.Contains("000000000"))
                //{
                //    if ((Int64.Parse(result) / 1000000000) > 1000)
                //        result = (Int64.Parse(result)).ToString("##,###") + " Ñ?Çá ";
                //    else
                //        result = (Int64.Parse(result) / 1000000000).ToString("##,###") + " ã?á?ÇÑÏ Ñ?Çá ";
                //}
                //else if (result.Contains("000000"))
                //{
                //    if ((Int64.Parse(result) / 1000000) > 1000)
                //        result = (Int64.Parse(result)).ToString("##,###") + " Ñ?Çá ";
                //    else
                //        result = (Int64.Parse(result) / 1000000).ToString("##,###") + " ã?á?æä Ñ?Çá ";
                //}
                //else if (result.Contains("000"))
                //{
                //    if ((Int64.Parse(result) / 1000) > 1000)
                //        result = (Int64.Parse(result)).ToString("##,###") + " Ñ?Çá ";
                //    else
                //        result = (Int64.Parse(result) / 1000).ToString("##,###") + " åÒÇÑ Ñ?Çá ";
                //}
                //else
                //    result = Int64.Parse(result).ToString("##,###") + " Ñ?Çá "; 
                #endregion
            }
            catch
            {
                return number.ToString();
            }
        }

        /// <summary>
        /// ÊÈÏ?á ÚÏÏ Èå ÍÑæİ
        /// </summary>
        /// <param name="number">ÚÏÏ ãæÑÏ äÙÑ</param>
        /// <returns></returns>
        [SqlFunction]
        public static SqlString ToText(long number)
        {
            return PrivateToText(number, 1);
        }

        [SqlFunction]
        public static SqlString ToTextWithLevel(long number, int level)
        {
            return PrivateToText(number, level);
        }

    }
}
