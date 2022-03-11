using System.Data;
using System.Text;
using System.Data.Common;

namespace Bat.Core;

public enum NumericCultureType
{
    PersianToEnglish,
    PersianToArabic,
    ArabicToPersian,
    ArabicToEnglish,
    EnglishToPersian,
    EnglishToArabic
}

public static class EnglishNumberExtension
{
    public static string ToEnglishNumber(this string input, NumericCultureType convertType = NumericCultureType.PersianToEnglish)
    {
        string[] persian = { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
        string[] arabic = { "٠", "١", "٢", "٣", "٤", "٥", "٦", "٧", "٨", "٩" };

        for (int i = 0; i <= 9; i++)
        {
            switch (convertType)
            {
                case NumericCultureType.PersianToEnglish:
                    input = input.Replace(persian[i], i.ToString());
                    break;
                case NumericCultureType.PersianToArabic:
                    input = input.Replace(persian[i], arabic[i]);
                    break;
                case NumericCultureType.ArabicToPersian:
                    input = input.Replace(arabic[i], persian[i]);
                    break;
                case NumericCultureType.ArabicToEnglish:
                    input = input.Replace(arabic[i], i.ToString());
                    break;
                case NumericCultureType.EnglishToPersian:
                    input = input.Replace(i.ToString(), persian[i]);
                    break;
                case NumericCultureType.EnglishToArabic:
                    input = input.Replace(i.ToString(), arabic[i]);
                    break;
            }
        }

        return input;
    }

    public static string ToEnglishNumber(this string input)
    {
        //Persian Ascii 1776-1785
        //Arabic  Ascii 1632-1641
        var myString = input.ToArray();
        var result = new StringBuilder().Clear();

        for (int i = 0; i < myString.Length; i++)
        {
            if (myString[i] >= 1776 && myString[i] <= 1785)
            {
                result.Append((char)((myString[i] - 1776) + 48));
            }
            else if (myString[i] >= 1632 && myString[i] <= 1641)
            {
                result.Append((char)((myString[i] - 1632) + 48));
            }
            else
            {
                result.Append(myString[i]);
            }
        }

        return result.ToString();
    }

    public static string ToEnglishNumber2(this string input)
    {
        return input.Replace("۰", "0")
                    .Replace("۱", "1")
                    .Replace("۲", "2")
                    .Replace("۳", "3")
                    .Replace("۴", "4")
                    .Replace("۵", "5")
                    .Replace("۶", "6")
                    .Replace("۷", "7")
                    .Replace("۸", "8")
                    .Replace("۹", "9")
                    //iphone numeric
                    .Replace("٠", "0")
                    .Replace("١", "1")
                    .Replace("٢", "2")
                    .Replace("٣", "3")
                    .Replace("٤", "4")
                    .Replace("٥", "5")
                    .Replace("٦", "6")
                    .Replace("٧", "7")
                    .Replace("٨", "8")
                    .Replace("٩", "9");
    }

    public static void ToEnglishNumber(this DbCommand command)
    {
        command.CommandText = command.CommandText.ToEnglishNumber();

        foreach (DbParameter parameter in command.Parameters)
        {
            switch (parameter.DbType)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                case DbType.StringFixedLength:
                case DbType.Xml:
                    parameter.Value = parameter.Value.ToString().ToEnglishNumber();
                    break;
            }
        }
    }

    public static void ToEnglishNumber2(this DbCommand command)
    {
        command.CommandText = command.CommandText.ToEnglishNumber2();

        foreach (DbParameter parameter in command.Parameters)
        {
            switch (parameter.DbType)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                case DbType.StringFixedLength:
                case DbType.Xml:
                    parameter.Value = parameter.Value.ToString().ToEnglishNumber2();
                    break;
            }
        }
    }

}