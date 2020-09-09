using System.IO;

namespace Code.Library.Extensions
{
    /// <summary>
    /// All file system/directory related extensions.
    /// </summary>
    public static class FileSystemExtensions
    {
        /// <summary>
        /// delete all the files and folders in the specified directory.
        /// </summary>
        /// <param name="directory">Directory which need to clean.</param>
        public static void Clean(this DirectoryInfo directory)
        {
            foreach (var file in directory.GetFiles()) file.Delete();
            foreach (var subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }
    }
}