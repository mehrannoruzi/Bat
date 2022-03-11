using System.Globalization;

namespace Bat.Core;

public static class NumberExtension
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
                return " تریلیارد و ";
            case 1000000000:
                return " میلیارد و ";
            case 1000000:
                return " میلیون و ";
            case 1000:
                return " هزار و ";

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

        #region Old Code
        //if ((number / 1000000000000) >= 1)
        //{
        //    #region Trilyard
        //    var step = 1000000000000;
        //    mod = number % step;
        //    number = number / step;
        //    result += number + GetStepString(step);
        //    result += mod == 0 ? string.Empty : GetNumberString(mod);
        //    #endregion
        //}
        //else if ((number / 1000000000) >= 1)
        //{
        //    #region Milyard
        //    var step = 1000000000;
        //    mod = number % step;
        //    number = number / step;
        //    result += number + GetStepString(step);
        //    result += mod == 0 ? string.Empty : GetNumberString(mod);
        //    #endregion
        //}
        //else if ((number / 1000000) >= 1)
        //{
        //    #region Miliun
        //    var step = 1000000;
        //    mod = number % step;
        //    number = number / step;
        //    result += number + GetStepString(step);
        //    result += mod == 0 ? string.Empty : GetNumberString(mod);
        //    #endregion
        //}
        //else if ((number / 1000) >= 1)
        //{
        //    #region Hezar
        //    var step = 1000;
        //    mod = number % step;
        //    number = number / step;
        //    result += number + GetStepString(step);
        //    result += mod == 0 ? string.Empty : GetNumberString(mod);
        //    #endregion
        //}
        //else
        //    result = number.ToString(); 
        #endregion
    }

    private static string GetSpecialNumberString(long number)
    {
        if (number < 1000000)
            return number.ToString("##,###") + " ریال ";


        var result = number.ToString();
        if (result.EndsWith("000000000000"))
        {
            return (number / 1000000000000).ToString("##,###") + " تریلیارد ریال ";
        }
        else if (result.EndsWith("000000000"))
        {
            return (number / 1000000000).ToString("##,###") + " میلیارد ریال ";
        }
        else if (result.EndsWith("000000"))
        {
            return (number / 1000000).ToString("##,###") + " میلیون ریال ";
        }
        else
            return number.ToString("##,###") + " ریال ";
    }

    /// <summary>
    /// متد تبدیل عدد به حروف فارسی
    /// </summary>
    /// <param name="number">عدد ورودی</param>
    /// <param name="level">سطح تبدیل </param>
    /// <returns>خروجی حروف تبدیل شده</returns>
    private static string ToText(long number, int level)
    {
        var numstr = new string[][]
        {
                new string[]{"0","1","2","3","4","5","6","7","8","9"},
                new string[]{"10","11","12","13","14","15","16","17","18","19","20","30","40","50","60","70","80","90"},
                new string[]{"","100","200","300","400","500","600","700","800","900"} ,
                new string[]{"هزار"},
                new string[]{"ميليون"},
                new string[]{"ميليارد"},
                new string[]{"تريليون"},
                new string[]{"تريليارد"}
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
                    return ToText(number / 100, 3) + " و " + numstr[1][System.Convert.ToInt32(number % 10)];
                }
                return ToText(number / 10, 2) + ((number % 10 != 0) ? " و " + numstr[0][System.Convert.ToInt32(number % 10)] : "");

            case 2:
                if (number < 10)
                    return numstr[1][System.Convert.ToInt32(number % 10 + 8)];

                return ToText(number / 10, 3) + ((number % 10 >= 2) ? " و " + numstr[1][System.Convert.ToInt32(number % 10 + 8)] : "");
            case 3:
                if (number < 10)
                    return numstr[2][System.Convert.ToInt32(number % 10)];

                return ToText(number / 10, 4) + ((number % 10 != 0) ? " و " : "") + numstr[2][System.Convert.ToInt32(number % 10)];
            case 4:
                if (number < 1000)
                    return ToText(number, 1) + " " + numstr[3][0];

                return ToText(number / 1000, 5) + ((System.Convert.ToInt32(number % 1000) != 0) ? " و " + ToText(number % 1000, 1) + " " + numstr[3][0] : "");
            case 5:
                if (number < 1000)
                    return ToText(number, 1) + " " + numstr[4][0];
                return ToText(number / 1000, 6) + ((System.Convert.ToInt32(number % 1000) != 0) ? " و " + ToText(number % 1000, 1) + " " + numstr[4][0] : "");
            case 6:
                if (number < 1000)
                    return ToText(number, 1) + " " + numstr[5][0];
                return ToText(number / 1000, 7) + ((System.Convert.ToInt32(number % 1000) != 0) ? " و " + ToText(number % 1000, 1) + " " + numstr[5][0] : "");
            case 7:
                if (number < 1000)
                    return ToText(number, 1) + " " + numstr[6][0];
                return ToText(number / 1000, 8) + ((System.Convert.ToInt32(number % 1000) != 0) ? " و " + ToText(number % 1000, 1) + " " + numstr[6][0] : "");
            case 8:
                if (number < 1000)
                    return ToText(number, 1) + " " + numstr[7][0];
                return "";
            default:
                return "";
        }
    }


    /// <summary>
    /// برداشتن کاراکتر های . و , از روی عدد
    /// </summary>
    /// <param name="number">عدد مورد نظر</param>
    /// <returns></returns>
    public static string ToPlainNumber(this string number)
    {
        if (!IsNumber(number)) return number;

        var result = number;
        if (result.Contains(".")) result = result.Substring(0, result.IndexOf('.'));
        if (result.Contains(",")) result = result.Replace(",", "");
        return result;
    }

    /// <summary>
    /// تبدیل عدد به عددی که 3کاراکتر 3کاراکتر جدا شده است
    /// </summary>
    /// <param name="number">عدد مورد نظر</param>
    /// <returns></returns>
    public static string To3DigitSplited(this long number, char symbol = ',')
    {
        if (number == 0) return "0";
        else if (number < 1000 && number > -1000) return number.ToString();
        else return string.Format("{0:N0}", long.Parse(number.ToString().Replace(",", "")));
    }

    /// <summary>
    /// تبدیل عدد به عددی که 3کاراکتر 3کاراکتر جدا شده است
    /// </summary>
    /// <param name="number">عدد مورد نظر</param>
    /// <returns></returns>
    public static string To3DigitSplited(this int number)
    {
        if (number == 0) return "0";
        else if (number < 1000 && number > -1000) return number.ToString();
        else return string.Format("{0:N0}", int.Parse(number.ToString().Replace(",", "")));
    }

    /// <summary>
    /// تبدیل عدد به حروف با فرمت خاص
    /// </summary>
    /// <param name="number">عدد مورد نظر</param>
    /// <returns></returns>
    public static string ToNormalNumber(this long number)
    {
        try
        {
            if (!IsNumber(number.ToString())) return number.ToString();
            if (number.ToString().StartsWith("0")) return number.ToString();

            var result = ToPlainNumber(number.ToString());
            return GetSpecialNumberString(Int64.Parse(result));

            #region Old Code
            //result = GetNumberString(Int64.Parse(result));
            //result = result.Substring(result.Length - 3).Contains("و")
            //    ? result.Substring(0, result.Length - 3)
            //    : result;
            //return result + " ریال ";



            //if (result.Contains("000000000"))
            //{
            //    if ((Int64.Parse(result) / 1000000000) > 1000)
            //        result = (Int64.Parse(result)).ToString("##,###") + " ریال ";
            //    else
            //        result = (Int64.Parse(result) / 1000000000).ToString("##,###") + " میلیارد ریال ";
            //}
            //else if (result.Contains("000000"))
            //{
            //    if ((Int64.Parse(result) / 1000000) > 1000)
            //        result = (Int64.Parse(result)).ToString("##,###") + " ریال ";
            //    else
            //        result = (Int64.Parse(result) / 1000000).ToString("##,###") + " میلیون ریال ";
            //}
            //else if (result.Contains("000"))
            //{
            //    if ((Int64.Parse(result) / 1000) > 1000)
            //        result = (Int64.Parse(result)).ToString("##,###") + " ریال ";
            //    else
            //        result = (Int64.Parse(result) / 1000).ToString("##,###") + " هزار ریال ";
            //}
            //else
            //    result = Int64.Parse(result).ToString("##,###") + " ریال "; 
            #endregion
        }
        catch
        {
            return number.ToString();
        }
    }

    /// <summary>
    /// تبدیل عدد به حروف با فرمت خاص
    /// </summary>
    /// <param name="number">عدد مورد نظر</param>
    /// <returns></returns>
    public static string ToNormalNumber(this int number)
    {
        try
        {
            if (!IsNumber(number.ToString())) return number.ToString();
            if (number.ToString().StartsWith("0")) return number.ToString();

            var result = ToPlainNumber(number.ToString());
            return GetSpecialNumberString(Int64.Parse(result));

            #region Old Code
            //result = GetNumberString(Int64.Parse(result));
            //result = result.Substring(result.Length - 3).Contains("و")
            //    ? result.Substring(0, result.Length - 3)
            //    : result;
            //return result + " ریال ";



            //if (result.Contains("000000000"))
            //{
            //    if ((Int64.Parse(result) / 1000000000) > 1000)
            //        result = (Int64.Parse(result)).ToString("##,###") + " ریال ";
            //    else
            //        result = (Int64.Parse(result) / 1000000000).ToString("##,###") + " میلیارد ریال ";
            //}
            //else if (result.Contains("000000"))
            //{
            //    if ((Int64.Parse(result) / 1000000) > 1000)
            //        result = (Int64.Parse(result)).ToString("##,###") + " ریال ";
            //    else
            //        result = (Int64.Parse(result) / 1000000).ToString("##,###") + " میلیون ریال ";
            //}
            //else if (result.Contains("000"))
            //{
            //    if ((Int64.Parse(result) / 1000) > 1000)
            //        result = (Int64.Parse(result)).ToString("##,###") + " ریال ";
            //    else
            //        result = (Int64.Parse(result) / 1000).ToString("##,###") + " هزار ریال ";
            //}
            //else
            //    result = Int64.Parse(result).ToString("##,###") + " ریال "; 
            #endregion
        }
        catch
        {
            return number.ToString();
        }
    }

    /// <summary>
    /// تبدیل عدد به حروف
    /// </summary>
    /// <param name="number">عدد مورد نظر</param>
    /// <returns></returns>
    public static string ToText(this long number)
    {
        return ToText(number, 1);
    }

    /// <summary>
    /// تبدیل عدد به حروف
    /// </summary>
    /// <param name="number">عدد مورد نظر</param>
    /// <returns></returns>
    public static string ToText(this int number)
    {
        return ToText(number, 1);
    }



    public static string ToSizeUnit(this long Bytes)
    {
        if (Bytes >= 0x1000000000000000) { return ((double)(Bytes >> 50) / 1024).ToString("0.### EB"); }
        if (Bytes >= 0x4000000000000) { return ((double)(Bytes >> 40) / 1024).ToString("0.### PB"); }
        if (Bytes >= 0x10000000000) { return ((double)(Bytes >> 30) / 1024).ToString("0.### TB"); }
        if (Bytes >= 0x40000000) { return ((double)(Bytes >> 20) / 1024).ToString("0.### GB"); }
        if (Bytes >= 0x100000) { return ((double)(Bytes >> 10) / 1024).ToString("0.### MB"); }
        if (Bytes >= 0x400) { return ((double)(Bytes) / 1024).ToString("0.###") + " KB"; }
        return Bytes.ToString("0 Bytes");
    }

    public static string ToSizeUnit(this string Bytes)
    {
        var number = long.Parse(Bytes);
        if (number >= 0x1000000000000000) { return ((double)(number >> 50) / 1024).ToString("0.### EB"); }
        if (number >= 0x4000000000000) { return ((double)(number >> 40) / 1024).ToString("0.### PB"); }
        if (number >= 0x10000000000) { return ((double)(number >> 30) / 1024).ToString("0.### TB"); }
        if (number >= 0x40000000) { return ((double)(number >> 20) / 1024).ToString("0.### GB"); }
        if (number >= 0x100000) { return ((double)(number >> 10) / 1024).ToString("0.### MB"); }
        if (number >= 0x400) { return ((double)(number) / 1024).ToString("0.###") + " KB"; }
        return number.ToString("0 Bytes");
    }

    public static string ToCurrency(this double value, string cultureName)
    {
        var currentCulture = new CultureInfo(cultureName);
        return string.Format(currentCulture, "{0:C}", value);
    }

}