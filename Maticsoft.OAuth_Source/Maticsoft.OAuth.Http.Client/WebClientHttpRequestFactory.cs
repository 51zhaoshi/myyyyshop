namespace Maticsoft.OAuth.Http.Client
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Util;
    using System;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;

    public class WebClientHttpRequestFactory : IClientHttpRequestFactory
    {
        private bool? _allowAutoRedirect;
        private DecompressionMethods? _automaticDecompression;
        private X509CertificateCollection _clientCertificates;
        private System.Net.CookieContainer _cookieContainer;
        private ICredentials _credentials;
        private bool? _expect100Continue;
        private IWebProxy _proxy;
        private int? _timeout;
        private bool? _useDefaultCredentials;

        public virtual IClientHttpRequest CreateRequest(Uri uri, HttpMethod method)
        {
            ArgumentUtils.AssertNotNull(uri, "uri");
            ArgumentUtils.AssertNotNull(method, "method");
            HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            request.Method = method.ToString();
            if (this._allowAutoRedirect.HasValue)
            {
                request.AllowAutoRedirect = this._allowAutoRedirect.Value;
            }
            if (this._useDefaultCredentials.HasValue)
            {
                request.UseDefaultCredentials = this._useDefaultCredentials.Value;
            }
            if (this._cookieContainer != null)
            {
                request.CookieContainer = this._cookieContainer;
            }
            if (this._credentials != null)
            {
                request.Credentials = this._credentials;
            }
            if (this._clientCertificates != null)
            {
                foreach (X509Certificate2 certificate in this._clientCertificates)
                {
                    request.ClientCertificates.Add(certificate);
                }
            }
            if (this._proxy != null)
            {
                request.Proxy = this._proxy;
            }
            if (this._timeout.HasValue)
            {
                request.Timeout = this._timeout.Value;
            }
            if (this._expect100Continue.HasValue)
            {
                request.ServicePoint.Expect100Continue = this._expect100Continue.Value;
            }
            if (this._automaticDecompression.HasValue)
            {
                request.AutomaticDecompression = this._automaticDecompression.Value;
            }
            return new WebClientHttpRequest(request);
        }

        public bool? AllowAutoRedirect
        {
            get
            {
                return this._allowAutoRedirect;
            }
            set
            {
                this._allowAutoRedirect = value;
            }
        }

        public DecompressionMethods? AutomaticDecompression
        {
            get
            {
                return this._automaticDecompression;
            }
            set
            {
                this._automaticDecompression = value;
            }
        }

        public X509CertificateCollection ClientCertificates
        {
            get
            {
                if (this._clientCertificates == null)
                {
                    this._clientCertificates = new X509CertificateCollection();
                }
                return this._clientCertificates;
            }
        }

        public System.Net.CookieContainer CookieContainer
        {
            get
            {
                return this._cookieContainer;
            }
            set
            {
                this._cookieContainer = value;
            }
        }

        public ICredentials Credentials
        {
            get
            {
                return this._credentials;
            }
            set
            {
                this._credentials = value;
            }
        }

        public bool? Expect100Continue
        {
            get
            {
                return this._expect100Continue;
            }
            set
            {
                this._expect100Continue = value;
            }
        }

        public IWebProxy Proxy
        {
            get
            {
                return this._proxy;
            }
            set
            {
                this._proxy = value;
            }
        }

        public int? Timeout
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

        public bool? UseDefaultCredentials
        {
            get
            {
                return this._useDefaultCredentials;
            }
            set
            {
                this._useDefaultCredentials = value;
            }
        }
    }
}

