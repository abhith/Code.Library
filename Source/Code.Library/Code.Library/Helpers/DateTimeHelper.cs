using System;
using System.Collections.Generic;
using System.Globalization;

namespace Code.Library
{
    public static class DateTimeHelper
    {
        #region Extension Menthods

        public static int DaysInMonth(this DateTime value)
        {
            return DateTime.DaysInMonth(value.Year, value.Month);
        }

        public static DateTime FirstDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        /// <summary>
        /// The first day of year.
        /// </summary>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        public static DateTime FirstDayOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        public static DateTime FromUnixTime(this long value)
        {
            var unix = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return unix.AddMilliseconds(value);
        }

        /// <summary>
        /// The Arabian Standard Time
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetArabianStandardTime(this DateTime date)
        {
            var tst = TimeZoneInfo.FindSystemTimeZoneById("Arabian Standard Time");
            return TimeZoneInfo.ConvertTime(date, TimeZoneInfo.Local, tst);
        }

        /// <summary>
        /// Gives each  day between start and end date
        /// </summary>
        /// <param name="startDate">
        /// The start date
        /// </param>
        /// <param name="endDate">
        /// The end date
        /// </param>
        /// <returns>
        /// <see cref="IEnumerable{DateTime}"/>
        /// </returns>
        public static IEnumerable<DateTime> GetEachDay(this DateTime startDate, DateTime endDate)
        {
            for (var day = startDate.Date; day.Date <= endDate.Date; day = day.AddDays(1))
            {
                yield return day;
            }
        }

        public static DateTime Iso8601ToDateTime(this string iso8601text)
        {
            return DateTime.Parse(iso8601text, null, DateTimeStyles.RoundtripKind);
        }

        public static DateTime LastDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.DaysInMonth());
        }

        public static string NameOfMonth(this DateTime value)
        {
            return value.ToString("MMM", CultureInfo.InvariantCulture);
        }

        public static string ToIso8601(this DateTime date)
        {
            return date.ToString("s", CultureInfo.InvariantCulture);
        }

        public static long ToUnixTime(this DateTime value)
        {
            var unix = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            if (value < unix)
                throw new ArgumentException("Specified value is lower than the UNIX time.");

            return (long)value.Subtract(unix).TotalMilliseconds;
        }

        #endregion Extension Menthods
    }
}