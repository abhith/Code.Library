using System;
using System.Globalization;

namespace Code.Library
{
    public static class DateTimeHelper
    {
        public static DateTime FirstDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        public static int DaysInMonth(this DateTime value)
        {
            return DateTime.DaysInMonth(value.Year, value.Month);
        }

        public static DateTime LastDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.DaysInMonth());
        }

        public static string NameOfMonth(this DateTime value)
        {
            return value.ToString("MMM", CultureInfo.InvariantCulture);
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
    }
}