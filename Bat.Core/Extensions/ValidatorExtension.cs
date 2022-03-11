using System.Text;

namespace Bat.Core;

public static class ValidatorExtension
{
    public static bool IsIp(this string ip)
    {
        if (string.IsNullOrWhiteSpace(ip)) return false;

        var regex = new Regex(RegexPattern.Ip1);
        if (!regex.Match(ip).Success) return false;

        return true;
    }

    public static bool IsIp2(this string ip)
    {
        if (string.IsNullOrWhiteSpace(ip)) return false;

        var regex = new Regex(RegexPattern.Ip2);
        if (!regex.Match(ip).Success) return false;

        return true;
    }

    public static bool IsUrl(this string url)
    {
        if (string.IsNullOrWhiteSpace(url)) return false;

        var regex = new Regex(RegexPattern.Url);
        if (!regex.Match(url).Success) return false;

        return true;
    }

    public static bool IsIban(this string iban)
    {
        if (string.IsNullOrWhiteSpace(iban)) return false;
        iban = iban.ToUpper().Trim().Replace(" ", string.Empty);
        if (!Regex.IsMatch(iban, "^[A-Z]{2}[0-9]{24}$")) return false;

        string bank = iban.Substring(4, iban.Length - 4) + iban.Substring(0, 4);
        int asciiShift = 55;
        StringBuilder sb = new StringBuilder();
        foreach (char c in bank)
        {
            int v;
            if (char.IsLetter(c)) v = c - asciiShift;
            else v = int.Parse(c.ToString());
            sb.Append(v);
        }

        string checkSumString = sb.ToString();
        int checksum = int.Parse(checkSumString.Substring(0, 1));
        for (int i = 1; i < checkSumString.Length; i++)
        {
            int v = int.Parse(checkSumString.Substring(i, 1));
            checksum *= 10;
            checksum += v;
            checksum %= 97;
        }
        if (checksum != 1) return false;

        return true;
    }

    public static bool IsEmail(this string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        var regex = new Regex(RegexPattern.Email);
        if (!regex.Match(email).Success) return false;

        return true;
    }

    public static bool IsTime(this string time)
    {
        if (string.IsNullOrWhiteSpace(time)) return false;

        var regex = new Regex(RegexPattern.Time);
        if (!regex.Match(time).Success) return false;

        return true;
    }

    public static bool IsPersianDate(this string persianDate)
    {
        if (string.IsNullOrWhiteSpace(persianDate)) return false;

        var regex = new Regex(RegexPattern.PersianDateTime);
        if (!regex.Match(persianDate).Success) return false;

        return true;
    }

    public static bool IsDateTime(this string dateTime)
    {
        if (string.IsNullOrWhiteSpace(dateTime)) return false;

        var regex = new Regex(RegexPattern.LatinDateTime);
        if (!regex.Match(dateTime).Success) return false;

        return true;
    }

    public static bool IsNationalCode(this string nationalCode)
    {
        if (string.IsNullOrWhiteSpace(nationalCode)) return false;
        if (nationalCode.Length != 10) return false;
        if (!long.TryParse(nationalCode, out long number)) return false;

        var numbers = nationalCode.ToCharArray().Select(i => Convert.ToInt32(i.ToString())).ToList();
        var checkNumber = numbers.Last();
        numbers.RemoveAt(9);
        numbers.Reverse();
        var sum = 0;
        for (int i = 0; i < numbers.Count; i++)
            sum += numbers[i] * (i + 2);

        var remaining = sum % 11;
        if (remaining < 2)
            if (remaining != checkNumber) return false;
            else
            if ((11 - remaining) != checkNumber) return false;

        return true;
    }

    public static bool IsBankCardNumber(this string bankCardNumber)
    {
        if (string.IsNullOrWhiteSpace(bankCardNumber)) return false;
        if (bankCardNumber.Length != 19) return false;

        var regex = new Regex(RegexPattern.BankCardNumber);
        if (!regex.Match(bankCardNumber).Success) return false;

        return true;
    }

    public static bool IsBankAccountNumber(this string accountNumber)
    {
        if (string.IsNullOrWhiteSpace(accountNumber)) return false;
        if (accountNumber.Length <= 6) return false;

        return true;
    }

    public static bool IsBankShaba(this string shaba)
    {
        if (string.IsNullOrWhiteSpace(shaba)) return false;
        if (shaba.Length != 24) return false;

        return true;
    }
}