namespace Bat.Core;

public static class TimeSpanExtension
{
    public static string ToTimeFormat(long seconds, TimeFormat format = TimeFormat.Standard)
    {
        if (seconds > 0)
        {
            var timeSpan = TimeSpan.FromSeconds(seconds);
            var h = timeSpan.Hours;
            var m = timeSpan.Minutes;
            var s = timeSpan.Seconds;
            var ms = timeSpan.Milliseconds;

            switch (format)
            {
                case TimeFormat.LongTime:
                    return string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", h, m, s, ms);
                case TimeFormat.Full:
                    return string.Format("{0:D2}:{1:D2}:{2:D2}", h, m, s);
                case TimeFormat.Standard:
                    return string.Format("{0:D2}:{1:D2}", h, m);
                case TimeFormat.ToHours:
                    return string.Format("{0}", h);
                case TimeFormat.ToMinutes:
                    return string.Format("{0}", m);
                case TimeFormat.ToMiliSeconds:
                    return string.Format("{0}", ms);
                default:
                    return string.Format("{0:D2}:{1:D2}:{2:D2}", h, m, s);
            }
        }

        return string.Format("{0}", seconds);
    }

    public static string ToTimeFormat(int minutes, TimeFormat format = TimeFormat.Standard) => ToTimeFormat(minutes * 60, format);

    public static int ToInteger(this TimeSpan time) => int.Parse(time.Hours.ToString() + time.Minutes.ToString().PadLeft(2, '0') + time.Seconds.ToString().PadLeft(2, '0'));

    public static short ToShort(this TimeSpan time) => short.Parse(time.Hours.ToString() + time.Minutes.ToString().PadLeft(2, '0'));

    public static string ToHHMM(this TimeSpan time) => time.ToString("hh\\:mm");

    public static string ToHHMMSS(this TimeSpan time) => time.ToString("hh\\:mm\\:ss");
}