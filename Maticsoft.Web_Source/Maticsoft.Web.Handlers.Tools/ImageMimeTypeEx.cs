namespace Maticsoft.Web.Handlers.Tools
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class ImageMimeTypeEx
    {
        public static string GetMimeType(this Image image)
        {
            return image.RawFormat.GetMimeType();
        }

        public static string GetMimeType(this ImageFormat imageFormat)
        {
            return ImageCodecInfo.GetImageEncoders().First<ImageCodecInfo>(codec => (codec.FormatID == imageFormat.Guid)).MimeType;
        }
    }
}

