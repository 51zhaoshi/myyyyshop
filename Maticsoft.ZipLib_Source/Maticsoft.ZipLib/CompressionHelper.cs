namespace Maticsoft.ZipLib
{
    using Maticsoft.ZipLib.BZip2;
    using Maticsoft.ZipLib.GZip;
    using Maticsoft.ZipLib.Zip;
    using System;
    using System.IO;
    using System.Text;

    public class CompressionHelper
    {
        public static CompressionType CompressionProvider = CompressionType.GZip;

        public static byte[] Compress(byte[] bytesToCompress)
        {
            MemoryStream inputStream = new MemoryStream();
            Stream stream2 = OutputStream(inputStream);
            stream2.Write(bytesToCompress, 0, bytesToCompress.Length);
            stream2.Close();
            return inputStream.ToArray();
        }

        public static string Compress(string stringToCompress)
        {
            return Convert.ToBase64String(CompressToByte(stringToCompress));
        }

        public static byte[] CompressToByte(string stringToCompress)
        {
            return Compress(Encoding.Unicode.GetBytes(stringToCompress));
        }

        public string DeCompress(string stringToDecompress)
        {
            string str = string.Empty;
            if (stringToDecompress == null)
            {
                throw new ArgumentNullException("stringToDecompress", "You tried to use an empty string");
            }
            try
            {
                byte[] bytesToDecompress = Convert.FromBase64String(stringToDecompress.Trim());
                str = Encoding.Unicode.GetString(DeCompress(bytesToDecompress));
            }
            catch (NullReferenceException exception)
            {
                return exception.Message;
            }
            return str;
        }

        public static byte[] DeCompress(byte[] bytesToDecompress)
        {
            byte[] buffer = new byte[0x1000];
            Stream stream = InputStream(new MemoryStream(bytesToDecompress));
            MemoryStream stream2 = new MemoryStream();
            while (true)
            {
                int count = stream.Read(buffer, 0, buffer.Length);
                if (count > 0)
                {
                    stream2.Write(buffer, 0, count);
                }
                else
                {
                    stream.Close();
                    byte[] buffer2 = stream2.ToArray();
                    stream2.Close();
                    return buffer2;
                }
            }
        }

        private static Stream InputStream(Stream inputStream)
        {
            switch (CompressionProvider)
            {
                case CompressionType.GZip:
                    return new GZipInputStream(inputStream);

                case CompressionType.BZip2:
                    return new BZip2InputStream(inputStream);

                case CompressionType.Zip:
                    return new ZipInputStream(inputStream);
            }
            return new GZipInputStream(inputStream);
        }

        private static Stream OutputStream(Stream inputStream)
        {
            switch (CompressionProvider)
            {
                case CompressionType.GZip:
                    return new GZipOutputStream(inputStream);

                case CompressionType.BZip2:
                    return new BZip2OutputStream(inputStream);

                case CompressionType.Zip:
                    return new ZipOutputStream(inputStream);
            }
            return new GZipOutputStream(inputStream);
        }
    }
}

