using System;
using System.Globalization;

namespace Code.Library
{
    public static class ConvertHelper
    {
        /// <summary>
        /// Convert timespan to 12H format string
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static string ConvertTo12H(this TimeSpan timeSpan)
        {
            var dateTime = DateTime.MinValue.Add(timeSpan);
            var cultureInfo = CultureInfo.InvariantCulture;

            // optional
            //CultureInfo cultureInfo = new CultureInfo(CultureInfo.CurrentCulture.Name);
            //cultureInfo.DateTimeFormat.PMDesignator = "PM";

            return dateTime.ToString("hh:mm tt", cultureInfo);
        }
    }
}