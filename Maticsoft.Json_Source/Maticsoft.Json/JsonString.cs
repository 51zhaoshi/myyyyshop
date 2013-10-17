namespace Maticsoft.Json
{
    using System;
    using System.Globalization;
    using System.Text;

    public sealed class JsonString
    {
        private JsonString()
        {
            throw new NotSupportedException();
        }

        internal static string Dequote(BufferedCharReader input, char quote)
        {
            return Dequote(input, quote, null).ToString();
        }

        internal static StringBuilder Dequote(BufferedCharReader input, char quote, StringBuilder output)
        {
            char ch;
            if (output == null)
            {
                output = new StringBuilder();
            }
            char[] hexDigits = null;
        Label_000C:
            ch = input.Next();
            switch (ch)
            {
                case '\0':
                case '\n':
                case '\r':
                    throw new FormatException("Unterminated string.");

                default:
                    if (ch != '\\')
                    {
                        if (ch == quote)
                        {
                            return output;
                        }
                        output.Append(ch);
                        goto Label_000C;
                    }
                    ch = input.Next();
                    switch (ch)
                    {
                        case 'r':
                            output.Append('\r');
                            goto Label_000C;

                        case 't':
                            output.Append('\t');
                            goto Label_000C;

                        case 'u':
                            if (hexDigits == null)
                            {
                                hexDigits = new char[4];
                            }
                            output.Append(ParseHex(input, hexDigits));
                            goto Label_000C;

                        case 'n':
                            output.Append('\n');
                            goto Label_000C;

                        case 'b':
                            output.Append('\b');
                            goto Label_000C;

                        case 'f':
                            output.Append('\f');
                            goto Label_000C;
                    }
                    break;
            }
            output.Append(ch);
            goto Label_000C;
        }

        public static string Enquote(string str)
        {
            if ((str != null) && (str.Length != 0))
            {
                return Enquote(str, null).ToString();
            }
            return "\"\"";
        }

        public static StringBuilder Enquote(string str, StringBuilder sb)
        {
            return EnquoteStringOrChars(str, null, 0, Mask.NullString(str).Length, sb);
        }

        private static void Enquote(StringBuilder sb, char last, char ch)
        {
            char ch2 = ch;
            if (ch2 <= '"')
            {
                switch (ch2)
                {
                    case '\b':
                        sb.Append(@"\b");
                        return;

                    case '\t':
                        sb.Append(@"\t");
                        return;

                    case '\n':
                        sb.Append(@"\n");
                        return;

                    case '\v':
                        goto Label_00A2;

                    case '\f':
                        sb.Append(@"\f");
                        return;

                    case '\r':
                        sb.Append(@"\r");
                        return;

                    case '"':
                        goto Label_0038;
                }
                goto Label_00A2;
            }
            if (ch2 == '/')
            {
                if (last == '<')
                {
                    sb.Append('\\');
                }
                sb.Append(ch);
                return;
            }
            if (ch2 != '\\')
            {
                goto Label_00A2;
            }
        Label_0038:
            sb.Append('\\');
            sb.Append(ch);
            return;
        Label_00A2:
            if (ch < ' ')
            {
                sb.Append(@"\u");
                sb.Append(((int) ch).ToString("x4", CultureInfo.InvariantCulture));
            }
            else
            {
                sb.Append(ch);
            }
        }

        public static string Enquote(char[] chars, int offset, int length)
        {
            return Enquote(chars, offset, length, null).ToString();
        }

        public static StringBuilder Enquote(char[] chars, int offset, int length, StringBuilder sb)
        {
            if (chars == null)
            {
                throw new ArgumentNullException("chars");
            }
            return EnquoteStringOrChars(null, chars, offset, length, sb);
        }

        private static StringBuilder EnquoteStringOrChars(string str, char[] chars, int offset, int length, StringBuilder sb)
        {
            if (chars != null)
            {
                if (offset < 0)
                {
                    throw new ArgumentOutOfRangeException("offset", offset, null);
                }
                if (length < 0)
                {
                    throw new ArgumentOutOfRangeException("length", offset, null);
                }
            }
            if (sb == null)
            {
                sb = new StringBuilder(length + 4);
            }
            sb.Append('"');
            char ch2 = '\0';
            int num = offset + length;
            if ((chars != null) && (num > chars.Length))
            {
                throw new ArgumentOutOfRangeException();
            }
            for (int i = offset; i < num; i++)
            {
                char last = ch2;
                ch2 = (chars != null) ? chars[i] : str[i];
                Enquote(sb, last, ch2);
            }
            return sb.Append('"');
        }

        private static char ParseHex(BufferedCharReader input, char[] hexDigits)
        {
            hexDigits[0] = input.Next();
            hexDigits[1] = input.Next();
            hexDigits[2] = input.Next();
            hexDigits[3] = input.Next();
            return (char) ushort.Parse(new string(hexDigits), NumberStyles.HexNumber);
        }
    }
}

