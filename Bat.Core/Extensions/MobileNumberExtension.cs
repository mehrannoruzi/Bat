namespace Bat.Core;

public static class MobileNumberExtension
{
    public static bool IsMobileNumber(this string mobileNumber)
    {
        try
        {
            Regex rgx = new(RegexPattern.MobileNumber);
            return rgx.IsMatch(mobileNumber);
        }
        catch
        {
            return false;
        }
    }

    public static bool IsMobileNumber(this long mobileNumber)
    {
        try
        {
            Regex rgx = new(RegexPattern.MobileNumber);
            return rgx.IsMatch(mobileNumber.ToString());
        }
        catch
        {
            return false;
        }
    }

    public static bool IsMciMobileNumber(string number)
    {
        if (!IsMobileNumber(number)) return false;

        var regex = new Regex(RegexPattern.MciMobileNumber);
        if (!regex.Match(number).Success) return false;

        return true;
    }

    public static bool IsIrancellMobileNumber(string number)
    {
        if (!IsMobileNumber(number)) return false;

        var regex = new Regex(RegexPattern.IrancellMobileNumber);
        if (!regex.Match(number).Success) return false;

        return true;
    }



    public static string ToMobileNumberPattern(this string mobileNumber)
    {
        string pattern = mobileNumber;

        if (string.IsNullOrEmpty(mobileNumber)) return string.Empty;
        if (mobileNumber.Length == 10) pattern = "98" + mobileNumber;
        else if (mobileNumber.Length == 11)
        {
            if (mobileNumber.StartsWith("0"))
            {
                pattern = "98" + mobileNumber.Substring(1);
            }
        }
        else if (mobileNumber.Length == 13)
            pattern = mobileNumber.Substring(1);

        return pattern;
    }

    public static string ToMobileNumberPattern(this long mobileNumber) => $"0{mobileNumber}";

    public static long ToMobileNumber(this string mobileNumber)
    {
        if (long.TryParse(mobileNumber, out long value))
            return long.Parse(mobileNumber);

        return 0;
    }

    public static long ToStandardMobileNumber(this string mobileNumber)
        => long.Parse(mobileNumber.StartsWith("98") ? mobileNumber.Substring(2, mobileNumber.Length) : mobileNumber);

    public static long ToMobileNumber(this long mobileNumber)
    {
        if (!mobileNumber.ToString().IsMobileNumber()) return 0;

        if (mobileNumber.ToString().Length == 12) return mobileNumber;
        else if (mobileNumber.ToString().Length == 11) return long.Parse("98" + mobileNumber.ToString().Substring(1));
        else if (mobileNumber.ToString().Length == 10) return long.Parse("98" + mobileNumber);

        return 0;
    }

}