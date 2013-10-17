namespace Maticsoft.Common
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;

    public class PageLoader
    {
        private static Encoding _encoding = Encoding.Default;
        private static readonly string strAgentInfo = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";

        public static string Download(string url)
        {
            return Download(url, strAgentInfo);
        }

        public static string Download(string url, string agentInfo)
        {
            string str;
            try
            {
                WebResponse response = getResponse(url, agentInfo);
                StreamReader reader = new StreamReader(response.GetResponseStream(), getEncoding(response));
                str = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str;
        }

        private static Encoding getEncoding(WebResponse response)
        {
            try
            {
                string contentType = response.ContentType;
                if (contentType == null)
                {
                    return _encoding;
                }
                string[] strArray = contentType.ToLower(CultureInfo.InvariantCulture).Split(new char[] { ';', '=', ' ' });
                bool flag = false;
                foreach (string str2 in strArray)
                {
                    if (str2 == "charset")
                    {
                        flag = true;
                    }
                    else if (flag)
                    {
                        return Encoding.GetEncoding(str2);
                    }
                }
            }
            catch (Exception exception)
            {
                if (((exception is ThreadAbortException) || (exception is StackOverflowException)) || (exception is OutOfMemoryException))
                {
                    throw;
                }
            }
            return _encoding;
        }

        private static WebResponse getResponse(string url, string agentInfo)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.UserAgent = agentInfo;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            return request.GetResponse();
        }
    }
}

