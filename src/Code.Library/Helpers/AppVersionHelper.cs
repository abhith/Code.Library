namespace Code.Library
{
    using System;
    using System.IO;

    /// <summary>
    /// Central point for application version.
    /// </summary>
    public class AppVersionHelper
    {
        /// <summary>
        /// Gets release (last build) date of the application.
        /// It's shown in the web page.
        /// </summary>
        public static DateTime ReleaseDate
        {
            // TODO: Make it generic
            get { return new FileInfo(typeof(AppVersionHelper).Assembly.Location).LastWriteTime; }
        }
    }
}