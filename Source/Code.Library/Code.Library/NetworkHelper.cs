using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Code.Library
{
    public static class NetworkHelper
    {
        /// <summary>
        /// function returns the IP address of the current system
        /// Author : Jithin
        /// Date : 17 July 2015
        /// </summary>
        /// <returns></returns>
        public static string GetUser_IP()
        {
            string VisitorsIPAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }
            return VisitorsIPAddr;
        }
    }
}
