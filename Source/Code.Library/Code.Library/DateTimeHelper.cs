using System;
using System.Collections.Generic;
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

    }
}