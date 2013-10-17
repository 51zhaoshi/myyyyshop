namespace Maticsoft.OAuth.Http
{
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    public class HttpHeaders : NameValueCollection
    {
        private const string ACCEPT = "Accept";
        private const string ACCEPT_CHARSET = "Accept-Charset";
        private const string ALLOW = "Allow";
        private const string CACHE_CONTROL = "Cache-Control";
        private const string CONTENT_LENGTH = "Content-Length";
        private const string CONTENT_TYPE = "Content-Type";
        private const string DATE = "Date";
        private static readonly System.Globalization.DateTimeFormatInfo DateTimeFormatInfo = new System.Globalization.DateTimeFormatInfo();
        private const string ETAG = "ETag";
        private const string EXPIRES = "Expires";
        private const string IF_MODIFIED_SINCE = "If-Modified-Since";
        private const string IF_NONE_MATCH = "If-None-Match";
        private const string LAST_MODIFIED = "Last-Modified";
        private const string LOCATION = "Location";
        private const string PRAGMA = "Pragma";

        public HttpHeaders() : base(8, (IEqualityComparer) StringComparer.OrdinalIgnoreCase)
        {
        }

        protected HttpHeaders(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string[] GetMultiValues(string headerName)
        {
            string str = this.Get(headerName);
            if (str == null)
            {
                return null;
            }
            return str.Split(new char[] { ',' });
        }

        private DateTime? GetSingleDate(string headerName)
        {
            string singleValue = this.GetSingleValue(headerName);
            if (singleValue != null)
            {
                return new DateTime?(DateTime.Parse(singleValue, DateTimeFormatInfo).ToUniversalTime());
            }
            return null;
        }

        public string GetSingleValue(string headerName)
        {
            string[] values = this.GetValues(headerName);
            if ((values == null) || (values.Length == 0))
            {
                return null;
            }
            if (values.Length != 1)
            {
                throw new NotSupportedException(string.Format("Multiple values not supported for header '{0}'", headerName));
            }
            return values[0];
        }

        private string Quote(string s)
        {
            if (s == null)
            {
                return null;
            }
            if (!s.StartsWith("\"") && !s.EndsWith("\""))
            {
                s = "\"" + s + "\"";
            }
            return s;
        }

        private void SetDate(string headerName, DateTime? date)
        {
            if (date.HasValue)
            {
                this.Set(headerName, date.Value.ToUniversalTime().ToString("R", DateTimeFormatInfo));
            }
            else
            {
                this.Remove(headerName);
            }
        }

        private string Unquote(string s)
        {
            if (s == null)
            {
                return null;
            }
            if (s.StartsWith("\"") && s.EndsWith("\""))
            {
                s = s.Substring(1, s.Length - 2);
            }
            return s;
        }

        public MediaType[] Accept
        {
            get
            {
                string[] multiValues = this.GetMultiValues("Accept");
                if ((multiValues == null) || (multiValues.Length == 0))
                {
                    return new MediaType[0];
                }
                MediaType[] typeArray = new MediaType[multiValues.Length];
                for (int i = 0; i < multiValues.Length; i++)
                {
                    typeArray[i] = MediaType.Parse(multiValues[i]);
                }
                return typeArray;
            }
            set
            {
                foreach (MediaType type in value)
                {
                    this.Add("Accept", type.ToString());
                }
            }
        }

        public HttpMethod[] Allow
        {
            get
            {
                string[] multiValues = this.GetMultiValues("Allow");
                if ((multiValues == null) || (multiValues.Length == 0))
                {
                    return new HttpMethod[0];
                }
                HttpMethod[] methodArray = new HttpMethod[multiValues.Length];
                for (int i = 0; i < multiValues.Length; i++)
                {
                    methodArray[i] = new HttpMethod(multiValues[i].Trim());
                }
                return methodArray;
            }
            set
            {
                foreach (HttpMethod method in value)
                {
                    this.Add("Allow", method.ToString());
                }
            }
        }

        public string CacheControl
        {
            get
            {
                return this.Get("Cache-Control");
            }
            set
            {
                this.Set("Cache-Control", value);
            }
        }

        public long ContentLength
        {
            get
            {
                string singleValue = this.GetSingleValue("Content-Length");
                if (singleValue == null)
                {
                    return -1L;
                }
                return long.Parse(singleValue);
            }
            set
            {
                this.Set("Content-Length", value.ToString());
            }
        }

        public MediaType ContentType
        {
            get
            {
                string singleValue = this.GetSingleValue("Content-Type");
                if (singleValue == null)
                {
                    return null;
                }
                return MediaType.Parse(singleValue);
            }
            set
            {
                if (value.IsWildcardType)
                {
                    throw new ArgumentException("'Content-Type' header cannot contain wildcard type '*'", "Content-Type");
                }
                if (value.IsWildcardSubtype)
                {
                    throw new ArgumentException("'Content-Type' header cannot contain wildcard subtype '*'", "Content-Type");
                }
                this.Set("Content-Type", value.ToString());
            }
        }

        public DateTime? Date
        {
            get
            {
                return this.GetSingleDate("Date");
            }
            set
            {
                this.SetDate("Date", value);
            }
        }

        public string ETag
        {
            get
            {
                return this.Unquote(this.Get("ETag"));
            }
            set
            {
                this.Set("ETag", this.Quote(value));
            }
        }

        public string Expires
        {
            get
            {
                return this.Get("Expires");
            }
            set
            {
                this.Set("Expires", value);
            }
        }

        public DateTime? IfModifiedSince
        {
            get
            {
                return this.GetSingleDate("If-Modified-Since");
            }
            set
            {
                this.SetDate("If-Modified-Since", value);
            }
        }

        public string[] IfNoneMatch
        {
            get
            {
                string[] multiValues = this.GetMultiValues("If-None-Match");
                if ((multiValues == null) || (multiValues.Length == 0))
                {
                    return new string[0];
                }
                string[] strArray2 = new string[multiValues.Length];
                for (int i = 0; i < multiValues.Length; i++)
                {
                    strArray2[i] = this.Unquote(multiValues[i].Trim());
                }
                return strArray2;
            }
            set
            {
                foreach (string str in value)
                {
                    this.Add("If-None-Match", this.Quote(str));
                }
            }
        }

        public DateTime? LastModified
        {
            get
            {
                return this.GetSingleDate("Last-Modified");
            }
            set
            {
                this.SetDate("Last-Modified", value);
            }
        }

        public Uri Location
        {
            get
            {
                string singleValue = this.GetSingleValue("Location");
                if (singleValue == null)
                {
                    return null;
                }
                return new Uri(singleValue, UriKind.RelativeOrAbsolute);
            }
            set
            {
                this.Set("Location", value.ToString());
            }
        }

        public string Pragma
        {
            get
            {
                return this.Get("Pragma");
            }
            set
            {
                this.Set("Pragma", value);
            }
        }
    }
}

