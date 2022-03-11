namespace Bat.Core;

public class FileConvertor
{
    static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

    private static string ToCurrectSize(long value, int decimalPlaces = 2)
    {
        if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
        if (value < 0) { return "-" + ToCurrectSize(-value); }
        if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }

        // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
        int mag = (int)Math.Log(value, 1024);

        // 1L << (mag * 10) == 2 ^ (10 * mag) 
        // [i.e. the number of bytes in the unit corresponding to mag]
        decimal adjustedSize = (decimal)value / (1L << (mag * 10));

        // make adjustment when the value is large enough that
        // it would round up to 1000 or more
        if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
        {
            mag += 1;
            adjustedSize /= 1024;
        }

        return string.Format("{0:n" + decimalPlaces + "} {1}", adjustedSize, SizeSuffixes[mag]);
    }

    public static byte[] ToByteArray(string file) => Convert.FromBase64String(file);

    public static string ToBase64(byte[] file) => Convert.ToBase64String(file);

    public static string ToStandardSize(long size) => ToCurrectSize(size);

    public static string ToStandardSize(long size, SizeType inputSizeType)
    {
        string result;
        switch (inputSizeType)
        {
            case SizeType.Bit:
                result = Math.Round(((double)size * 8), 2) + " Bit";
                break;
            case SizeType.Byte:
                result = Math.Round(((double)size), 2) + " Byte";
                break;
            case SizeType.KB:
                result = Math.Round(((double)size / 1024), 2) + " KB";
                break;
            case SizeType.MB:
                result = Math.Round((((double)size / 1024) / 1024), 2) + " MB";
                break;
            case SizeType.GB:
                result = Math.Round(((((double)size / 1024) / 1024) / 1024), 2) + " GB";
                break;
            case SizeType.TB:
                result = Math.Round((((((double)size / 1024) / 1024) / 1024) / 1024), 2) + " TB";
                break;

            default:
                result = Math.Round(((double)size), 2) + " Byte";
                break;
        }

        return result;
    }

    public static string ToStandardSize(long size, SizeType inputSizeType, SizeType outputSizeType) => string.Empty;

    public static float ToNewSize(long size, SizeType inputSizeType, SizeType outputSizeType) => 1;
}