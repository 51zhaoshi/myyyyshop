namespace Maticsoft.OAuth.v1
{
    using Maticsoft.OAuth.Http;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    internal class SigningSupport
    {
        public const string HMAC_SHA1_SIGNATURE_NAME = "HMAC-SHA1";
        private ITimestampGenerator timestampGenerator = new DefaultTimestampGenerator();
        private const string UNRESERVED_CHARS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

        private string BuildAuthorizationHeaderValue(HttpMethod method, Uri targetUrl, IDictionary<string, string> oauthParameters, NameValueCollection additionalParameters, string consumerSecret, string tokenSecret)
        {
            NameValueCollection collectedParameters = new NameValueCollection();
            StringBuilder builder = new StringBuilder();
            builder.Append("OAuth ");
            foreach (KeyValuePair<string, string> pair in oauthParameters)
            {
                builder.Append(OAuthEncode(pair.Key)).Append("=\"").Append(OAuthEncode(pair.Value)).Append("\", ");
                collectedParameters.Add(pair.Key, pair.Value);
            }
            foreach (string str in additionalParameters)
            {
                collectedParameters.Add(str, additionalParameters[str]);
            }
            string data = CalculateSignature(BuildBaseString(method, GetBaseStringUri(targetUrl), collectedParameters), consumerSecret, tokenSecret);
            builder.Append("oauth_signature=\"").Append(OAuthEncode(data)).Append("\"");
            return builder.ToString();
        }

        public string BuildAuthorizationHeaderValue(Uri tokenUrl, IDictionary<string, string> tokenParameters, NameValueCollection additionalParameters, string consumerKey, string consumerSecret, string tokenSecret)
        {
            IDictionary<string, string> oauthParameters = this.CreateCommonOAuthParameters(consumerKey);
            foreach (KeyValuePair<string, string> pair in tokenParameters)
            {
                oauthParameters.Add(pair);
            }
            if (additionalParameters == null)
            {
                additionalParameters = new NameValueCollection();
            }
            return this.BuildAuthorizationHeaderValue(HttpMethod.POST, tokenUrl, oauthParameters, additionalParameters, consumerSecret, tokenSecret);
        }

        public string BuildAuthorizationHeaderValue(Uri requestUri, HttpMethod requestMethod, HttpHeaders requestHeaders, Action<Stream> requestBody, string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            IDictionary<string, string> oauthParameters = this.CreateCommonOAuthParameters(consumerKey);
            oauthParameters.Add("oauth_token", accessToken);
            NameValueCollection additionalParameters = ReadFormParameters(requestHeaders.ContentType, requestBody);
            NameValueCollection values2 = ParseFormParameters(requestUri.Query.TrimStart(new char[] { '?' }));
            foreach (string str in values2)
            {
                additionalParameters.Add(str, values2[str]);
            }
            return this.BuildAuthorizationHeaderValue(requestMethod, requestUri, oauthParameters, additionalParameters, consumerSecret, accessTokenSecret);
        }

        internal static string BuildBaseString(HttpMethod method, string targetUrl, NameValueCollection collectedParameters)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(method).Append('&').Append(OAuthEncode(targetUrl)).Append('&');
            builder.Append(OAuthEncode(NormalizeParameters(collectedParameters)));
            return builder.ToString();
        }

        private static string CalculateSignature(string baseString, string consumerSecret, string tokenSecret)
        {
            string key = OAuthEncode(consumerSecret) + "&" + ((tokenSecret != null) ? OAuthEncode(tokenSecret) : "");
            return Sign(baseString, key);
        }

        private IDictionary<string, string> CreateCommonOAuthParameters(string consumerKey)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("oauth_consumer_key", consumerKey);
            dictionary.Add("oauth_signature_method", "HMAC-SHA1");
            long timestamp = this.timestampGenerator.GenerateTimestamp();
            dictionary.Add("oauth_timestamp", timestamp.ToString());
            dictionary.Add("oauth_nonce", this.timestampGenerator.GenerateNonce(timestamp).ToString());
            dictionary.Add("oauth_version", "1.0");
            return dictionary;
        }

        private static string GetBaseStringUri(Uri uri)
        {
            if (((uri.Scheme != "http") || (uri.Port != 80)) && (!(uri.Scheme == "https") || (uri.Port != 0x1bb)))
            {
                return uri.GetComponents(UriComponents.HostAndPort | UriComponents.Path | UriComponents.Scheme, UriFormat.UriEscaped);
            }
            return uri.GetComponents(UriComponents.Path | UriComponents.Host | UriComponents.Scheme, UriFormat.UriEscaped);
        }

        private static string NormalizeParameters(NameValueCollection collectedParameters)
        {
            NameValueCollection values = new NameValueCollection();
            foreach (string str in collectedParameters)
            {
                string name = OAuthEncode(str);
                string[] array = collectedParameters.GetValues(str);
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = OAuthEncode(array[i]);
                }
                Array.Sort<string>(array);
                foreach (string str3 in array)
                {
                    values.Add(name, str3);
                }
            }
            string[] allKeys = values.AllKeys;
            Array.Sort<string>(allKeys);
            StringBuilder builder = new StringBuilder();
            foreach (string str4 in allKeys)
            {
                foreach (string str5 in values.GetValues(str4))
                {
                    builder.Append(str4).Append('=').Append(str5).Append('&');
                }
            }
            return builder.ToString().TrimEnd(new char[] { '&' });
        }

        private static string OAuthEncode(string data)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char ch in data)
            {
                if ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~".IndexOf(ch) != -1)
                {
                    builder.Append(ch);
                }
                else
                {
                    foreach (byte num in Encoding.UTF8.GetBytes(new char[] { ch }))
                    {
                        builder.AppendFormat("%{0:X2}", num);
                    }
                }
            }
            return builder.ToString();
        }

        private static NameValueCollection ParseFormParameters(string parameterString)
        {
            NameValueCollection values = new NameValueCollection();
            if ((parameterString != null) && (parameterString.Trim().Length > 0))
            {
                foreach (string str in parameterString.Split(new char[] { '&' }))
                {
                    int index = str.IndexOf('=');
                    if (index == -1)
                    {
                        values.Add(HttpUtils.FormDecode(str), string.Empty);
                    }
                    else
                    {
                        string name = HttpUtils.FormDecode(str.Substring(0, index));
                        string str3 = HttpUtils.FormDecode(str.Substring(index + 1));
                        values.Add(name, str3);
                    }
                }
            }
            return values;
        }

        private static NameValueCollection ReadFormParameters(MediaType requestContentType, Action<Stream> requestBody)
        {
            if (((requestContentType != null) && (requestBody != null)) && requestContentType.Equals(MediaType.APPLICATION_FORM_URLENCODED))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    requestBody(stream);
                    stream.Position = 0L;
                    byte[] bytes = stream.ToArray();
                    Encoding encoding = ((requestContentType != null) && (requestContentType.CharSet != null)) ? requestContentType.CharSet : Encoding.UTF8;
                    return ParseFormParameters(encoding.GetString(bytes, 0, bytes.Length));
                }
            }
            return new NameValueCollection();
        }

        private static string Sign(string baseString, string key)
        {
            HMACSHA1 hmacsha = new HMACSHA1 {
                Key = Encoding.UTF8.GetBytes(key)
            };
            byte[] bytes = Encoding.UTF8.GetBytes(baseString);
            return Convert.ToBase64String(hmacsha.ComputeHash(bytes));
        }

        public ITimestampGenerator TimestampGenerator
        {
            set
            {
                this.timestampGenerator = value;
            }
        }

        private class DefaultTimestampGenerator : SigningSupport.ITimestampGenerator
        {
            private static Random random = new Random();
            private static object syncRoot = new object();

            public long GenerateNonce(long timestamp)
            {
                int num;
                lock (syncRoot)
                {
                    num = random.Next();
                }
                return (timestamp + num);
            }

            public long GenerateTimestamp()
            {
                TimeSpan span = (TimeSpan) (DateTime.UtcNow - new DateTime(0x7b2, 1, 1));
                return (long) span.TotalSeconds;
            }
        }

        public interface ITimestampGenerator
        {
            long GenerateNonce(long timestamp);
            long GenerateTimestamp();
        }
    }
}

