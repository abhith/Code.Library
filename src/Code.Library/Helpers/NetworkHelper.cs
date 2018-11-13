using System.Web;

namespace Code.Library
{
    public static class NetworkHelper
    {
        /// <summary>
        /// Returns the IP address of the current request.
        /// Author : Jithin
        /// Date : 17 July 2015
        /// </summary>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static string GetUserIPAddress()
        {
            var visitorsIpAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                visitorsIpAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else if (!string.IsNullOrEmpty(HttpContext.Current.Request.UserHostAddress))
            {
                visitorsIpAddr = HttpContext.Current.Request.UserHostAddress;
            }
            return visitorsIpAddr;
        }
    }
}