namespace Maticsoft.TaoBao
{
    using Jayrock.Json.Conversion;
    using Maticsoft.TaoBao.Parser;
    using Maticsoft.TaoBao.Request;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class DefaultTopClient : ITopClient
    {
        public const string APP_KEY = "app_key";
        private string appKey;
        private string appSecret;
        private bool disableParser;
        private bool disableTrace;
        private string format;
        public const string FORMAT = "format";
        public const string FORMAT_XML = "xml";
        public const string METHOD = "method";
        public const string PARTNER_ID = "partner_id";
        private string serverUrl;
        public const string SESSION = "session";
        public const string SIGN = "sign";
        private IDictionary<string, string> systemParameters;
        public const string TIMESTAMP = "timestamp";
        private ITopLogger topLogger;
        public const string VERSION = "v";
        private WebUtils webUtils;

        public DefaultTopClient(string serverUrl, string appKey, string appSecret)
        {
            this.format = "xml";
            this.appKey = appKey;
            this.appSecret = appSecret;
            this.serverUrl = serverUrl;
            this.webUtils = new WebUtils();
            this.topLogger = new DefaultTopLogger();
        }

        public DefaultTopClient(string serverUrl, string appKey, string appSecret, string format) : this(serverUrl, appKey, appSecret)
        {
            this.format = format;
        }

        private T createErrorResponse<T>(string errCode, string errMsg) where T: TopResponse
        {
            T local = Activator.CreateInstance<T>();
            local.ErrCode = errCode;
            local.ErrMsg = errMsg;
            if ("xml".Equals(this.format))
            {
                XmlDocument document = new XmlDocument();
                XmlElement newChild = document.CreateElement("error_response");
                XmlElement element2 = document.CreateElement("code");
                element2.InnerText = errCode;
                newChild.AppendChild(element2);
                XmlElement element3 = document.CreateElement("msg");
                element3.InnerText = errMsg;
                newChild.AppendChild(element3);
                document.AppendChild(newChild);
                local.Body = document.OuterXml;
                return local;
            }
            IDictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("code", errCode);
            dictionary.Add("msg", errMsg);
            IDictionary<string, object> dictionary2 = new Dictionary<string, object>();
            dictionary2.Add("error_response", dictionary);
            string str = JsonConvert.ExportToString(dictionary2);
            local.Body = str;
            return local;
        }

        private T DoExecute<T>(ITopRequest<T> request, string session, DateTime timestamp) where T: TopResponse
        {
            string str;
            T local;
            try
            {
                request.Validate();
            }
            catch (TopException exception)
            {
                return this.createErrorResponse<T>(exception.ErrorCode, exception.ErrorMsg);
            }
            TopDictionary parameters = new TopDictionary(request.GetParameters());
            parameters.Add("method", request.GetApiName());
            parameters.Add("v", "2.0");
            parameters.Add("app_key", this.appKey);
            parameters.Add("format", this.format);
            parameters.Add("partner_id", "top-sdk-net-20121112");
            parameters.Add("timestamp", timestamp);
            parameters.Add("session", session);
            parameters.AddAll(this.systemParameters);
            parameters.Add("sign", TopUtils.SignTopRequest(parameters, this.appSecret));
            if (request is ITopUploadRequest<T>)
            {
                IDictionary<string, FileItem> fileParams = TopUtils.CleanupDictionary<FileItem>(((ITopUploadRequest<T>) request).GetFileParameters());
                str = this.webUtils.DoPost(this.serverUrl, parameters, fileParams);
            }
            else
            {
                str = this.webUtils.DoPost(this.serverUrl, parameters);
            }
            if (this.disableParser)
            {
                local = Activator.CreateInstance<T>();
                local.Body = str;
            }
            else if ("xml".Equals(this.format))
            {
                ITopParser parser = new TopXmlParser();
                local = parser.Parse<T>(str);
            }
            else
            {
                ITopParser parser2 = new TopJsonParser();
                local = parser2.Parse<T>(str);
            }
            if (!this.disableTrace)
            {
                local.ReqUrl = this.webUtils.BuildGetUrl(this.serverUrl, parameters);
                if (local.IsError)
                {
                    this.topLogger.Warn(local.ReqUrl + "\r\n" + local.Body);
                }
            }
            return local;
        }

        public T Execute<T>(ITopRequest<T> request) where T: TopResponse
        {
            return this.Execute<T>(request, null);
        }

        public T Execute<T>(ITopRequest<T> request, string session) where T: TopResponse
        {
            return this.Execute<T>(request, session, DateTime.Now);
        }

        public T Execute<T>(ITopRequest<T> request, string session, DateTime timestamp) where T: TopResponse
        {
            T local;
            if (this.disableTrace)
            {
                return this.DoExecute<T>(request, session, timestamp);
            }
            try
            {
                local = this.DoExecute<T>(request, session, timestamp);
            }
            catch (Exception exception)
            {
                this.topLogger.Error(this.serverUrl + "\r\n" + exception.StackTrace);
                throw exception;
            }
            return local;
        }

        public void SetDisableParser(bool disableParser)
        {
            this.disableParser = disableParser;
        }

        public void SetDisableTrace(bool disableTrace)
        {
            this.disableTrace = disableTrace;
        }

        public void SetSystemParameters(IDictionary<string, string> systemParameters)
        {
            this.systemParameters = systemParameters;
        }

        public void SetTimeout(int timeout)
        {
            this.webUtils.Timeout = timeout;
        }

        public void SetTopLogger(ITopLogger topLogger)
        {
            this.topLogger = topLogger;
        }
    }
}

