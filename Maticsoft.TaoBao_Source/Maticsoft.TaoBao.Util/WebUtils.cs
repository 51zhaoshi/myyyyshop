namespace Maticsoft.TaoBao.Util
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Web;

    public sealed class WebUtils
    {
        private int _timeout = 0x186a0;

        public string BuildGetUrl(string url, IDictionary<string, string> parameters)
        {
            if ((parameters != null) && (parameters.Count > 0))
            {
                if (url.Contains("?"))
                {
                    url = url + "&" + BuildQuery(parameters);
                    return url;
                }
                url = url + "?" + BuildQuery(parameters);
            }
            return url;
        }

        public static string BuildQuery(IDictionary<string, string> parameters)
        {
            StringBuilder builder = new StringBuilder();
            bool flag = false;
            IEnumerator<KeyValuePair<string, string>> enumerator = parameters.GetEnumerator();
            while (enumerator.MoveNext())
            {
                KeyValuePair<string, string> current = enumerator.Current;
                string key = current.Key;
                KeyValuePair<string, string> pair2 = enumerator.Current;
                string str2 = pair2.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(str2))
                {
                    if (flag)
                    {
                        builder.Append("&");
                    }
                    builder.Append(key);
                    builder.Append("=");
                    builder.Append(HttpUtility.UrlEncode(str2, Encoding.UTF8));
                    flag = true;
                }
            }
            return builder.ToString();
        }

        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public string DoGet(string url, IDictionary<string, string> parameters)
        {
            if ((parameters != null) && (parameters.Count > 0))
            {
                if (url.Contains("?"))
                {
                    url = url + "&" + BuildQuery(parameters);
                }
                else
                {
                    url = url + "?" + BuildQuery(parameters);
                }
            }
            HttpWebRequest webRequest = this.GetWebRequest(url, "GET");
            webRequest.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            HttpWebResponse rsp = (HttpWebResponse) webRequest.GetResponse();
            Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
            return this.GetResponseAsString(rsp, encoding);
        }

        public string DoPost(string url, IDictionary<string, string> parameters)
        {
            HttpWebRequest webRequest = this.GetWebRequest(url, "POST");
            webRequest.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            byte[] bytes = Encoding.UTF8.GetBytes(BuildQuery(parameters));
            Stream requestStream = webRequest.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse rsp = (HttpWebResponse) webRequest.GetResponse();
            Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
            return this.GetResponseAsString(rsp, encoding);
        }

        public string DoPost(string url, IDictionary<string, string> textParams, IDictionary<string, FileItem> fileParams)
        {
            if ((fileParams == null) || (fileParams.Count == 0))
            {
                return this.DoPost(url, textParams);
            }
            string str = DateTime.Now.Ticks.ToString("X");
            HttpWebRequest webRequest = this.GetWebRequest(url, "POST");
            webRequest.ContentType = "multipart/form-data;charset=utf-8;boundary=" + str;
            Stream requestStream = webRequest.GetRequestStream();
            byte[] bytes = Encoding.UTF8.GetBytes("\r\n--" + str + "\r\n");
            byte[] buffer = Encoding.UTF8.GetBytes("\r\n--" + str + "--\r\n");
            string format = "Content-Disposition:form-data;name=\"{0}\"\r\nContent-Type:text/plain\r\n\r\n{1}";
            IEnumerator<KeyValuePair<string, string>> enumerator = textParams.GetEnumerator();
            while (enumerator.MoveNext())
            {
                KeyValuePair<string, string> current = enumerator.Current;
                KeyValuePair<string, string> pair2 = enumerator.Current;
                string s = string.Format(format, current.Key, pair2.Value);
                byte[] buffer3 = Encoding.UTF8.GetBytes(s);
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Write(buffer3, 0, buffer3.Length);
            }
            string str4 = "Content-Disposition:form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type:{2}\r\n\r\n";
            IEnumerator<KeyValuePair<string, FileItem>> enumerator2 = fileParams.GetEnumerator();
            while (enumerator2.MoveNext())
            {
                KeyValuePair<string, FileItem> pair3 = enumerator2.Current;
                string key = pair3.Key;
                KeyValuePair<string, FileItem> pair4 = enumerator2.Current;
                FileItem item = pair4.Value;
                string str6 = string.Format(str4, key, item.GetFileName(), item.GetMimeType());
                byte[] buffer4 = Encoding.UTF8.GetBytes(str6);
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Write(buffer4, 0, buffer4.Length);
                byte[] content = item.GetContent();
                requestStream.Write(content, 0, content.Length);
            }
            requestStream.Write(buffer, 0, buffer.Length);
            requestStream.Close();
            HttpWebResponse rsp = (HttpWebResponse) webRequest.GetResponse();
            Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
            return this.GetResponseAsString(rsp, encoding);
        }

        public string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            Stream responseStream = null;
            StreamReader reader = null;
            string str;
            try
            {
                responseStream = rsp.GetResponseStream();
                reader = new StreamReader(responseStream, encoding);
                str = reader.ReadToEnd();
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (rsp != null)
                {
                    rsp.Close();
                }
            }
            return str;
        }

        public HttpWebRequest GetWebRequest(string url, string method)
        {
            HttpWebRequest request = null;
            if (url.Contains("https"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);
                request = (HttpWebRequest) WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                request = (HttpWebRequest) WebRequest.Create(url);
            }
            request.ServicePoint.Expect100Continue = false;
            request.Method = method;
            request.KeepAlive = true;
            request.UserAgent = "Top4Net";
            request.Timeout = this._timeout;
            return request;
        }

        public int Timeout
        {
            get
            {
                return this._timeout;
            }
            set
            {
                this._timeout = value;
            }
        }
    }
}

