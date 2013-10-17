namespace Maticsoft.TaoBao.Stream.Connect
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Stream;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;

    public class HttpClient
    {
        private Configuration conf;
        private ITopLogger log = new DefaultTopLogger();
        private IDictionary<string, string> parameters;

        public HttpClient(Configuration conf, IDictionary<string, string> parameters)
        {
            if ((conf == null) || (parameters == null))
            {
                throw new Exception("conf and params is must not null");
            }
            this.conf = conf;
            this.parameters = parameters;
            ServicePointManager.DefaultConnectionLimit = 0x80;
        }

        private HttpWebRequest GetConnection(string url, int connTimeout, int readTimeout)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            if (connTimeout > 0)
            {
                request.Timeout = connTimeout * 0x3e8;
            }
            if (readTimeout > 0)
            {
                request.ReadWriteTimeout = readTimeout * 0x3e8;
            }
            request.AllowAutoRedirect = false;
            request.ServicePoint.Expect100Continue = false;
            return request;
        }

        public HttpResponse Post()
        {
            int num2 = this.conf.GetHttpConnectRetryCount() + 1;
            for (int i = 1; i <= num2; i++)
            {
                try
                {
                    HttpWebRequest connection = null;
                    Stream requestStream = null;
                    try
                    {
                        connection = this.GetConnection(this.conf.GetConnectUrl(), this.conf.GetHttpConnectionTimeout(), this.conf.GetHttpReadTimeout());
                        connection.KeepAlive = true;
                        this.SetHeaders(connection, this.conf.GetRequestHeader());
                        connection.Method = "POST";
                        connection.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                        string s = WebUtils.BuildQuery(this.parameters);
                        byte[] bytes = Encoding.UTF8.GetBytes(s);
                        requestStream = connection.GetRequestStream();
                        requestStream.Write(bytes, 0, bytes.Length);
                        requestStream.Close();
                        HttpWebResponse response = (HttpWebResponse) connection.GetResponse();
                        HttpStatusCode statusCode = response.StatusCode;
                        if (HttpStatusCode.OK == statusCode)
                        {
                            this.log.Info("connect successful");
                            StringBuilder builder = new StringBuilder();
                            WebHeaderCollection headers = connection.Headers;
                            foreach (string str2 in headers.AllKeys)
                            {
                                foreach (string str3 in headers.GetValues(str2))
                                {
                                    if (str2 != null)
                                    {
                                        builder.Append(str2).Append("=").Append(str3);
                                    }
                                    else
                                    {
                                        builder.Append(str3);
                                    }
                                    builder.Append(";");
                                }
                                this.log.Info("Response: " + builder.ToString());
                            }
                            return new HttpResponse(connection);
                        }
                        if (HttpStatusCode.BadRequest == statusCode)
                        {
                            this.log.Info("Request param is invalid,errmsg is:" + connection.Headers.Get("errmsg"));
                            throw new TopCometSysErrorException("Server response err msg:" + connection.Headers.Get("errmsg"));
                        }
                        if (HttpStatusCode.Forbidden == statusCode)
                        {
                            this.log.Info("Server is deploying,sleep " + (i * this.conf.GetHttpConnectRetryInterval()) + " seconds");
                            if (i == this.conf.GetHttpConnectRetryCount())
                            {
                                this.log.Info("May be server occure some error,please contact top tech support");
                                throw new TopCometSysErrorException("May be server occure some error,please contact top tech support");
                            }
                            try
                            {
                                Thread.Sleep((int) ((i * this.conf.GetHttpConnectRetryInterval()) * 0x3e8));
                            }
                            catch (Exception)
                            {
                            }
                            continue;
                        }
                    }
                    catch (Exception exception)
                    {
                        this.log.Error(exception.Message);
                    }
                    finally
                    {
                        try
                        {
                            if (requestStream != null)
                            {
                                requestStream.Close();
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                catch (Exception exception2)
                {
                    if (i == this.conf.GetHttpConnectRetryCount())
                    {
                        throw new TopCometSysErrorException(exception2.Message);
                    }
                }
                try
                {
                    this.log.Info("Sleeping " + this.conf.GetHttpConnectRetryInterval() + " seconds until the next retry.");
                    Thread.Sleep((int) ((i * this.conf.GetHttpConnectRetryInterval()) * 0x3e8));
                }
                catch (Exception)
                {
                }
            }
            return null;
        }

        private void SetHeaders(HttpWebRequest connection, IDictionary<string, string> reqHeader)
        {
            if ((reqHeader != null) && (reqHeader.Count > 0))
            {
                foreach (KeyValuePair<string, string> pair in reqHeader)
                {
                    connection.Headers.Add(pair.Key, pair.Value);
                }
            }
        }
    }
}

