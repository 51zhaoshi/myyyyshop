namespace Maticsoft.TaoBao.Util
{
    using Jayrock.Json.Conversion;
    using Maticsoft.TaoBao.Parser;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;

    public abstract class TopUtils
    {
        public const string TOP_AUTH_URL = "http://container.open.taobao.com/container?authcode=";

        protected TopUtils()
        {
        }

        public static IDictionary<string, T> CleanupDictionary<T>(IDictionary<string, T> dict)
        {
            IDictionary<string, T> dictionary = new Dictionary<string, T>(dict.Count);
            IEnumerator<KeyValuePair<string, T>> enumerator = dict.GetEnumerator();
            while (enumerator.MoveNext())
            {
                KeyValuePair<string, T> current = enumerator.Current;
                string key = current.Key;
                KeyValuePair<string, T> pair2 = enumerator.Current;
                T local = pair2.Value;
                if (local != null)
                {
                    dictionary.Add(key, local);
                }
            }
            return dictionary;
        }

        public static IDictionary<string, string> DecodeTopParams(string topParams)
        {
            return DecodeTopParams(topParams, Encoding.GetEncoding("GBK"));
        }

        public static IDictionary<string, string> DecodeTopParams(string topParams, Encoding encoding)
        {
            if (string.IsNullOrEmpty(topParams))
            {
                return null;
            }
            byte[] bytes = Convert.FromBase64String(Uri.UnescapeDataString(topParams));
            return SplitUrlQuery(encoding.GetString(bytes));
        }

        public static long GetCurrentTimeMillis()
        {
            return (long) DateTime.UtcNow.Subtract(new DateTime(0x7b2, 1, 1)).TotalMilliseconds;
        }

        public static string GetFileSuffix(byte[] fileData)
        {
            if ((fileData != null) && (fileData.Length >= 10))
            {
                if (((fileData[0] == 0x47) && (fileData[1] == 0x49)) && (fileData[2] == 70))
                {
                    return "GIF";
                }
                if (((fileData[1] == 80) && (fileData[2] == 0x4e)) && (fileData[3] == 0x47))
                {
                    return "PNG";
                }
                if (((fileData[6] == 0x4a) && (fileData[7] == 70)) && ((fileData[8] == 0x49) && (fileData[9] == 70)))
                {
                    return "JPG";
                }
                if ((fileData[0] == 0x42) && (fileData[1] == 0x4d))
                {
                    return "BMP";
                }
            }
            return null;
        }

        public static string GetMimeType(string fileName)
        {
            fileName = fileName.ToLower();
            if (fileName.EndsWith(".bmp", StringComparison.CurrentCulture))
            {
                return "image/bmp";
            }
            if (fileName.EndsWith(".gif", StringComparison.CurrentCulture))
            {
                return "image/gif";
            }
            if (fileName.EndsWith(".jpg", StringComparison.CurrentCulture) || fileName.EndsWith(".jpeg", StringComparison.CurrentCulture))
            {
                return "image/jpeg";
            }
            if (fileName.EndsWith(".png", StringComparison.CurrentCulture))
            {
                return "image/png";
            }
            return "application/octet-stream";
        }

        public static string GetMimeType(byte[] fileData)
        {
            switch (GetFileSuffix(fileData))
            {
                case "JPG":
                    return "image/jpeg";

                case "GIF":
                    return "image/gif";

                case "PNG":
                    return "image/png";

                case "BMP":
                    return "image/bmp";
            }
            return "application/octet-stream";
        }

        public static string GetRootElement(string api)
        {
            int index = api.IndexOf(".");
            if ((index != -1) && (api.Length > index))
            {
                api = api.Substring(index + 1).Replace('.', '_');
            }
            return (api + "_response");
        }

        public static TopContext GetTopContext(string authCode)
        {
            string url = "http://container.open.taobao.com/container?authcode=" + authCode;
            string str2 = new WebUtils().DoGet(url, null);
            if (string.IsNullOrEmpty(str2))
            {
                return null;
            }
            TopContext context = new TopContext();
            IEnumerator<KeyValuePair<string, string>> enumerator = SplitUrlQuery(str2).GetEnumerator();
            while (enumerator.MoveNext())
            {
                KeyValuePair<string, string> current = enumerator.Current;
                if ("top_parameters".Equals(current.Key))
                {
                    KeyValuePair<string, string> pair2 = enumerator.Current;
                    context.AddParameters(DecodeTopParams(pair2.Value));
                }
                else
                {
                    KeyValuePair<string, string> pair3 = enumerator.Current;
                    KeyValuePair<string, string> pair4 = enumerator.Current;
                    context.AddParameter(pair3.Key, pair4.Value);
                }
            }
            return context;
        }

        public static IDictionary ParseJson(string json)
        {
            return (JsonConvert.Import(json) as IDictionary);
        }

        public static T ParseResponse<T>(string json) where T: TopResponse
        {
            TopJsonParser parser = new TopJsonParser();
            return parser.Parse<T>(json);
        }

        public static string SignTopRequest(IDictionary<string, string> parameters, string secret)
        {
            return SignTopRequest(parameters, secret, false);
        }

        public static string SignTopRequest(IDictionary<string, string> parameters, string secret, bool qhs)
        {
            IDictionary<string, string> dictionary = new SortedDictionary<string, string>(parameters);
            IEnumerator<KeyValuePair<string, string>> enumerator = dictionary.GetEnumerator();
            StringBuilder builder = new StringBuilder(secret);
            while (enumerator.MoveNext())
            {
                KeyValuePair<string, string> current = enumerator.Current;
                string key = current.Key;
                KeyValuePair<string, string> pair2 = enumerator.Current;
                string str2 = pair2.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(str2))
                {
                    builder.Append(key).Append(str2);
                }
            }
            if (qhs)
            {
                builder.Append(secret);
            }
            byte[] buffer = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(builder.ToString()));
            StringBuilder builder2 = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                string str3 = buffer[i].ToString("X");
                if (str3.Length == 1)
                {
                    builder2.Append("0");
                }
                builder2.Append(str3);
            }
            return builder2.ToString();
        }

        private static IDictionary<string, string> SplitUrlQuery(string query)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            string[] strArray = query.Split(new char[] { '&' });
            if ((strArray != null) && (strArray.Length > 0))
            {
                foreach (string str in strArray)
                {
                    string[] strArray2 = str.Split(new char[] { '=' }, 2);
                    if ((strArray2 != null) && (strArray2.Length == 2))
                    {
                        dictionary.Add(strArray2[0], strArray2[1]);
                    }
                }
            }
            return dictionary;
        }

        public static bool VerifyTopResponse(string callbackUrl, string appSecret)
        {
            string str2;
            string str3;
            string str4;
            string str5;
            Uri uri = new Uri(callbackUrl);
            string query = uri.Query;
            if (string.IsNullOrEmpty(query))
            {
                return false;
            }
            query = query.Trim(new char[] { '?', ' ' });
            if (query.Length == 0)
            {
                return false;
            }
            IDictionary<string, string> dictionary = SplitUrlQuery(query);
            dictionary.TryGetValue("top_parameters", out str2);
            dictionary.TryGetValue("top_session", out str3);
            dictionary.TryGetValue("top_sign", out str4);
            dictionary.TryGetValue("top_appkey", out str5);
            str4 = (str4 == null) ? null : Uri.UnescapeDataString(str4);
            return VerifyTopResponse(str2, str3, str4, str5, appSecret);
        }

        public static bool VerifyTopResponse(string topParams, string topSession, string topSign, string appKey, string appSecret)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(appKey).Append(topParams).Append(topSession).Append(appSecret);
            return (Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(builder.ToString()))) == topSign);
        }
    }
}

