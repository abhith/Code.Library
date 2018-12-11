namespace Code.Library
{
    using System;
    using System.Globalization;

    /// <summary>
    /// All Date Time extensions.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Convert timespan to 12H format string
        /// </summary>
        /// <param name="timeSpan">Input timespan.</param>
        /// <returns>Tweleve hour format string.</returns>
        public static string ConvertTo12H(this TimeSpan timeSpan)
        {
            var dateTime = DateTime.MinValue.Add(timeSpan);
            var cultureInfo = CultureInfo.InvariantCulture;

            return dateTime.ToString("hh:mm tt", cultureInfo);
        }
    }
}