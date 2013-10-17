namespace Maticsoft.Common
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web;

    public static class InjectionFilter
    {
        public static string Filter(string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                return SqlFilter(FlashFilter(XSSFilter(inputString)));
            }
            return inputString;
        }

        public static string FlashFilter(string htmlCode)
        {
            if (string.IsNullOrEmpty(htmlCode))
            {
                return htmlCode;
            }
            string pattern = @"\w*<OBJECT\s+.*(macromedia)[\s*|\S*]{1,1300}</OBJECT>";
            return Regex.Replace(htmlCode, pattern, "", RegexOptions.Multiline);
        }

        public static string HtmlFilter(string value)
        {
            string input = value;
            Regex regex = new Regex("<[^>]+>|</[^>]+>");
            return regex.Replace(input, "");
        }

        public static string QuoteFilter(string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                source = source.Replace("'", "");
                source = source.Replace("\"", "");
            }
            return source;
        }

        public static string SqlFilter(string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                source = source.Replace("'", "''").Replace(";", "；").Replace("(", "（").Replace(")", "）");
                source = Regex.Replace(source, "select", "", RegexOptions.IgnoreCase);
                source = Regex.Replace(source, "insert", "", RegexOptions.IgnoreCase);
                source = Regex.Replace(source, "update", "", RegexOptions.IgnoreCase);
                source = Regex.Replace(source, "delete", "", RegexOptions.IgnoreCase);
                source = Regex.Replace(source, "drop", "", RegexOptions.IgnoreCase);
                source = Regex.Replace(source, "truncate", "", RegexOptions.IgnoreCase);
                source = Regex.Replace(source, "declare", "", RegexOptions.IgnoreCase);
                source = Regex.Replace(source, "xp_cmdshell", "", RegexOptions.IgnoreCase);
                source = Regex.Replace(source, "/add", "", RegexOptions.IgnoreCase);
                source = Regex.Replace(source, "net user", "", RegexOptions.IgnoreCase);
                source = Regex.Replace(source, "exec", "", RegexOptions.IgnoreCase);
                source = Regex.Replace(source, "execute", "", RegexOptions.IgnoreCase);
                source = Regex.Replace(source, "xp_", "x p_", RegexOptions.IgnoreCase);
                source = Regex.Replace(source, "sp_", "s p_", RegexOptions.IgnoreCase);
                source = Regex.Replace(source, "0x", "0 x", RegexOptions.IgnoreCase);
            }
            return source;
        }

        public static string XSSFilter(string source)
        {
            Match match;
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }
            string input = HttpUtility.UrlDecode(source);
            string replacement = " onXXX =";
            string str3 = "";
            string pattern = "<[^<>]*>";
            string str5 = @"([\s]|[:])+[o]{1}[n]{1}\w*\s*={1}";
            string str6 = @"\s*((javascript)|(vbscript))\s*[:]?";
            string str7 = @"<([\s/])*script.*>";
            string str8 = @"<([\s/])*\S.+>";
            Regex regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex regex2 = new Regex(str5, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex regex3 = new Regex(str7, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex regex4 = new Regex(str6, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex regex5 = new Regex(str8, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            for (match = regex.Match(input); match.Success; match = match.NextMatch())
            {
                str3 = match.Groups[0].Value;
                if (regex3.Match(str3).Success)
                {
                    str3 = regex3.Replace(str3, "");
                    input = input.Replace(match.Groups[0].Value, str3);
                }
            }
            for (match = regex.Match(input); match.Success; match = match.NextMatch())
            {
                str3 = match.Groups[0].Value;
                if (regex2.Match(str3).Success)
                {
                    str3 = regex2.Replace(str3, replacement);
                    input = input.Replace(match.Groups[0].Value, str3);
                }
            }
            for (match = regex.Match(input); match.Success; match = match.NextMatch())
            {
                str3 = match.Groups[0].Value;
                if (regex4.Match(str3).Success)
                {
                    str3 = regex4.Replace(str3, "");
                    input = input.Replace(match.Groups[0].Value, str3);
                }
            }
            for (Match match2 = regex2.Match(input); match2.Success; match2 = match2.NextMatch())
            {
                str3 = match2.Groups[0].Value;
                str3 = regex2.Replace(str3, replacement);
                input = input.Replace(match2.Groups[0].Value, str3);
            }
            for (Match match5 = regex5.Match(input); match5.Success; match5 = match5.NextMatch())
            {
                str3 = match5.Groups[0].Value;
                str3 = regex5.Replace(str3, "");
                input = input.Replace(match5.Groups[0].Value, str3);
            }
            return input;
        }
    }
}

