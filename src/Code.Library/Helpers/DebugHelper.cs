using System;
using System.Web;

namespace Code.Library
{
    public static class DebugHelper
    {
        /// <summary>
        /// Log message to the response header
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public static void LogMessage(string title,string message)
        {
            HttpContext.Current.Response.Write(string.Format("| {0} | {1} - {2}",DateTime.Now,title,message));
        }
    }
}
