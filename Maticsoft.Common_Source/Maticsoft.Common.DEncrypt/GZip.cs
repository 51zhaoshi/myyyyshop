namespace Maticsoft.Common.DEncrypt
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Text;

    public static class GZip
    {
        public static string DeflateCompress(string strSource)
        {
            if ((strSource == null) || (strSource.Length > 0x2000))
            {
                throw new ArgumentException("字符串为空或长度超过上限");
            }
            byte[] bytes = Encoding.UTF8.GetBytes(strSource);
            using (MemoryStream stream = new MemoryStream())
            {
                using (DeflateStream stream2 = new DeflateStream(stream, CompressionMode.Compress, true))
                {
                    stream2.Write(bytes, 0, bytes.Length);
                    stream2.Close();
                }
                byte[] inArray = stream.ToArray();
                stream.Close();
                return Convert.ToBase64String(inArray);
            }
        }

        public static string DeflateDecompress(string strSource)
        {
            string str;
            byte[] buffer = Convert.FromBase64String(strSource);
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(buffer, 0, buffer.Length);
                stream.Position = 0L;
                using (DeflateStream stream2 = new DeflateStream(stream, CompressionMode.Decompress))
                {
                    stream2.Flush();
                    int count = 0x4100;
                    byte[] buffer2 = new byte[count];
                    int num2 = stream2.Read(buffer2, 0, count);
                    stream2.Close();
                    str = Encoding.UTF8.GetString(buffer2, 0, num2);
                }
            }
            return str;
        }

        public static byte[] GZipCompress(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (GZipStream stream2 = new GZipStream(stream, CompressionMode.Compress))
                {
                    stream2.Write(data, 0, data.Length);
                    stream2.Close();
                }
                return stream.ToArray();
            }
        }

        public static byte[] GZipDecompress(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (GZipStream stream2 = new GZipStream(new MemoryStream(data), CompressionMode.Decompress))
                {
                    int num;
                    byte[] buffer = new byte[0xa000];
                    while ((num = stream2.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        stream.Write(buffer, 0, num);
                    }
                    stream2.Close();
                }
                return stream.ToArray();
            }
        }
    }
}

