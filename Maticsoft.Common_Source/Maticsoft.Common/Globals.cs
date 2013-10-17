namespace Maticsoft.Common
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Globalization;
    using System.Net.Mail;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    public sealed class Globals
    {
        private const string DOMAIN_RULES = "||com.cn|net.cn|org.cn|gov.cn|com.hk|公司|中国|网络|com|net|org|int|edu|gov|mil|arpa|Asia|biz|info|name|pro|coop|aero|museum|ac|ad|ae|af|ag|ai|al|am|an|ao|aq|ar|as|at|au|aw|az|ba|bb|bd|be|bf|bg|bh|bi|bj|bm|bn|bo|br|bs|bt|bv|bw|by|bz|ca|cc|cf|cg|ch|ci|ck|cl|cm|cn|co|cq|cr|cu|cv|cx|cy|cz|de|dj|dk|dm|do|dz|ec|ee|eg|eh|es|et|ev|fi|fj|fk|fm|fo|fr|ga|gb|gd|ge|gf|gh|gi|gl|gm|gn|gp|gr|gt|gu|gw|gy|hk|hm|hn|hr|ht|hu|id|ie|il|in|io|iq|ir|is|it|jm|jo|jp|ke|kg|kh|ki|km|kn|kp|kr|kw|ky|kz|la|lb|lc|li|lk|lr|ls|lt|lu|lv|ly|ma|mc|md|me|mg|mh|ml|mm|mn|mo|mp|mq|mr|ms|mt|mv|mw|mx|my|mz|na|nc|ne|nf|ng|ni|nl|no|np|nr|nt|nu|nz|om|pa|pe|pf|pg|ph|pk|pl|pm|pn|pr|pt|pw|py|qa|re|ro|ru|rw|sa|sb|sc|sd|se|sg|sh|si|sj|sk|sl|sm|sn|so|sr|st|su|sy|sz|tc|td|tf|tg|th|tj|tk|tm|tn|to|tp|tr|tt|tv|tw|tz|ua|ug|uk|us|uy|va|vc|ve|vg|vn|vu|wf|ws|ye|yu|za|zm|zr|zw|";
        public const int PAY_BALANCE_PAYMENTMODEID = -2;
        public const string PAY_SECURITY_CODE = "MATICSOFT_SENDING";
        public const string PAY_SECURITY_KEY = "MATICSOFT_SECURITY_CODE";
        public static decimal POINT_RATIO = 1M;
        public static string SESSIONKEY_ADMIN = "Admin_UserInfo";
        public static string SESSIONKEY_AGENTS = "Agents_UserInfo";
        public static string SESSIONKEY_ENTERPRISE = "Enterprise_UserInfo";
        public static string SESSIONKEY_SUPPLIER = "Supplier_UserInfo";
        public static string SESSIONKEY_USER = (SESSIONKEY_AGENTS = SESSIONKEY_ENTERPRISE = SESSIONKEY_SUPPLIER = SESSIONKEY_ADMIN = "UserInfo");

        private Globals()
        {
        }

        public static string AppendQuerystring(string url, string querystring)
        {
            return AppendQuerystring(url, querystring, false).Trim();
        }

        public static string AppendQuerystring(string url, string querystring, bool urlEncoded)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            string str = "?";
            if (url.IndexOf('?') > -1)
            {
                if (!urlEncoded)
                {
                    str = "&";
                }
                else
                {
                    str = "&amp;";
                }
            }
            return (url + str + querystring);
        }

        public static string FullPath(string local)
        {
            if (string.IsNullOrEmpty(local))
            {
                return local;
            }
            if (local.ToLower(CultureInfo.InvariantCulture).StartsWith("http://"))
            {
                return local;
            }
            if (HttpContext.Current == null)
            {
                return local;
            }
            return FullPath(HostPath(HttpContext.Current.Request.Url), local);
        }

        public static string FullPath(string hostPath, string local)
        {
            return (hostPath + local);
        }

        public static string GenRandomCodeFor6()
        {
            long ticks = DateTime.Now.Ticks;
            Random random = new Random(((int) (((ulong) ticks) & 0xffffffffL)) | ((int) (ticks >> 0x20)));
            return random.Next(0, 0xf423f).ToString(CultureInfo.InvariantCulture);
        }

        public static DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)
        {
            if (PageIndex == 0)
            {
                return dt;
            }
            DataTable table = dt.Copy();
            table.Clear();
            int num = (PageIndex - 1) * PageSize;
            int count = PageIndex * PageSize;
            if (num < dt.Rows.Count)
            {
                if (count > dt.Rows.Count)
                {
                    count = dt.Rows.Count;
                }
                for (int i = num; i <= (count - 1); i++)
                {
                    DataRow row = table.NewRow();
                    DataRow row2 = dt.Rows[i];
                    foreach (DataColumn column in dt.Columns)
                    {
                        row[column.ColumnName] = row2[column.ColumnName];
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public static string GetTopLevelDomain(string domain)
        {
            string str;
            if (string.IsNullOrWhiteSpace(domain))
            {
                return string.Empty;
            }
            if (domain.IndexOf(".") < 1)
            {
                domain = domain.Split(new char[] { ':' })[0];
                return domain;
            }
            string[] strArray = domain.Split(new char[] { ':' })[0].Split(new char[] { '.' });
            if (IsNumeric(strArray[strArray.Length - 1]))
            {
                return domain.Split(new char[] { ':' })[0];
            }
            if (strArray.Length >= 4)
            {
                str = strArray[strArray.Length - 3] + "." + strArray[strArray.Length - 2] + "." + strArray[strArray.Length - 1];
                if ("||com.cn|net.cn|org.cn|gov.cn|com.hk|公司|中国|网络|com|net|org|int|edu|gov|mil|arpa|Asia|biz|info|name|pro|coop|aero|museum|ac|ad|ae|af|ag|ai|al|am|an|ao|aq|ar|as|at|au|aw|az|ba|bb|bd|be|bf|bg|bh|bi|bj|bm|bn|bo|br|bs|bt|bv|bw|by|bz|ca|cc|cf|cg|ch|ci|ck|cl|cm|cn|co|cq|cr|cu|cv|cx|cy|cz|de|dj|dk|dm|do|dz|ec|ee|eg|eh|es|et|ev|fi|fj|fk|fm|fo|fr|ga|gb|gd|ge|gf|gh|gi|gl|gm|gn|gp|gr|gt|gu|gw|gy|hk|hm|hn|hr|ht|hu|id|ie|il|in|io|iq|ir|is|it|jm|jo|jp|ke|kg|kh|ki|km|kn|kp|kr|kw|ky|kz|la|lb|lc|li|lk|lr|ls|lt|lu|lv|ly|ma|mc|md|me|mg|mh|ml|mm|mn|mo|mp|mq|mr|ms|mt|mv|mw|mx|my|mz|na|nc|ne|nf|ng|ni|nl|no|np|nr|nt|nu|nz|om|pa|pe|pf|pg|ph|pk|pl|pm|pn|pr|pt|pw|py|qa|re|ro|ru|rw|sa|sb|sc|sd|se|sg|sh|si|sj|sk|sl|sm|sn|so|sr|st|su|sy|sz|tc|td|tf|tg|th|tj|tk|tm|tn|to|tp|tr|tt|tv|tw|tz|ua|ug|uk|us|uy|va|vc|ve|vg|vn|vu|wf|ws|ye|yu|za|zm|zr|zw|".IndexOf("|" + str + "|") > 0)
                {
                    return (strArray[strArray.Length - 4] + "." + str);
                }
            }
            if (strArray.Length >= 3)
            {
                str = strArray[strArray.Length - 2] + "." + strArray[strArray.Length - 1];
                if ("||com.cn|net.cn|org.cn|gov.cn|com.hk|公司|中国|网络|com|net|org|int|edu|gov|mil|arpa|Asia|biz|info|name|pro|coop|aero|museum|ac|ad|ae|af|ag|ai|al|am|an|ao|aq|ar|as|at|au|aw|az|ba|bb|bd|be|bf|bg|bh|bi|bj|bm|bn|bo|br|bs|bt|bv|bw|by|bz|ca|cc|cf|cg|ch|ci|ck|cl|cm|cn|co|cq|cr|cu|cv|cx|cy|cz|de|dj|dk|dm|do|dz|ec|ee|eg|eh|es|et|ev|fi|fj|fk|fm|fo|fr|ga|gb|gd|ge|gf|gh|gi|gl|gm|gn|gp|gr|gt|gu|gw|gy|hk|hm|hn|hr|ht|hu|id|ie|il|in|io|iq|ir|is|it|jm|jo|jp|ke|kg|kh|ki|km|kn|kp|kr|kw|ky|kz|la|lb|lc|li|lk|lr|ls|lt|lu|lv|ly|ma|mc|md|me|mg|mh|ml|mm|mn|mo|mp|mq|mr|ms|mt|mv|mw|mx|my|mz|na|nc|ne|nf|ng|ni|nl|no|np|nr|nt|nu|nz|om|pa|pe|pf|pg|ph|pk|pl|pm|pn|pr|pt|pw|py|qa|re|ro|ru|rw|sa|sb|sc|sd|se|sg|sh|si|sj|sk|sl|sm|sn|so|sr|st|su|sy|sz|tc|td|tf|tg|th|tj|tk|tm|tn|to|tp|tr|tt|tv|tw|tz|ua|ug|uk|us|uy|va|vc|ve|vg|vn|vu|wf|ws|ye|yu|za|zm|zr|zw|".IndexOf("|" + str + "|") > 0)
                {
                    return (strArray[strArray.Length - 3] + "." + str);
                }
            }
            if (strArray.Length >= 2)
            {
                str = strArray[strArray.Length - 1];
                if ("||com.cn|net.cn|org.cn|gov.cn|com.hk|公司|中国|网络|com|net|org|int|edu|gov|mil|arpa|Asia|biz|info|name|pro|coop|aero|museum|ac|ad|ae|af|ag|ai|al|am|an|ao|aq|ar|as|at|au|aw|az|ba|bb|bd|be|bf|bg|bh|bi|bj|bm|bn|bo|br|bs|bt|bv|bw|by|bz|ca|cc|cf|cg|ch|ci|ck|cl|cm|cn|co|cq|cr|cu|cv|cx|cy|cz|de|dj|dk|dm|do|dz|ec|ee|eg|eh|es|et|ev|fi|fj|fk|fm|fo|fr|ga|gb|gd|ge|gf|gh|gi|gl|gm|gn|gp|gr|gt|gu|gw|gy|hk|hm|hn|hr|ht|hu|id|ie|il|in|io|iq|ir|is|it|jm|jo|jp|ke|kg|kh|ki|km|kn|kp|kr|kw|ky|kz|la|lb|lc|li|lk|lr|ls|lt|lu|lv|ly|ma|mc|md|me|mg|mh|ml|mm|mn|mo|mp|mq|mr|ms|mt|mv|mw|mx|my|mz|na|nc|ne|nf|ng|ni|nl|no|np|nr|nt|nu|nz|om|pa|pe|pf|pg|ph|pk|pl|pm|pn|pr|pt|pw|py|qa|re|ro|ru|rw|sa|sb|sc|sd|se|sg|sh|si|sj|sk|sl|sm|sn|so|sr|st|su|sy|sz|tc|td|tf|tg|th|tj|tk|tm|tn|to|tp|tr|tt|tv|tw|tz|ua|ug|uk|us|uy|va|vc|ve|vg|vn|vu|wf|ws|ye|yu|za|zm|zr|zw|".IndexOf("|" + str + "|") > 0)
                {
                    return (strArray[strArray.Length - 2] + "." + str);
                }
            }
            return domain;
        }

        public static string HostPath(Uri uri)
        {
            if (uri == null)
            {
                return string.Empty;
            }
            string str = (uri.Port == 80) ? string.Empty : (":" + uri.Port.ToString(CultureInfo.InvariantCulture));
            return string.Format(CultureInfo.InvariantCulture, "{0}://{1}{2}", new object[] { uri.Scheme, uri.Host, str });
        }

        public static string HtmlDecode(object target)
        {
            if (StringPlus.IsNullOrEmpty(target))
            {
                return "";
            }
            return HttpUtility.HtmlDecode(target.ToString().Trim());
        }

        public static string HtmlDecodeForSpaceWrap(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }
            return HttpUtility.HtmlDecode(content).Replace("<br />", "\n").Replace("&nbsp;", " ");
        }

        public static string HtmlEncode(object target)
        {
            if (StringPlus.IsNullOrEmpty(target))
            {
                return "";
            }
            return HttpUtility.HtmlEncode(target.ToString().Trim());
        }

        public static string HtmlEncodeForSpaceWrap(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }
            return HttpUtility.HtmlEncode(content).Replace(" ", "&nbsp;").Replace("\n", "<br />");
        }

        private static bool IsNumeric(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            int length = value.Length;
            if ((('-' != value[0]) && ('+' != value[0])) && !char.IsNumber(value[0]))
            {
                return false;
            }
            for (int i = 1; i < length; i++)
            {
                if (!char.IsNumber(value[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static int PageCount(int count, int pageye)
        {
            int num = 0;
            int num2 = pageye;
            if ((count % num2) == 0)
            {
                num = count / num2;
            }
            else
            {
                num = (count / num2) + 1;
            }
            if (num == 0)
            {
                num++;
            }
            return num;
        }

        public static void RedirectToSSL(HttpContext context)
        {
            if ((context != null) && !context.Request.IsSecureConnection)
            {
                Uri url = context.Request.Url;
                context.Response.Redirect("https://" + url.ToString().Substring(7));
            }
        }

        public static bool? SafeBool(object target, bool? defaultValue)
        {
            if (target == null)
            {
                return defaultValue;
            }
            string str = target.ToString();
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            return SafeBool(str, defaultValue);
        }

        public static bool SafeBool(object target, bool defaultValue)
        {
            if (target == null)
            {
                return defaultValue;
            }
            string str = target.ToString();
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            return SafeBool(str, defaultValue);
        }

        public static bool SafeBool(string text, bool defaultValue)
        {
            bool flag;
            if (bool.TryParse(text, out flag))
            {
                defaultValue = flag;
            }
            return defaultValue;
        }

        public static bool? SafeBool(string text, bool? defaultValue)
        {
            bool flag;
            if (bool.TryParse(text, out flag))
            {
                defaultValue = new bool?(flag);
            }
            return defaultValue;
        }

        public static DateTime SafeDateTime(object target, DateTime defaultValue)
        {
            if (target == null)
            {
                return defaultValue;
            }
            string str = target.ToString();
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            return SafeDateTime(str, defaultValue);
        }

        public static DateTime? SafeDateTime(object target, DateTime? defaultValue)
        {
            if (target == null)
            {
                return defaultValue;
            }
            string str = target.ToString();
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            return SafeDateTime(str, defaultValue);
        }

        public static DateTime? SafeDateTime(string text, DateTime? defaultValue)
        {
            DateTime time;
            if (DateTime.TryParse(text, out time))
            {
                defaultValue = new DateTime?(time);
            }
            return defaultValue;
        }

        public static DateTime SafeDateTime(string text, DateTime defaultValue)
        {
            DateTime time;
            if (DateTime.TryParse(text, out time))
            {
                defaultValue = time;
            }
            return defaultValue;
        }

        public static decimal? SafeDecimal(object target, decimal? defaultValue)
        {
            if (target == null)
            {
                return defaultValue;
            }
            string str = target.ToString();
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            return SafeDecimal(str, defaultValue);
        }

        public static decimal SafeDecimal(object target, decimal defaultValue)
        {
            if (target == null)
            {
                return defaultValue;
            }
            string str = target.ToString();
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            return SafeDecimal(str, defaultValue);
        }

        public static decimal SafeDecimal(string text, decimal defaultValue)
        {
            decimal num;
            if (decimal.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static decimal? SafeDecimal(string text, decimal? defaultValue)
        {
            decimal num;
            if (decimal.TryParse(text, out num))
            {
                defaultValue = new decimal?(num);
            }
            return defaultValue;
        }

        public static T SafeEnum<T>(string value, T defaultValue) where T: struct
        {
            return SafeEnum<T>(value, defaultValue, false);
        }

        public static T SafeEnum<T>(string value, T defaultValue, bool ignoreCase) where T: struct
        {
            T local;
            if (Enum.TryParse<T>(value, ignoreCase, out local) && Enum.IsDefined(typeof(T), local))
            {
                defaultValue = local;
            }
            return defaultValue;
        }

        public static int? SafeInt(object target, int? defaultValue)
        {
            if (target == null)
            {
                return defaultValue;
            }
            string str = target.ToString();
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            return SafeInt(str, defaultValue);
        }

        public static int SafeInt(object target, int defaultValue)
        {
            if (target == null)
            {
                return defaultValue;
            }
            string str = target.ToString();
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            return SafeInt(str, defaultValue);
        }

        public static int SafeInt(string text, int defaultValue)
        {
            int num;
            if (int.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static int? SafeInt(string text, int? defaultValue)
        {
            int num;
            if (int.TryParse(text, out num))
            {
                defaultValue = new int?(num);
            }
            return defaultValue;
        }

        public static long? SafeLong(object target, long? defaultValue)
        {
            if (target == null)
            {
                return defaultValue;
            }
            string str = target.ToString();
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            return SafeLong(str, defaultValue);
        }

        public static long SafeLong(object target, long defaultValue)
        {
            if (target == null)
            {
                return defaultValue;
            }
            string str = target.ToString();
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            return SafeLong(str, defaultValue);
        }

        public static long? SafeLong(string text, long? defaultValue)
        {
            long num;
            if (long.TryParse(text, out num))
            {
                defaultValue = new long?(num);
            }
            return defaultValue;
        }

        public static long SafeLong(string text, long defaultValue)
        {
            long num;
            if (long.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static short SafeShort(object target, short defaultValue)
        {
            if (target == null)
            {
                return defaultValue;
            }
            string str = target.ToString();
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            return SafeShort(str, defaultValue);
        }

        public static short? SafeShort(object target, short? defaultValue)
        {
            if (target == null)
            {
                return defaultValue;
            }
            string str = target.ToString();
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            return SafeShort(str, defaultValue);
        }

        public static short SafeShort(string text, short defaultValue)
        {
            short num;
            if (short.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static short? SafeShort(string text, short? defaultValue)
        {
            short num;
            if (short.TryParse(text, out num))
            {
                defaultValue = new short?(num);
            }
            return defaultValue;
        }

        public static string SafeString(object target, string defaultValue)
        {
            if ((target != null) && ("" != target.ToString()))
            {
                return target.ToString();
            }
            return defaultValue;
        }

        public static string StripAllTags(string strToStrip)
        {
            strToStrip = Regex.Replace(strToStrip, @"</p(?:\s*)>(?:\s*)<p(?:\s*)>", "\n\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            strToStrip = Regex.Replace(strToStrip, @"<br(?:\s*)/>", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            strToStrip = Regex.Replace(strToStrip, "\"", "''", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            strToStrip = StripHtmlXmlTags(strToStrip);
            return strToStrip;
        }

        public static string StripForPreview(string content)
        {
            content = Regex.Replace(content, "<br>", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<br/>", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<br />", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<p>", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            content = content.Replace("'", "&#39;");
            return StripHtmlXmlTags(content);
        }

        public static string StripHtmlXmlTags(string content)
        {
            return Regex.Replace(content, "<[^>]+>", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public static string StripScriptTags(string content)
        {
            content = Regex.Replace(content, "<script((.|\n)*?)</script>", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "'javascript:", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return Regex.Replace(content, "\"javascript:", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }

        public static string ToDelimitedString(ICollection collection, string delimiter)
        {
            if (collection == null)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            if (collection is Hashtable)
            {
                foreach (object obj2 in ((Hashtable) collection).Keys)
                {
                    builder.Append(obj2.ToString() + delimiter);
                }
            }
            if (collection is ArrayList)
            {
                foreach (object obj3 in (ArrayList) collection)
                {
                    builder.Append(obj3.ToString() + delimiter);
                }
            }
            if (collection is string[])
            {
                foreach (string str in (string[]) collection)
                {
                    builder.Append(str + delimiter);
                }
            }
            if (collection is MailAddressCollection)
            {
                foreach (MailAddress address in (MailAddressCollection) collection)
                {
                    builder.Append(address.Address + delimiter);
                }
            }
            return builder.ToString().TrimEnd(new char[] { Convert.ToChar(delimiter, CultureInfo.InvariantCulture) });
        }

        public static string UnHtmlEncode(string formattedPost)
        {
            RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase;
            formattedPost = Regex.Replace(formattedPost, "&quot;", "\"", options);
            formattedPost = Regex.Replace(formattedPost, "&lt;", "<", options);
            formattedPost = Regex.Replace(formattedPost, "&gt;", ">", options);
            return formattedPost;
        }

        public static string UrlDecode(string urlToDecode)
        {
            if (string.IsNullOrEmpty(urlToDecode))
            {
                return urlToDecode;
            }
            return HttpUtility.UrlDecode(urlToDecode, Encoding.UTF8);
        }

        public static string UrlEncode(string urlToEncode)
        {
            if (string.IsNullOrEmpty(urlToEncode))
            {
                return urlToEncode;
            }
            return HttpUtility.UrlEncode(urlToEncode, Encoding.UTF8);
        }

        public static string ApplicationPath
        {
            get
            {
                string applicationPath = "/";
                if (HttpContext.Current != null)
                {
                    applicationPath = HttpContext.Current.Request.ApplicationPath;
                }
                if (applicationPath == "/")
                {
                    return string.Empty;
                }
                return applicationPath.ToLower(CultureInfo.InvariantCulture);
            }
        }

        public static string ClientIP
        {
            get
            {
                string userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(userHostAddress))
                {
                    userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (string.IsNullOrEmpty(userHostAddress))
                {
                    userHostAddress = HttpContext.Current.Request.UserHostAddress;
                }
                return userHostAddress;
            }
        }

        public static string DomainFullName
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                return HttpContext.Current.Request.Url.Authority;
            }
        }

        public static string DomainName
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                return HttpContext.Current.Request.Url.Host;
            }
        }

        public static bool IsPublicSession
        {
            get
            {
                return false;
            }
        }

        public static string TopLevelDomain
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                return GetTopLevelDomain(HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToLower());
            }
        }
    }
}

