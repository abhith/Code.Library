// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileHelper.cs" company="Open Source">
//   Open Source
// </copyright>
// <summary>
//   The file helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Code.Library
{
    using System.IO;
    using System.Net;

    /// <summary>
    /// The file helper.
    /// </summary>
    public static class FileHelper
    {
        #region Extension Methods

        /// <summary>
        /// delete all the files and folders in the specified directory
        /// </summary>
        /// <param name="directory"></param>
        public static void Clean(this DirectoryInfo directory)
        {
            foreach (var file in directory.GetFiles()) file.Delete();
            foreach (var subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }

        #endregion Extension Methods

        #region Public Methods

        /// <summary>
        /// deletes the specified file
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
        /// Gets file via ftp.
        /// </summary>
        /// <param name="downloadTo">
        /// The download to.
        /// </param>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <param name="ftpAddress">
        /// The ftp address.
        /// </param>
        /// <param name="ftpUsername">
        /// The ftp username.
        /// </param>
        /// <param name="ftpPassword">
        /// The ftp password.
        /// </param>
        public static void GetFileViaFTP(string downloadTo, string filename, string ftpAddress, string ftpUsername, string ftpPassword)
        {
            var localPath = downloadTo;
            var fileName = filename;

            var requestFileDownload = (FtpWebRequest)WebRequest.Create(ftpAddress + fileName);
            requestFileDownload.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            requestFileDownload.Method = WebRequestMethods.Ftp.DownloadFile;

            var responseFileDownload = (FtpWebResponse)requestFileDownload.GetResponse();

            var responseStream = responseFileDownload.GetResponseStream();
            var writeStream = new FileStream(localPath + fileName, FileMode.Create);

            const int Length = 2048;
            var buffer = new byte[Length];
            if (responseStream != null)
            {
                var bytesRead = responseStream.Read(buffer, 0, Length);

                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                }

                responseStream.Close();
            }

            writeStream.Close();
        }

        #endregion Public Methods
    }
}