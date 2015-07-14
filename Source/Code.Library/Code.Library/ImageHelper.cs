using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Code.Library
{
    public static class ImageHelper
    {
        /// <summary>
        /// Create Thumbnail
        /// Author : Abhith
        /// Date : 14 July 2015
        /// Reference : Sysberries
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void CreateThumbnail(string imageUrl, int width, int height)
        {
            var saveDirectory = string.Concat(Path.GetDirectoryName(imageUrl), "\\Thumbnails");
            var newFileName = string.Concat(saveDirectory, "\\", Path.GetFileName(imageUrl));

            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            var originalImage = Image.FromFile(imageUrl);
            Image thumbnail = new Bitmap(width, height);

            using (var graphicsHandle = Graphics.FromImage(thumbnail))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.High;
                graphicsHandle.DrawImage(originalImage, 0, 0, width, height);
            }

            var ms = new MemoryStream();

            thumbnail.Save(ms, GetImageFormat(newFileName));

            using (var fileStream = new FileStream(newFileName, FileMode.OpenOrCreate))
            {
                var thumnailBytes = ms.GetBuffer();

                fileStream.Write(thumnailBytes, 0, thumnailBytes.Length);
            }
        }

        /// <summary>
        /// Get Image Format
        /// Author : Abhith
        /// Date : 14 July 2015
        /// Reference : Sysberries
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static ImageFormat GetImageFormat(string fileName)
        {
            var extension = Path.GetExtension(fileName);

            if (extension != null)
            {
                var ext = extension.ToLower();

                switch (ext)
                {
                    case ".jpg":
                    case ".jpeg":
                        return ImageFormat.Jpeg;

                    case ".gif":
                        return ImageFormat.Gif;

                    case ".bmp":
                        return ImageFormat.Bmp;
                }
            }

            return ImageFormat.Png;
        }
    }
}
