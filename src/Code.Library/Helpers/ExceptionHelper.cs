using System;
using System.Web;

namespace Code.Library
{
    public static class ExceptionHelper
    {
        /// <summary>
        /// Log exception to the response
        /// Author : Abhith
        /// Date : 14 July 2015
        /// Reference : Sysberries
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="page"></param>
        public static void LogException(Exception ex, string page)
        {
            HttpContext.Current.Response.Write(ex + " : " + page);
        }
    }
}
