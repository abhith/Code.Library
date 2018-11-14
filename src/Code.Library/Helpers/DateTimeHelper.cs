using System;
using System.Collections.Generic;
using System.Globalization;

namespace Code.Library
{
    public static class DateTimeHelper
    {
        #region Extension Methods

        /// <summary>
        /// Returns the number of days in the month of specified date
        /// </summary>
        /// <param name="value">datetime</param>
        /// <returns></returns>
        public static int DaysInMonth(this DateTime value)
        {
            return DateTime.DaysInMonth(value.Year, value.Month);
        }

        /// <summary>
        /// Returns the first day in the month of specified date
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        /// <summary>
        /// Returns the first day in the year of the given date.
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

        /// <summary>
        /// Converts unix time to datetime
        /// </summary>
        /// <param name="value">unix value</param>
        /// <returns></returns>
        public static DateTime FromUnixTime(this long value)
        {
            var unix = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return unix.AddMilliseconds(value);
        }

        /// <summary>
        /// Returns the Arabian Standard Time
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetArabianStandardTime(this DateTime date)
        {
            var tst = TimeZoneInfo.FindSystemTimeZoneById("Arabian Standard Time");
            return TimeZoneInfo.ConvertTime(date, TimeZoneInfo.Local, tst);
        }

        /// <summary>
        /// Gives list of days between start and end date
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

        /// <summary>
        /// Converts ISO 8601 time to datetime
        /// </summary>
        /// <param name="iso8601text"></param>
        /// <returns></returns>
        public static DateTime Iso8601ToDateTime(this string iso8601text)
        {
            return DateTime.Parse(iso8601text, null, DateTimeStyles.RoundtripKind);
        }

        /// <summary>
        /// Returns the last day in the month of specified date
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.DaysInMonth());
        }

        /// <summary>
        /// Returns the name of month of the specified date
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string NameOfMonth(this DateTime value)
        {
            return value.ToString("MMM", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns the Iso 8601 format of the specified date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToIso8601(this DateTime date)
        {
            return date.ToString("s", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts datetime value to unix time
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ToUnixTime(this DateTime value)
        {
            var unix = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            if (value < unix)
                throw new ArgumentException("Specified value is lower than the UNIX time.");

            return (long)value.Subtract(unix).TotalMilliseconds;
        }

        #endregion Extension Methods
    }
}