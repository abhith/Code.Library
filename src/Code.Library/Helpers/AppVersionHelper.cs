using System;

namespace Code.Library
{
    using System.IO;

    /// <summary>
    /// Central point for application version.
    /// </summary>
    public class AppVersionHelper
    {
        #region Public Fields

        /// <summary>
        /// Gets current version of the application.
        /// All project's assembly versions are changed when this value is changed.
        /// It's also shown in the web page.
        /// </summary>
        public const string Version = "1.1.0";

        #endregion Public Fields

        #region Public Properties

        /// <summary>
        /// Gets release (last build) date of the application.
        /// It's shown in the web page.
        /// </summary>
        public static DateTime ReleaseDate
        {
            get { return new FileInfo(typeof(AppVersionHelper).Assembly.Location).LastWriteTime; }
        }

        #endregion Public Properties
    }
}