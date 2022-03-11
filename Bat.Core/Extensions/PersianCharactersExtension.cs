using System.Data;
using System.Data.Common;

namespace Bat.Core;

public static class PersianCharactersExtension
{
    public const char ArabicYeChar = (char)1610;
    public const char PersianYeChar = (char)1740;
    public const char ArabicKeChar = (char)1603;
    public const char PersianKeChar = (char)1705;


    public static string ToPersianCharacters(this string data)
        => string.IsNullOrWhiteSpace(data) ? string.Empty : data.Replace(ArabicYeChar, PersianYeChar).Replace(ArabicKeChar, PersianKeChar).Trim();

    public static string ToPersianCharacters2(this string data)
    {
        return data.Replace("ﮎ", "ک")
                    .Replace("ﮏ", "ک")
                    .Replace("ﮐ", "ک")
                    .Replace("ﮑ", "ک")
                    .Replace("ك", "ک")
                    .Replace("ي", "ی")
                    .Replace(" ", " ")
                    .Replace("‌", " ")
                    .Replace("ھ", "ه");//.Replace("ئ", "ی");
    }


    public static void ToPersianCharacters(this DbCommand command)
    {
        command.CommandText = command.CommandText.ToPersianCharacters();

        foreach (DbParameter parameter in command.Parameters)
        {
            switch (parameter.DbType)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                case DbType.StringFixedLength:
                case DbType.Xml:
                    parameter.Value = parameter.Value.ToString().ToPersianCharacters();
                    break;
            }
        }
    }

    public static void ToPersianCharacters2(this DbCommand command)
    {
        command.CommandText = command.CommandText.ToPersianCharacters2();

        foreach (DbParameter parameter in command.Parameters)
        {
            switch (parameter.DbType)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                case DbType.StringFixedLength:
                case DbType.Xml:
                    parameter.Value = parameter.Value.ToString().ToPersianCharacters2();
                    break;
            }
        }
    }
}