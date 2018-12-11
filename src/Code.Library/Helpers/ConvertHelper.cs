using System;
using System.Globalization;

namespace Code.Library
{
    public static class ConvertHelper
    {
        #region Extension Methods

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

        /// <summary>
        /// string to nullable int
        /// </summary>
        /// <param name="s">string</param>
        /// <returns></returns>
        public static int? ToNullableInt32(this string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }

        /// <summary>
        /// string to nullable bool
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool? ToNullableBool(this string s)
        {
            bool i;
            if (bool.TryParse(s, out i)) return i;
            return null;
        }

        #endregion Extension Methods
    }
}