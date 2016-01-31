// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileHelper.cs" company="Open Source">
//   File Helper
// </copyright>
// <summary>
//   The file helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Code.Library
{
    using System.IO;

    /// <summary>
    /// The file helper.
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// The delete file.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        public static void DeleteFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        /// <summary>
        /// The create directory.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        public static void CreateDirectory(string path)
        {
            var exists = Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(path));
            if (!exists)
            {
                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(path));
            }
        }
    }
}