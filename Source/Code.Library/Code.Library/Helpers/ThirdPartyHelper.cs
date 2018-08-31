using System;

namespace Code.Library
{
    public static class ThirdPartyHelper
    {
        /// <summary>
        /// Gets Anchor link for Add to Google Calendar
        /// </summary>
        /// <param name="title"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="detailsUrl"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static string GetAddToGoogleCalendarUrl(string title, DateTime startDate, DateTime endDate, string detailsUrl, string location)
        {
            return string.Format("https://www.google.com/calendar/render?action=TEMPLATE&text={0}&dates={1}Z/{2}Z&details=For+details,+link+here:{3}&location={4}&sf=true&output=xml", title, startDate.ToString("yyyyMMdd'T'HHmmss"), endDate.ToString("yyyyMMdd'T'HHmmss"), detailsUrl, location);
        }

        /// <summary>
        /// Gets image url for the qr code
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetQrCodeImageUrl(string url)
        {
            return string.Format("http://chart.apis.google.com/chart?cht=qr&chs=400x400&chl={0}", url);
        }
    }
}