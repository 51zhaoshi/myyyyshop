namespace Maticsoft.TaoBao.Util
{
    using ICSharpCode.SharpZipLib.Zip;
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    public abstract class AtsUtils
    {
        private const string CTYPE_OCTET = "application/octet-stream";
        private static Regex regex = new Regex("attachment;filename=\"([\\w\\-]+)\"", RegexOptions.Compiled);

        protected AtsUtils()
        {
        }

        public static bool CheckMd5sum(string fileName, string checkCode)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(stream);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < buffer.Length; i++)
                {
                    builder.Append(buffer[i].ToString("x2"));
                }
                return builder.ToString().Equals(checkCode);
            }
        }

        public static string Download(string url, string destDir)
        {
            string path = null;
            try
            {
                WebUtils utils = new WebUtils();
                HttpWebResponse rsp = (HttpWebResponse) utils.GetWebRequest(url, "GET").GetResponse();
                if ("application/octet-stream".Equals(rsp.ContentType))
                {
                    path = Path.Combine(destDir, GetFileName(rsp.Headers["Content-Disposition"]));
                    using (Stream stream = rsp.GetResponseStream())
                    {
                        int count = 0;
                        byte[] buffer = new byte[0x2000];
                        using (FileStream stream2 = new FileStream(path, FileMode.OpenOrCreate))
                        {
                            while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                stream2.Write(buffer, 0, count);
                            }
                        }
                        return path;
                    }
                }
                throw new TopException(utils.GetResponseAsString(rsp, Encoding.UTF8));
            }
            catch (WebException exception)
            {
                throw new TopException("isv.file-already-download", exception.Message);
            }
            return path;
        }

        private static string GetFileName(string contentDisposition)
        {
            Match match = regex.Match(contentDisposition);
            if (!match.Success)
            {
                throw new TopException("Invalid response header format!");
            }
            return match.Groups[1].ToString();
        }

        public static string Ungzip(string gzipFile, string destDir)
        {
            string path = Path.Combine(destDir, Path.GetFileName(gzipFile));
            using (Stream stream = File.Create(path))
            {
                using (Stream stream2 = new GZipStream(File.Open(gzipFile, FileMode.Open), CompressionMode.Decompress))
                {
                    int count = 0;
                    byte[] buffer = new byte[0x2000];
                    while ((count = stream2.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        stream.Write(buffer, 0, count);
                    }
                }
            }
            return path;
        }

        public static List<string> Unzip(string zipFile, string destDir)
        {
            List<string> list = new List<string>();
            using (ZipInputStream stream = new ZipInputStream(File.OpenRead(zipFile)))
            {
                ZipEntry entry;
                while ((entry = stream.GetNextEntry()) != null)
                {
                    if (entry.IsDirectory)
                    {
                        Directory.CreateDirectory(Path.Combine(destDir, entry.Name));
                    }
                    else
                    {
                        string path = Path.Combine(destDir, entry.Name);
                        using (FileStream stream2 = File.Create(path))
                        {
                            int count = 0;
                            byte[] buffer = new byte[0x2000];
                            while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                stream2.Write(buffer, 0, count);
                            }
                        }
                        list.Add(path);
                    }
                }
            }
            return list;
        }
    }
}

