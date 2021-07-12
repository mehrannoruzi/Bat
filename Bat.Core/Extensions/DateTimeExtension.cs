using System;

namespace Bat.Core
{
    public static class DateTimeExtension
    {
        public static DateTime ToDateTime(this string persianDateTime) => PersianDateTime.Parse(persianDateTime).ToDateTime();

        public static DateTime ToDateTime(this string persianDateTime, int hour, int minute) => PersianDateTime.Parse(persianDateTime).ToDateTime().AddHours(hour).AddMinutes(minute);

        public static PersianDateTime ToPersianDateTime(this DateTime date) => PersianDateTime.Parse(date);

        public static string ToPersianDate(this DateTime date) => PersianDateTime.Parse(date).ToString(PersianDateTimeFormat.Date);

        public static bool IsFuture(this DateTime date) => date.IsFuture(DateTime.Now);

        public static bool IsFuture(this DateTime date, DateTime from) => date.Date > from.Date;

        public static bool IsPast(this DateTime date) => date.IsPast(DateTime.Now);

        public static bool IsPast(this DateTime date, DateTime from) => date.Date < from.Date;
    }
}