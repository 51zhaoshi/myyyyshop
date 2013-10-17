namespace Maticsoft.Common
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    public class UrlOper
    {
        private static Encoding encoding = Encoding.UTF8;

        public static string AddParam(string url, string paramName, string value)
        {
            Uri uri = new Uri(url);
            if (string.IsNullOrEmpty(uri.Query))
            {
                string str = HttpContext.Current.Server.UrlEncode(value);
                return (url + ("?" + paramName + "=" + str));
            }
            string str2 = HttpContext.Current.Server.UrlEncode(value);
            return (url + ("&" + paramName + "=" + str2));
        }

        public static string Base64Decrypt(string eStr)
        {
            if (!IsBase64(eStr))
            {
                return eStr;
            }
            byte[] bytes = Convert.FromBase64String(eStr);
            return HttpUtility.UrlDecode(encoding.GetString(bytes));
        }

        public static string Base64Encrypt(string sourthUrl)
        {
            string s = HttpUtility.UrlEncode(sourthUrl);
            return Convert.ToBase64String(encoding.GetBytes(s));
        }

        public static void GetDomain(string fromUrl, out string domain, out string subDomain)
        {
            domain = "";
            subDomain = "";
            try
            {
                if (fromUrl.IndexOf("的名片") > -1)
                {
                    subDomain = fromUrl;
                    domain = "名片";
                }
                else
                {
                    fromUrl = new UriBuilder(fromUrl).ToString();
                    Uri uri = new Uri(fromUrl);
                    if (uri.IsWellFormedOriginalString())
                    {
                        if (uri.IsFile)
                        {
                            subDomain = domain = "客户端本地文件路径";
                        }
                        else
                        {
                            string authority = uri.Authority;
                            string[] strArray = uri.Authority.Split(new char[] { '.' });
                            if (strArray.Length == 2)
                            {
                                authority = "www." + authority;
                            }
                            int index = authority.IndexOf('.', 0);
                            domain = authority.Substring(index + 1, (authority.Length - index) - 1).Replace("comhttp", "com");
                            subDomain = authority.Replace("comhttp", "com");
                            if (strArray.Length < 2)
                            {
                                domain = "不明路径";
                                subDomain = "不明路径";
                            }
                        }
                    }
                    else if (uri.IsFile)
                    {
                        subDomain = domain = "客户端本地文件路径";
                    }
                    else
                    {
                        subDomain = domain = "不明路径";
                    }
                }
            }
            catch
            {
                subDomain = domain = "不明路径";
            }
        }

        public static bool IsBase64(string eStr)
        {
            if ((eStr.Length % 4) != 0)
            {
                return false;
            }
            if (!Regex.IsMatch(eStr, "^[A-Z0-9/+=]*$", RegexOptions.IgnoreCase))
            {
                return false;
            }
            return true;
        }

        public static void ParseUrl(string url, out string baseUrl, out NameValueCollection nvc)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }
            nvc = new NameValueCollection();
            baseUrl = "";
            if (url != "")
            {
                int index = url.IndexOf('?');
                if (index == -1)
                {
                    baseUrl = url;
                }
                else
                {
                    baseUrl = url.Substring(0, index);
                    if (index != (url.Length - 1))
                    {
                        string input = url.Substring(index + 1);
                        Regex regex = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
                        foreach (Match match in regex.Matches(input))
                        {
                            nvc.Add(match.Result("$2").ToLower(), match.Result("$3"));
                        }
                    }
                }
            }
        }

        public static string UpdateParam(string url, string paramName, string value)
        {
            string str = paramName + "=";
            int startIndex = url.IndexOf(str) + str.Length;
            int index = url.IndexOf("&", startIndex);
            if (index == -1)
            {
                url = url.Remove(startIndex, url.Length - startIndex);
                url = url + value;
                return url;
            }
            url = url.Remove(startIndex, index - startIndex);
            url = url.Insert(startIndex, value);
            return url;
        }
    }
}

