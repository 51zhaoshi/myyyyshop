namespace Maticsoft.Common.Mime
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    public static class QuotedPrintableEncoding
    {
        private const string Equal = "=";
        private const string HexPattern = @"(\=([0-9A-F][0-9A-F]))";

        public static string Decode(string contents)
        {
            if (contents == null)
            {
                throw new ArgumentNullException("contents");
            }
            using (StringWriter writer = new StringWriter())
            {
                using (StringReader reader = new StringReader(contents))
                {
                    string str;
                    while ((str = reader.ReadLine()) != null)
                    {
                        str.TrimEnd(new char[0]);
                        if (str.EndsWith("="))
                        {
                            writer.Write(DecodeLine(str));
                        }
                        else
                        {
                            writer.WriteLine(DecodeLine(str));
                        }
                    }
                }
                writer.Flush();
                return writer.ToString();
            }
        }

        private static string DecodeLine(string line)
        {
            if (line == null)
            {
                throw new ArgumentNullException("line");
            }
            Regex regex = new Regex(@"(\=([0-9A-F][0-9A-F]))", RegexOptions.IgnoreCase);
            return regex.Replace(line, new MatchEvaluator(QuotedPrintableEncoding.HexMatchEvaluator));
        }

        private static string HexMatchEvaluator(Match m)
        {
            return Convert.ToChar(Convert.ToInt32(m.Groups[2].Value, 0x10)).ToString();
        }
    }
}

