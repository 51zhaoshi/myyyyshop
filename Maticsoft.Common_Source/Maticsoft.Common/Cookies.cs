namespace Maticsoft.Common
{
    using System;
    using System.Web;

    public class Cookies
    {
        public static string getCookie(string key, string value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            try
            {
                if (cookie != null)
                {
                    return HttpUtility.UrlDecode(cookie.Values[value]);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static bool setCookie(string key, string value, double time)
        {
            try
            {
                string s = HttpUtility.UrlEncode(value);
                HttpCookie cookie2 = new HttpCookie(key) {
                    Expires = DateTime.Now.AddMinutes(time)
                };
                HttpCookie cookie = cookie2;
                cookie.Values.Add("Value", HttpContext.Current.Server.UrlEncode(s));
                HttpContext.Current.Response.Cookies.Add(cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool updateCookies(string key, string value, double time)
        {
            bool flag;
            try
            {
                HttpContext.Current.Response.Cookies[key]["Value"] = value;
                flag = setCookie(key, value, time);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }
    }
}

