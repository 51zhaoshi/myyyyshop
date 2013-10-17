namespace Maticsoft.OAuth.Http
{
    using System;

    public static class HttpUtils
    {
        public static string FormDecode(string s)
        {
            if (s == null)
            {
                return null;
            }
            return Uri.UnescapeDataString(s.Replace('+', ' '));
        }

        public static string FormEncode(string s)
        {
            if (s == null)
            {
                return null;
            }
            return UrlEncode(s).Replace("%20", "+");
        }

        public static string UrlDecode(string s)
        {
            if (s == null)
            {
                return null;
            }
            return Uri.UnescapeDataString(s);
        }

        public static string UrlEncode(string s)
        {
            if (s == null)
            {
                return null;
            }
            return Uri.EscapeDataString(s).Replace("!", "%21").Replace("'", "%27").Replace("(", "%28").Replace(")", "%29").Replace("*", "%2A");
        }
    }
}

