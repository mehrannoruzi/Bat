using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace Bat.Sql
{
    public partial class DateTimeConvertor
    {
        /// <summary>
        /// Add Time String To Miladi Date
        /// </summary>
        /// <param name="MiladiDate">Miladi DateTime</param>
        /// <param name="Time">Time To Add Miladi DateTime</param>
        /// <returns>Miladi DateTime</returns>
        [SqlFunction]
        public static SqlDateTime AddTime(DateTime MiladiDate, string Time)
        {
            var times = Time.Split(':');
            if (times.Length == 3)
            {
                var seconds = times[2].Split('.');

                var result = MiladiDate.AddHours(int.Parse(times[0])).AddMinutes(int.Parse(times[1]));
                if (seconds.Length == 2)
                {
                    result = result.AddSeconds(double.Parse(seconds[0]))
                                .AddMilliseconds(double.Parse(seconds[1]));
                }
                else if (seconds.Length == 1)
                {
                    result = result.AddSeconds(double.Parse(seconds[0]));
                }
                return result;
            }
            else
                return MiladiDate;
        }


        /// <summary>
        /// Convert Shamsi Date String to Miladi DateTime
        /// </summary>
        /// <param name="ShamsiDate">String Shamsi Date</param>
        /// <returns>Miladi DateTime</returns>
        [SqlFunction]
        public static SqlDateTime ToDateTimeMiladi(string ShamsiDate)
        {
            return PersianDateTime.Parse(ShamsiDate).ToDateTime();
        }


        /// <summary>
        /// Convert Miladi DateTime to Shamsi Date
        /// </summary>
        /// <param name="MiladiDate">Miladi DateTime</param>
        /// <returns>Shamsi Date String</returns>
        [SqlFunction]
        public static SqlString ToDateTimeShamsi(DateTime MiladiDate)
        {
            return PersianDateTime.Parse(MiladiDate).ToString(PersianDateTimeFormat.Date);
        }


        /// <summary>
        /// Convert Miladi DateTime String to Shamsi Date
        /// </summary>
        /// <param name="MiladiDate">String Miladi DateTime</param>
        /// <returns>Shamsi Date String</returns>
        [SqlFunction]
        public static SqlString ToDateTimeShamsiFromString(string MiladiDate)
        {
            return PersianDateTime.Parse(DateTime.Parse(MiladiDate)).ToString();
        }

    }
}
