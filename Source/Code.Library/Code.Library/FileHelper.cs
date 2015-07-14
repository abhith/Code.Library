using System.IO;

namespace Code.Library
{
    public static class FileHelper
    {
        /// <summary>
        /// Delete file
        /// Author : Abhith
        /// Date : 14 July 2015
        /// Reference : Sysberries
        /// </summary>
        /// <param name="fileName"></param>
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
    }
}
