namespace Maticsoft.Json.Diagnostics
{
    using System;
    using System.Text;

    public sealed class DebugString
    {
        public static readonly char ControlReplacement = '?';
        public static readonly string Ellipsis = "…";

        private DebugString()
        {
            throw new NotSupportedException();
        }

        public static string Format(string s)
        {
            return Format(s, 50);
        }

        public static string Format(string s, int width)
        {
            if (s == null)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder(width);
            for (int i = 0; i < Math.Min(width, s.Length); i++)
            {
                builder.Append(!char.IsControl(s, i) ? s[i] : ControlReplacement);
            }
            if (s.Length > width)
            {
                builder.Remove(width - Ellipsis.Length, Ellipsis.Length);
                builder.Append(Ellipsis);
            }
            return builder.ToString();
        }
    }
}

