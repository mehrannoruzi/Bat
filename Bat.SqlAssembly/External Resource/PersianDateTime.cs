using System;
using System.Linq;
using System.Globalization;

namespace Bat.Sql
{
    /// <summary>
    /// ���� ��?�?� ���?� ���?
    /// </summary>
    public class PersianDateTime
    {
        private readonly static PersianCalendar _persianCalendar = new PersianCalendar();
        private readonly static string[] _dayNames = new string[] { "����", "?�����", "������", "�� ����", "��������", "��� ����", "����" };
        private readonly static string[] _monthNames = new string[] { "�����?�", "���?����", "�����", "�?�", "�����", "���?��", "���", "����", "���", "�?", "����", "�����" };

        private static string AM = "�.�";
        private static string PM = "�.�";

        private static PersianDateTimeMode Mode = PersianDateTimeMode.UtcOffset;
        private static TimeSpan DaylightSavingTimeStart = TimeSpan.FromDays(1);
        private static TimeSpan DaylightSavingTimeEnd = TimeSpan.FromDays(185);
        private static TimeSpan DaylightSavingTime = TimeSpan.FromHours(1);
        private static TimeSpan OffsetFromUtc = new TimeSpan(3, 30, 0);

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
                    var persianTimeZone = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(x =>
                        x.StandardName.Contains("Iran") ||
                        x.StandardName.Contains("Tehran") ||
                        x.StandardName.Contains("Ir") ||
                        x.StandardName.Contains("Persian"));

                    if (persianTimeZone != null) return persianTimeZone;
                    else return TimeZoneInfo.Local;
                }
            }
        }

        /// <summary>
        /// System.TimeZoneInfo ��?��� ��?� ��� �?��� � ���� ���� �� �� ��̘�
        /// </summary>
        public static TimeZoneInfo PersianTimeZoneInfo = GetPersianTimeZoneInfo();

        /// <summary>
        /// �� ���� 2 ��� ���?� ���? �� ���?��
        /// </summary>
        /// <param name="d1">���?� ���? ���?</param>
        /// <param name="d2">���?� ���? ���?</param>
        /// <returns>�?��� ����� ? �?� �?� 2 ���?�</returns>
        public static TimeSpan operator -(PersianDateTime d1, PersianDateTime d2)
        {
            return d1.ToDateTime() - d2.ToDateTime();
        }

        /// <summary>
        /// ���?�� 2 �� ���?� ���? �� ?��?��
        /// </summary>
        /// <param name="d1">���?� ���? ���?</param>
        /// <param name="d2">���?� ���? ���?</param>
        /// <returns>False ����� ����� ���? ��ѐ�� ���� �� �?� �?� ���� True </returns>
        public static bool operator >(PersianDateTime d1, PersianDateTime d2)
        {
            return d1.ToDateTime() > d2.ToDateTime();
        }

        /// <summary>
        /// ���?�� 2 �� ���?� ���? �� ?��?��   /// </summary>
        /// <param name="d1">���?� ���? ���?</param>
        /// <param name="d2">���?� ���? ���?</param>
        /// <returns>False ����� ����� ���? ��ѐ�� � ?� ����? ���� �� �?� �?� ���� True </returns>
        public static bool operator >=(PersianDateTime d1, PersianDateTime d2)
        {
            return d1.ToDateTime() >= d2.ToDateTime();
        }

        /// <summary>
        /// ���?�� 2 �� ���?� ���? �� ?��?��   
        /// </summary>
        /// <param name="d1">���?� ���? ���?</param>
        /// <param name="d2">���?� ���? ���?</param>
        /// <returns>False ����� ����� ���? �捘�� � ?� ����? ���� �� �?� �?� ���� True </returns>
        public static bool operator <=(PersianDateTime d1, PersianDateTime d2)
        {
            return d1.ToDateTime() <= d2.ToDateTime();
        }

        /// <summary>
        /// �� ���� ����? �� ���?� ���?
        /// </summary>
        /// <param name="d">���?� ���?</param>
        /// <param name="t">����</param>
        /// <returns>���?� ���? ��� ���</returns>
        public static PersianDateTime operator -(PersianDateTime d, TimeSpan t)
        {
            return new PersianDateTime(d.ToDateTime() - t);
        }

        /// <summary>
        /// ����� ���� ����? �� ���?� ���?
        /// </summary>
        /// <param name="d">���?� ���?</param>
        /// <param name="t">����</param>
        /// <returns>���?� ���? ����� ���</returns>
        public static PersianDateTime operator +(PersianDateTime d, TimeSpan t)
        {
            return new PersianDateTime(d.ToDateTime() + t);
        }

        /// <summary>
        /// ���?�� 2 �� ���?� ���? �� ?��?��   
        /// </summary>
        /// <param name="d1">���?� ���? ���?</param>
        /// <param name="d2">���?� ���? ���?</param>
        /// <returns>False ����� ����� ���? �捘�� ���� �� �?� �?� ���� True </returns>
        public static bool operator <(PersianDateTime d1, PersianDateTime d2)
        {
            return d1.ToDateTime() < d2.ToDateTime();
        }

        /// <summary>
        /// ���?�� 2 �� ���?� ���? �� ?��?��   
        /// </summary>
        /// <param name="d1">���?� ���? ���?</param>
        /// <param name="d2">���?� ���? ���?</param>
        /// <returns>False ����� ����� ���? ��?���� ����� ���? ?���� ����(�� �� ���?� � �� �� ���� ) �� �?� �?� ���� True </returns>
        public static bool operator ==(PersianDateTime d1, PersianDateTime d2)
        {
            if (object.ReferenceEquals(d1, null))
            {
                return object.ReferenceEquals(d2, null);
            }
            if (object.ReferenceEquals(d2, null))
            {
                return false;
            }
            return d1.ToDateTime() == d2.ToDateTime();
        }

        /// <summary>
        /// ���?�� 2 �� ���?� ���? �� ?��?��   
        /// </summary>
        /// <param name="d1">���?� ���? ���?</param>
        /// <param name="d2">���?� ���? ���?</param>
        /// <returns>False ����� ����� ���? ����� ����� ��� ���� �� �?� �?� ���� True </returns>
        public static bool operator !=(PersianDateTime d1, PersianDateTime d2)
        {
            return !(d1 == d2);
        }

        /// <summary>
        /// ��?��� ��� ��� �������?
        /// </summary>
        /// <param name="month">��� ��� �?� 1 �� 12</param>
        /// <returns>��� ��� �������?</returns>
        public static string GetMonthName(int month)
        {
            return _monthNames[month + 1];
        }

        /// <summary>
        /// ��?��� ��� ��� �������?
        /// </summary>
        /// <param name="day">��� ��� �?� 0 �� 6</param>
        /// <returns>��� ��� �������?</returns>
        public static string GetDayName(int day)
        {
            return _dayNames[day];
        }

        /// <summary>
        /// ���?� �?�� �?� ��� ���? ��� ��?�� ��� ?� �??�
        /// </summary>
        /// <param name="year">��� ��� ����? �?� 1 �� 9378</param>
        /// <returns>��? � �� ����? �� ��� �������? ��� ��?�� ����</returns>
        public static bool IsLeapYear(int year)
        {
            return _persianCalendar.IsLeapYear(year);
        }

        /// <summary>
        /// ��?��� ����� �����? ?� ��� ����
        /// </summary>
        /// <param name="year">��� ��� ����? �?� 1 �� 9378</param>
        /// <returns>����� ��� �����? ���� ��� 365 ��Ґ������ �? ��� �� �?� �?� ���� 366</returns>
        public static int GetDaysInYear(int year)
        {
            return _persianCalendar.GetDaysInYear(year);
        }

        /// <summary>
        /// Returns the number of days in the specified month of the specified year.
        /// </summary>
        /// <param name="year">An integer from 1 through 9378 that represents the year.</param>
        /// <param name="month">An integer that represents the month, and ranges from 1 through 12.</param>
        /// <returns>The number of days in the specified month of the specified year.</returns>
        public static int GetDaysInMonth(int year, int month)
        {
            return _persianCalendar.GetDaysInMonth(year, month);
        }

        /// <summary>
        /// Gets a PersianDateTime object that is set to the current date and time in the persian calendar on this computer.
        /// </summary>
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
                        PersianDateTime now = new PersianDateTime(DateTime.UtcNow.Add(OffsetFromUtc));
                        return now.IsInDaylightSavingTime ? now.Add(DaylightSavingTime) : now;

                    default:
                        throw new NotSupportedException(Mode.ToString());
                }
            }
        }

        /// <summary>
        ///���?� ���?� �?���? �� ���?
        /// </summary>
        /// <param name="miladiDate">���?� �?���?</param>
        public static PersianDateTime Parse(DateTime miladiDate)
        {
            return new PersianDateTime(miladiDate);
        }

        /// <summary>
        /// Converts the specified string representation of a date to its PersianDateTime equivalent.
        /// </summary>
        /// <param name="persianDate">A string containing a date to convert.</param>
        public static PersianDateTime Parse(string persianDate)
        {
            return Parse(persianDate, "0");
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its PersianDateTime equivalent.
        /// </summary>
        /// <param name="persianDate">A string containing a date to convert.</param>
        /// <param name="time">A string containing a time to convert.</param>
        public static PersianDateTime Parse(string persianDate, string time)
        {
            return new PersianDateTime(int.Parse(persianDate.Replace("/", "")), int.Parse(time.Replace(":", "")));
        }

        private readonly DateTime _dateTime;

        /// <summary>
        /// Gets the year component of the date represented by this instance.
        /// </summary>
        public int Year
        {
            get { return _persianCalendar.GetYear(_dateTime); }
        }

        /// <summary>
        /// Gets the month component of the date represented by this instance.
        /// </summary>
        public int Month
        {
            get { return _persianCalendar.GetMonth(_dateTime); }
        }

        /// <summary>
        /// Gets the day of the month represented by this instance.
        /// </summary>
        public int Day
        {
            get { return _persianCalendar.GetDayOfMonth(_dateTime); }
        }

        /// <summary>
        /// Gets the hour component of the date represented by this instance.
        /// </summary>
        public int Hour
        {
            get { return _dateTime.Hour; }
        }

        /// <summary>
        /// Gets the minute component of the date represented by this instance.
        /// </summary>
        public int Minute
        {
            get { return _dateTime.Minute; }
        }

        /// <summary>
        /// Gets the seconds component of the date represented by this instance.
        /// </summary>
        public int Second
        {
            get { return _dateTime.Second; }
        }

        /// <summary>
        /// Gets the milliseconds component of the date represented by this instance.
        /// </summary>
        public int Millisecond
        {
            get { return _dateTime.Millisecond; }
        }

        /// <summary>
        /// Gets the number of ticks that represent the date and time of this instance.
        /// </summary>
        public long Ticks
        {
            get { return _dateTime.Ticks; }
        }

        private bool IsInDaylightSavingTime
        {
            get
            {
                TimeSpan timeOfYear = TimeOfYear;
                return timeOfYear > DaylightSavingTimeStart && timeOfYear < DaylightSavingTimeEnd;
            }
        }

        /// <summary>
        /// Gets the time of day for this instance.
        /// </summary>
        public TimeSpan TimeOfDay
        {
            get { return _dateTime.TimeOfDay; }
        }

        /// <summary>
        /// Gets the time of year for this instance.
        /// </summary>
        public TimeSpan TimeOfYear
        {
            get { return this - FirstDayOfYear; }
        }

        /// <summary>
        /// Gets the time of month for this instance.
        /// </summary>
        public TimeSpan TimeOfMonth
        {
            get { return this - FirstDayOfMonth; }
        }

        /// <summary>
        /// Gets the time of week for this instance.
        /// </summary>
        public TimeSpan TimeOfWeek
        {
            get { return this - FirstDayOfWeek; }
        }

        /// <summary>
        /// Initializes a new instance of the PersianDateTime class to a specified dateTime.
        /// </summary>
        /// <param name="dateTime">A date and time in the Gregorian calendar.</param>
        public PersianDateTime(DateTime dateTime)
        {
            _dateTime = dateTime;
        }

        /// <summary>
        /// Initializes a new instance of the PersianDateTime class to the specified persian date.
        /// </summary>
        /// <param name="persianDate">The persian date</param>
        public PersianDateTime(int persianDate)
            : this(persianDate, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PersianDateTime class to the specified persian date and time.
        /// </summary>
        /// <param name="persianDate">The persian date.</param>
        /// <param name="time">The time.</param>
        public PersianDateTime(int persianDate, short time)
            : this(persianDate, time * 100)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PersianDateTime class to the specified persian date and time.
        /// </summary>
        /// <param name="persianDate">The persian date.</param>
        /// <param name="time">The time.</param>
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

        /// <summary>
        /// Initializes a new instance of the PersianDateTime class to the specified year, month, and day.
        /// </summary>
        /// <param name="year">The year (1 through 9999).</param>
        /// <param name="month">The month (1 through 12).</param>
        /// <param name="day">The day (1 through the number of days in month).</param>
        public PersianDateTime(int year, int month, int day)
            : this(year, month, day, 0, 0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PersianDateTime class to the specified year, month, day, hour, minute, and second.
        /// </summary>
        /// <param name="year">The year (1 through 9999).</param>
        /// <param name="month">The month (1 through 12).</param>
        /// <param name="day">The day (1 through the number of days in month).</param>
        /// <param name="hour">The hours (0 through 23).</param>
        /// <param name="minute">The minutes (0 through 59).</param>
        /// <param name="second">The seconds (0 through 59).</param>
        public PersianDateTime(int year, int month, int day, int hour, int minute, int second)
        {
            _dateTime = _persianCalendar.ToDateTime(year, month, day, hour, minute, second, 0);
        }

        /// <summary>
        /// Determines whether the year represented by this instance is a leap year. The number of days is 366 in a leap year.
        /// </summary>
        /// <returns>true if the year represented by this instance is a leap year; otherwise, false.</returns>
        public bool IsLeapYear()
        {
            return _persianCalendar.IsLeapYear(Year);
        }

        /// <summary>
        /// Returns the number of days in the year represented by this instance.
        /// </summary>
        public int DaysInYear
        {
            get { return _persianCalendar.GetDaysInYear(Year); }
        }

        /// <summary>
        /// Returns the number of days in the month represented by this instance.
        /// </summary>
        public int DaysInMonth
        {
            get { return _persianCalendar.GetDaysInMonth(Year, Month); }
        }

        /// <summary>
        /// Returns the week of the year that includes the date represented by this instance.
        /// </summary>
        /// <param name="rule">An enumeration value that defines a calendar week.</param>
        /// <returns>A positive integer that represents the week of the year that includes the date represented by this instance.</returns>
        public int GetWeekOfYear(CalendarWeekRule rule)
        {
            return _persianCalendar.GetWeekOfYear(_dateTime, rule, System.DayOfWeek.Saturday);
        }

        /// <summary>
        /// Gets the day of the year represented by this instance.
        /// </summary>
        public int DayOfYear
        {
            get { return _persianCalendar.GetDayOfYear(_dateTime); }
        }

        /// <summary>
        /// Gets the day of the week represented by this instance.
        /// </summary>
        public int DayOfWeek
        {
            get { return ((int)_dateTime.DayOfWeek + 1) % 7; }
        }

        /// <summary>
        /// Gets the name of the day of the week represented by this instance.
        /// </summary>
        public string DayName
        {
            get { return _dayNames[DayOfWeek]; }
        }

        /// <summary>
        /// Gets the name of the month represented by this instance.
        /// </summary>
        public string MonthName
        {
            get { return _monthNames[Month - 1]; }
        }

        /// <summary>
        /// Gets the date component of this instance.
        /// </summary>
        public PersianDateTime Date
        {
            get { return new PersianDateTime(_dateTime.Date); }
        }

        /// <summary>
        /// Gets the first day of the year represented by this instance.
        /// </summary>
        public PersianDateTime FirstDayOfYear
        {
            get { return AddDays(-DayOfYear + 1).Date; }
        }

        /// <summary>
        /// Gets the last day of the year represented by this instance.
        /// </summary>
        public PersianDateTime LastDayOfYear
        {
            get { return AddDays(DaysInYear - DayOfYear).Date; }
        }

        /// <summary>
        /// Gets the first day of the month represented by this instance.
        /// </summary>
        public PersianDateTime FirstDayOfMonth
        {
            get { return AddDays(-Day + 1).Date; }
        }

        /// <summary>
        /// Gets the last day of the month represented by this instance.
        /// </summary>
        public PersianDateTime LastDayOfMonth
        {
            get { return AddDays(DaysInMonth - Day).Date; }
        }

        /// <summary>
        /// Gets the first day of the week represented by this instance.
        /// </summary>
        public PersianDateTime FirstDayOfWeek
        {
            get { return AddDays(-DayOfWeek).Date; }
        }

        /// <summary>
        /// Gets the last day of the week represented by this instance.
        /// </summary>
        public PersianDateTime LastDayOfWeek
        {
            get { return AddDays(6 - DayOfWeek).Date; }
        }

        /// <summary>
        /// Returns a new PersianDateTime that adds the specified number of seconds to the value of this instance.
        /// </summary>
        /// <param name="value">A number of whole and fractional seconds. The value parameter can be negative or positive.</param>
        /// <returns>An object whose value is the sum of the date and time represented by this instance and the number of seconds represented by value.</returns>
        public PersianDateTime AddSeconds(double value)
        {
            return new PersianDateTime(_dateTime.AddSeconds(value));
        }

        /// <summary>
        /// Returns a new PersianDateTime that adds the specified number of minutes to the value of this instance.
        /// </summary>
        /// <param name="value">A number of whole and fractional minutes. The value parameter can be negative or positive.</param>
        /// <returns>An object whose value is the sum of the date and time represented by this instance and the number of minutes represented by value.</returns>
        public PersianDateTime AddMinutes(double value)
        {
            return new PersianDateTime(_dateTime.AddMinutes(value));
        }

        /// <summary>
        /// Returns a new PersianDateTime that adds the specified number of hours to the value of this instance.
        /// </summary>
        /// <param name="value">A number of whole and fractional hours. The value parameter can be negative or positive.</param>
        /// <returns>An object whose value is the sum of the date and time represented by this instance and the number of hours represented by value.</returns>
        public PersianDateTime AddHours(double value)
        {
            return new PersianDateTime(_dateTime.AddHours(value));
        }

        /// <summary>
        /// Returns a new PersianDateTime that adds the specified number of years to the value of this instance.
        /// </summary>
        /// <param name="value">A number of years. The value parameter can be negative or positive.</param>
        /// <returns>An object whose value is the sum of the date and time represented by this instance and the number of years represented by value.</returns>
        public PersianDateTime AddYears(int value)
        {
            var newYear = Year + value;

            var daysInNewMonth = PersianDateTime.GetDaysInMonth(newYear, Month);
            var newDay = daysInNewMonth < Day ? daysInNewMonth : Day;

            return new PersianDateTime(Year + value, Month, Day).Add(TimeOfDay);
        }

        /// <summary>
        /// Returns a new PersianDateTime that adds the specified number of months to the value of this instance.
        /// </summary>
        /// <param name="value">A number of months. The months parameter can be negative or positive.</param>
        /// <returns>An object whose value is the sum of the date and time represented by this instance and months.</returns>
        public PersianDateTime AddMonths(int value)
        {
            var months = Month + value;

            var newYear = Year + (months > 0 ? (months - 1) / 12 : months / 12 - 1);
            var newMonth = months > 0 ? (months - 1) % 12 + 1 : months % 12 + 12;

            var daysInNewMonth = PersianDateTime.GetDaysInMonth(newYear, newMonth);
            var newDay = daysInNewMonth < Day ? daysInNewMonth : Day;

            return new PersianDateTime(newYear, newMonth, newDay).Add(TimeOfDay);
        }

        /// <summary>
        /// Returns a new PersianDateTime that adds the specified number of days to the value of this instance.
        /// </summary>
        /// <param name="value">A number of whole and fractional days. The value parameter can be negative or positive.</param>
        /// <returns>An object whose value is the sum of the date and time represented by this instance and the number of days represented by value.</returns>
        public PersianDateTime AddDays(double value)
        {
            return new PersianDateTime(_dateTime.AddDays(value));
        }

        /// <summary>
        /// Returns a new PersianDateTime that adds the value of the specified System.TimeSpan to the value of this instance.
        /// </summary>
        /// <param name="value">A positive or negative time interval.</param>
        /// <returns>An object whose value is the sum of the date and time represented by this instance and the time interval represented by value.</returns>
        public PersianDateTime Add(TimeSpan value)
        {
            return new PersianDateTime(_dateTime.Add(value));
        }

        /// <summary>
        /// Returns a System.DateTime object that is set to the date and time represented by this instance.
        /// </summary>
        /// <returns>A System.DateTime object that is set to the date and time represented by this instance</returns>
        public DateTime ToDateTime()
        {
            return _dateTime;
        }

        /// <summary>
        /// Converts the date represented by this instance to an equivalent 32-bit signed integer.
        /// </summary>
        /// <returns>n 32-bit signed integer equivalent to the date represented by this instance.</returns>
        public int ToInt()
        {
            return int.Parse(Year.ToString() + Month.ToString().PadLeft(2, '0') + Day.ToString().PadLeft(2, '0'));
        }

        /// <summary>
        /// Converts the value of the current PersianDateTime object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the value of the current PersianDateTime object.</returns>
        public override string ToString()
        {
            return ToString(PersianDateTimeFormat.DateTime);
        }

        /// <summary>
        /// Converts the value of the current PersianDateTime object to its equivalent string representation using the specified format.
        /// </summary>
        /// <param name="format">A custom date and time format string.</param>
        /// <returns>A string representation of value of the current PersianDateTime object as specified by format.</returns>
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

        /// <summary>
        /// Converts the value of the current PersianDateTime object to its equivalent string representation using the specified format.
        /// </summary>
        /// <param name="format">A persian date and time format string.</param>
        /// <returns>A string representation of value of the current PersianDateTime object as specified by format.</returns>
        public string ToString(PersianDateTimeFormat format)
        {
            switch (format)
            {
                case PersianDateTimeFormat.Date:
                    return Year.ToString() + "/" + Month.ToString().PadLeft(2, '0') + "/" + Day.ToString().PadLeft(2, '0');

                case PersianDateTimeFormat.DateTime:
                    return ToString(PersianDateTimeFormat.Date) + " " + TimeOfDay.ToHHMMSS();

                case PersianDateTimeFormat.DateShortTime:
                    return ToString(PersianDateTimeFormat.Date) + " " + TimeOfDay.ToHHMM();

                case PersianDateTimeFormat.LongDate:
                    return DayName + " " + Day + " " + MonthName;

                case PersianDateTimeFormat.LongDateFullTime:
                    return DayName + " " + Day + " " + MonthName + " ���� " + TimeOfDay.ToHHMMSS();

                case PersianDateTimeFormat.LongDateLongTime:
                    return DayName + " " + Day + " " + MonthName + " ���� " + TimeOfDay.ToHHMM();

                case PersianDateTimeFormat.ShortDateShortTime:
                    return Day.ToString() + " " + MonthName + " " + TimeOfDay.ToHHMM();

                case PersianDateTimeFormat.FullDate:
                    return DayName + " " + Day + " " + MonthName + " " + Year;

                case PersianDateTimeFormat.FullDateLongTime:
                    return DayName + " " + Day + " " + MonthName + " " + Year + " ���� " + TimeOfDay.ToHHMM();

                case PersianDateTimeFormat.FullDateFullTime:
                    return DayName + " " + Day + " " + MonthName + " " + Year + " ���� " + TimeOfDay.ToHHMMSS();

                default:
                    throw new NotImplementedException(format.ToString());
            }
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return _dateTime.GetHashCode();
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="value">An object to compare to this instance.</param>
        /// <returns>true if value is an instance of PersianDateTime and equals the value of this instance; otherwise, false.</returns>
        public override bool Equals(object value)
        {
            return Equals(value as PersianDateTime);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to the specified PersianDateTime instance.
        /// </summary>
        /// <param name="value">A PersianDateTime instance to compare to this instance.</param>
        /// <returns>true if the value parameter equals the value of this instance; otherwise, false.</returns>
        public bool Equals(PersianDateTime value)
        {
            if (object.ReferenceEquals(value, null))
            {
                return false;
            }
            return _dateTime.Equals(value._dateTime);
        }


        /// <summary>
        /// ��Ґ������ �?��� ���� ����� �� ���?� �����? �� ����?
        /// </summary>
        /// <param name="dateTime">���?� ����? �� �?���?</param>
        /// <returns>��Ґ������ �?��� ���� ����� �� ���?� �����? �� ����? ����� ?� ��?�� ���</returns>
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
            {
                return ts.Seconds == 1 ? "���� �? ���" : ts.Seconds + " ���?� ���";
            }
            if (delta < 2 * MINUTE)
            {
                return "?� ��?�� ���";
            }
            if (delta < 45 * MINUTE)
            {
                return ts.Minutes + " ��?�� ���";
            }
            if (delta < 90 * MINUTE)
            {
                return "?� ���� ���";
            }
            if (delta < 24 * HOUR)
            {
                return ts.Hours + " ���� ���";
            }
            if (delta < 48 * HOUR)
            {
                return "�?���";
            }
            if (delta < 30 * DAY)
            {
                return ts.Days + " ��� ���";
            }
            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "?� ��� ���" : months + " ��� ���";
            }
            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "?� ��� ���" : years + " ��� ���";
        }
    }

    #region Enums

    /// <summary>
    /// ���� ���?� �?�� ���? ���?� ���?
    /// </summary>
    public enum PersianDateTimeFormat
    {
        Date = 0,
        DateTime = 1,
        LongDate = 2,
        LongDateLongTime = 3,
        FullDate = 4,
        FullDateLongTime = 5,
        FullDateFullTime = 6,
        DateShortTime = 7,
        ShortDateShortTime = 8,
        LongDateFullTime = 9
    }

    /// <summary>
    ///PersianDateTime.Now. ����� �� ���?� ���?� ���? ��  
    /// </summary>
    public enum PersianDateTimeMode
    {
        /// <summary>
        /// ������� �� ��?� ��� ���?
        /// </summary>
        System,
        /// <summary>
        /// ������� �� ��?� ��� ���?
        /// </summary>
        PersianTimeZoneInfo,
        /// <summary>
        ///������� �� Utc
        /// </summary>
        UtcOffset
    }

    #endregion

    /// <summary>
    /// ����? ���? ���?� ��?� ���� �� ��� ��? �?��
    /// </summary>
    public static class TimeSpanExtension
    {
        /// <summary>
        /// ��� �����? ���?� ��?� ���� �� ���
        /// </summary>
        /// <param name="time">��?� ����?</param>
        /// <returns>���</returns>
        public static int ToInteger(this TimeSpan time)
        {
            return int.Parse(time.Hours.ToString() + time.Minutes.ToString().PadLeft(2, '0') + time.Seconds.ToString().PadLeft(2, '0'));
        }

        /// <summary>
        /// ��� �����? ���?� ��?� ���� �� ���
        /// </summary>
        /// <param name="time">��?� ����?</param>
        /// <returns>���</returns>
        public static short ToShort(this TimeSpan time)
        {
            return short.Parse(time.Hours.ToString() + time.Minutes.ToString().PadLeft(2, '0'));
        }

        /// <summary>
        /// ��� �����? ���?� ��?� ���� �� ����?�
        /// </summary>
        /// <param name="time">��?� ����?</param>
        /// <returns> hh:mm ����?� �� ���� </returns>
        public static string ToHHMM(this TimeSpan time)
        {
            return time.ToString("hh\\:mm");
        }

        /// <summary>
        /// ��� �����? ���?� ��?� ���� �� ����?�
        /// </summary>
        /// <param name="time">��?� ����?</param>
        /// <returns> hh:mm:ss ����?� �� ���� </returns>
        public static string ToHHMMSS(this TimeSpan time)
        {
            return time.ToString("hh\\:mm\\:ss");
        }
    }
}
