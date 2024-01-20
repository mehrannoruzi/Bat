using System.Globalization;

namespace Bat.Core;

public class PersianDateTime
{
    private readonly static PersianCalendar _persianCalendar = new();
    private readonly static string[] _dayNames = ["شنبه", "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه"];
    private readonly static string[] _monthNames = ["فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"];

    private static readonly string AM = "ق.ظ";
    private static readonly string PM = "ب.ظ";

    private static readonly PersianDateTimeMode Mode = PersianDateTimeMode.UtcOffset;
    private static readonly TimeSpan DaylightSavingTimeStart = TimeSpan.FromDays(1);
    private static readonly TimeSpan DaylightSavingTimeEnd = TimeSpan.FromDays(185);
    private static readonly TimeSpan DaylightSavingTime = TimeSpan.FromHours(1);
    private static readonly TimeSpan OffsetFromUtc = new(3, 30, 0);

    public static TimeZoneInfo GetPersianTimeZoneInfo()
    {
        try
        {
            return TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
        }
        catch
        {
            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Asia/Tehran");
            }
            catch
            {
                var persianTimeZone = TimeZoneInfo.GetSystemTimeZones()
                    .FirstOrDefault(x =>
                        x.Id.Contains("Iran") ||
                        x.Id.Contains("Tehran") ||
                        x.Id.Contains("Persian") ||
                        x.Id.Contains("Ir-Fa"));

                if (persianTimeZone.IsNotNull()) return persianTimeZone;
                else return TimeZoneInfo.Local;
            }
        }
    }

    public static TimeZoneInfo PersianTimeZoneInfo = GetPersianTimeZoneInfo();

    public static TimeSpan operator -(PersianDateTime d1, PersianDateTime d2) => d1.ToDateTime() - d2.ToDateTime();

    public static bool operator >(PersianDateTime d1, PersianDateTime d2) => d1.ToDateTime() > d2.ToDateTime();

    public static bool operator >=(PersianDateTime d1, PersianDateTime d2) => d1.ToDateTime() >= d2.ToDateTime();

    public static bool operator <=(PersianDateTime d1, PersianDateTime d2) => d1.ToDateTime() <= d2.ToDateTime();

    public static PersianDateTime operator -(PersianDateTime d, TimeSpan t) => new(d.ToDateTime() - t);

    public static PersianDateTime operator +(PersianDateTime d, TimeSpan t) => new(d.ToDateTime() + t);

    public static bool operator <(PersianDateTime d1, PersianDateTime d2) => d1.ToDateTime() < d2.ToDateTime();

    public static bool operator ==(PersianDateTime d1, PersianDateTime d2)
    {
        if (d1 is null)
        {
            return d2 is null;
        }
        if (d2 is null)
        {
            return false;
        }
        return d1.ToDateTime() == d2.ToDateTime();
    }

    public static bool operator !=(PersianDateTime d1, PersianDateTime d2) => !(d1 == d2);

    public static string GetMonthName(int month) => _monthNames[month + 1];

    public static string GetDayName(int day) => _dayNames[day];

    public static bool IsLeapYear(int year) => _persianCalendar.IsLeapYear(year);

    public static int GetDaysInYear(int year) => _persianCalendar.GetDaysInYear(year);

    public static int GetDaysInMonth(int year, int month) => _persianCalendar.GetDaysInMonth(year, month);

    public static PersianDateTime Now
    {
        get
        {
            switch (Mode)
            {
                case PersianDateTimeMode.System:
                    return new PersianDateTime(DateTime.Now);

                case PersianDateTimeMode.PersianTimeZoneInfo:
                    return new PersianDateTime(TimeZoneInfo.ConvertTime(DateTime.Now, PersianTimeZoneInfo));

                case PersianDateTimeMode.UtcOffset:
                    PersianDateTime now = new(DateTime.UtcNow.Add(OffsetFromUtc));
                    return now.IsInDaylightSavingTime ? now.Add(DaylightSavingTime) : now;

                default:
                    throw new NotSupportedException(Mode.ToString());
            }
        }
    }

    public static PersianDateTime Parse(DateTime miladiDate) => new(miladiDate);

    public static PersianDateTime Parse(string persianDate) => Parse(persianDate, "0");

    public static PersianDateTime Parse(string persianDate, string time) => new(int.Parse(persianDate.Replace("/", "")), int.Parse(time.Replace(":", "")));

    private readonly DateTime _dateTime;

    public int Year => _persianCalendar.GetYear(_dateTime);

    public int Month => _persianCalendar.GetMonth(_dateTime);

    public int Day => _persianCalendar.GetDayOfMonth(_dateTime);

    public int Hour => _dateTime.Hour;

    public int Minute => _dateTime.Minute;

    public int Second => _dateTime.Second;

    public int Millisecond => _dateTime.Millisecond;

    public long Ticks => _dateTime.Ticks;

    private bool IsInDaylightSavingTime
    {
        get
        {
            TimeSpan timeOfYear = TimeOfYear;
            return timeOfYear > DaylightSavingTimeStart && timeOfYear < DaylightSavingTimeEnd;
        }
    }

    public TimeSpan TimeOfDay => _dateTime.TimeOfDay;

    public TimeSpan TimeOfYear => this - FirstDayOfYear;

    public TimeSpan TimeOfMonth => this - FirstDayOfMonth;

    public TimeSpan TimeOfWeek => this - FirstDayOfWeek;

    public PersianDateTime(DateTime dateTime) => _dateTime = dateTime;

    public PersianDateTime(int persianDate) : this(persianDate, 0)
    { }

    public PersianDateTime(int persianDate, short time) : this(persianDate, time * 100)
    { }

    public PersianDateTime(int persianDate, int time)
    {
        int year = persianDate / 10000;
        int month = (persianDate / 100) % 100;
        int day = persianDate % 100;

        int hour = time / 10000;
        int minute = (time / 100) % 100;
        int second = time % 100;

        _dateTime = _persianCalendar.ToDateTime(year, month, day, hour, minute, second, 0);
    }

    public PersianDateTime(int year, int month, int day) : this(year, month, day, 0, 0, 0)
    { }

    public PersianDateTime(int year, int month, int day, int hour, int minute, int second) => _dateTime = _persianCalendar.ToDateTime(year, month, day, hour, minute, second, 0);

    public bool IsLeapYear() => _persianCalendar.IsLeapYear(Year);

    public int DaysInYear => _persianCalendar.GetDaysInYear(Year);

    public int DaysInMonth => _persianCalendar.GetDaysInMonth(Year, Month);

    public int GetWeekOfYear(CalendarWeekRule rule) => _persianCalendar.GetWeekOfYear(_dateTime, rule, System.DayOfWeek.Saturday);

    public int DayOfYear => _persianCalendar.GetDayOfYear(_dateTime);

    public int DayOfWeek => ((int)_dateTime.DayOfWeek + 1) % 7;

    public string DayName => _dayNames[DayOfWeek];

    public string MonthName => _monthNames[Month - 1];

    public PersianDateTime Date => new(_dateTime.Date);

    public PersianDateTime FirstDayOfYear => AddDays(-DayOfYear + 1).Date;

    public PersianDateTime LastDayOfYear => AddDays(DaysInYear - DayOfYear).Date;

    public PersianDateTime FirstDayOfMonth => AddDays(-Day + 1).Date;

    public PersianDateTime LastDayOfMonth => AddDays(DaysInMonth - Day).Date;

    public PersianDateTime FirstDayOfWeek => AddDays(-DayOfWeek).Date;

    public PersianDateTime LastDayOfWeek => AddDays(6 - DayOfWeek).Date;

    public PersianDateTime AddSeconds(double value) => new(_dateTime.AddSeconds(value));

    public PersianDateTime AddMinutes(double value) => new(_dateTime.AddMinutes(value));

    public PersianDateTime AddHours(double value) => new(_dateTime.AddHours(value));

    public PersianDateTime AddYears(int value) => new PersianDateTime(Year + value, Month, Day).Add(TimeOfDay);

    public PersianDateTime AddMonths(int value)
    {
        var months = Month + value;

        var newYear = Year + (months > 0 ? (months - 1) / 12 : months / 12 - 1);
        var newMonth = months > 0 ? (months - 1) % 12 + 1 : months % 12 + 12;

        var daysInNewMonth = PersianDateTime.GetDaysInMonth(newYear, newMonth);
        var newDay = daysInNewMonth < Day ? daysInNewMonth : Day;

        return new PersianDateTime(newYear, newMonth, newDay).Add(TimeOfDay);
    }

    public PersianDateTime AddDays(double value) => new(_dateTime.AddDays(value));

    public PersianDateTime Add(TimeSpan value) => new(_dateTime.Add(value));

    public DateTime ToDateTime() => _dateTime;

    public int ToInt() => int.Parse(Year.ToString() + Month.ToString().PadLeft(2, '0') + Day.ToString().PadLeft(2, '0'));

    public override string ToString() => ToString(PersianDateTimeFormat.DateTime);

    public string ToString(string format)
    {
        string towDigitYear = (Year % 100).ToString();
        string month = Month.ToString();
        string day = Day.ToString();
        string fullHour = Hour.ToString();
        string hour = (Hour % 12 == 0 ? 12 : Hour % 12).ToString();
        string minute = Minute.ToString();
        string second = Second.ToString();
        string dayPart = Hour >= 12 ? PM : AM;

        return format.Replace("yyyy", Year.ToString())
                     .Replace("yy", towDigitYear.PadLeft(2, '0'))
                     .Replace("y", towDigitYear)
                     .Replace("MMMM", MonthName)
                     .Replace("MM", month.PadLeft(2, '0'))
                     .Replace("M", month)
                     .Replace("dddd", DayName)
                     .Replace("ddd", DayName[0].ToString())
                     .Replace("dd", day.PadLeft(2, '0'))
                     .Replace("d", day)
                     .Replace("HH", fullHour.PadLeft(2, '0'))
                     .Replace("H", fullHour.ToString())
                     .Replace("hh", hour.PadLeft(2, '0'))
                     .Replace("h", hour.ToString())
                     .Replace("mm", minute.PadLeft(2, '0'))
                     .Replace("m", minute.ToString())
                     .Replace("ss", second.PadLeft(2, '0'))
                     .Replace("s", second)
                     .Replace("tt", dayPart)
                     .Replace('t', dayPart[0]);
    }

    public string ToString(PersianDateTimeFormat format = default)
    {
        return format switch
        {
            PersianDateTimeFormat.DateTime => ToString(PersianDateTimeFormat.Date) + " " + TimeOfDay.ToHHMMSS(),
            PersianDateTimeFormat.DateShortTime => ToString(PersianDateTimeFormat.Date) + " " + TimeOfDay.ToHHMM(),
            PersianDateTimeFormat.LongDate => DayName + " " + Day + " " + MonthName,
            PersianDateTimeFormat.LongDateFullTime => DayName + " " + Day + " " + MonthName + " ساعت " + TimeOfDay.ToHHMMSS(),
            PersianDateTimeFormat.LongDateLongTime => DayName + " " + Day + " " + MonthName + " ساعت " + TimeOfDay.ToHHMM(),
            PersianDateTimeFormat.ShortDateShortTime => Day.ToString() + " " + MonthName + " " + TimeOfDay.ToHHMM(),
            PersianDateTimeFormat.FullDate => DayName + " " + Day + " " + MonthName + " " + Year,
            PersianDateTimeFormat.FullDateLongTime => DayName + " " + Day + " " + MonthName + " " + Year + " ساعت " + TimeOfDay.ToHHMM(),
            PersianDateTimeFormat.FullDateFullTime => DayName + " " + Day + " " + MonthName + " " + Year + " ساعت " + TimeOfDay.ToHHMMSS(),
            _ => Year.ToString() + "/" + Month.ToString().PadLeft(2, '0') + "/" + Day.ToString().PadLeft(2, '0'),
        };
    }

    public override int GetHashCode() => _dateTime.GetHashCode();

    public override bool Equals(object value) => Equals(value as PersianDateTime);

    public bool Equals(PersianDateTime value)
    {
        if (value is null)
        {
            return false;
        }
        return _dateTime.Equals(value._dateTime);
    }

    public static string GetRelativeTime(DateTime dateTime)
    {
        const int SECOND = 1;
        const int MINUTE = 60 * SECOND;
        const int HOUR = 60 * MINUTE;
        const int DAY = 24 * HOUR;
        const int MONTH = 30 * DAY;

        var ts = new TimeSpan(DateTime.Now.Ticks - dateTime.Ticks);
        double delta = Math.Abs(ts.TotalSeconds);
        if (delta < 1 * MINUTE)
            return ts.Seconds == 1 ? "لحظه ای قبل" : ts.Seconds + " ثانیه قبل";
        if (delta < 2 * MINUTE)
            return "یک دقیقه قبل";
        if (delta < 45 * MINUTE)
            return ts.Minutes + " دقیقه قبل";
        if (delta < 90 * MINUTE)
            return "یک ساعت قبل";
        if (delta < 24 * HOUR)
            return ts.Hours + " ساعت قبل";
        if (delta < 48 * HOUR)
            return "دیروز";
        if (delta < 30 * DAY)
            return ts.Days + " روز قبل";
        if (delta < 12 * MONTH)
        {
            int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
            return months <= 1 ? "یک ماه قبل" : months + " ماه قبل";
        }
        int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
        return years <= 1 ? "یک سال قبل" : years + " سال قبل";
    }
}
