namespace Maticsoft.Json
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    public sealed class JsonTextReader : JsonReaderBase
    {
        private Continuation _methodParse;
        private Continuation _methodParseArrayFirst;
        private Continuation _methodParseArrayNext;
        private Continuation _methodParseNextMember;
        private Continuation _methodParseObjectMember;
        private Continuation _methodParseObjectMemberValue;
        private static readonly char[] _numNonDigitChars = new char[] { '.', 'e', 'E', '+', '-' };
        private BufferedCharReader _reader;
        private Stack _stack;

        public JsonTextReader(TextReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            this._reader = new BufferedCharReader(reader);
            this.Push(this.ParseMethod);
        }

        private char NextClean()
        {
            char ch;
        Label_0000:
            ch = this._reader.Next();
            if (ch != '/')
            {
                if (ch != '#')
                {
                    if ((ch == '\0') || (ch > ' '))
                    {
                        return ch;
                    }
                }
                else
                {
                    do
                    {
                        ch = this._reader.Next();
                        switch (ch)
                        {
                            case '\n':
                            case '\r':
                                goto Label_0000;
                        }
                    }
                    while (ch != '\0');
                }
                goto Label_0000;
            }
            char ch2 = this._reader.Next();
            if (ch2 != '*')
            {
                if (ch2 != '/')
                {
                    goto Label_007E;
                }
                do
                {
                    switch (this._reader.Next())
                    {
                        case '\n':
                        case '\r':
                            goto Label_0000;
                    }
                }
                while (ch != '\0');
                goto Label_0000;
            }
            while (true)
            {
                switch (this._reader.Next())
                {
                    case '\0':
                        throw this.SyntaxError("Unclosed comment.");

                    case '*':
                        if (this._reader.Next() == '/')
                        {
                            goto Label_0000;
                        }
                        this._reader.Back();
                        break;
                }
            }
        Label_007E:
            this._reader.Back();
            return '/';
        }

        private string NextString(char quote)
        {
            string str;
            try
            {
                str = JsonString.Dequote(this._reader, quote);
            }
            catch (FormatException exception)
            {
                throw this.SyntaxError(exception.Message, exception);
            }
            return str;
        }

        private JsonToken Parse()
        {
            char quote = this.NextClean();
            switch (quote)
            {
                case '"':
                case '\'':
                    return this.Yield(JsonToken.String(this.NextString(quote)));

                case '{':
                    this._reader.Back();
                    return this.ParseObject();

                case '[':
                    this._reader.Back();
                    return this.ParseArray();
            }
            StringBuilder builder = new StringBuilder();
            char ch2 = quote;
            while ((quote >= ' ') && (",:]}/\\\"[{;=#".IndexOf(quote) < 0))
            {
                builder.Append(quote);
                quote = this._reader.Next();
            }
            this._reader.Back();
            string text = builder.ToString().Trim();
            if (text.Length == 0)
            {
                throw this.SyntaxError("Missing value.");
            }
            if ((text == "true") || (text == "false"))
            {
                return this.Yield(JsonToken.Boolean(text == "true"));
            }
            if (text == "null")
            {
                return this.Yield(JsonToken.Null());
            }
            if (((ch2 >= '0') && (ch2 <= '9')) || (((ch2 == '.') || (ch2 == '-')) || (ch2 == '+')))
            {
                if (((ch2 != '0') || (text.Length <= 1)) || (text.IndexOfAny(_numNonDigitChars) >= 0))
                {
                    if (!JsonNumber.IsValid(text))
                    {
                        throw this.SyntaxError(string.Format("The text '{0}' has the incorrect syntax for a number.", text));
                    }
                    return this.Yield(JsonToken.Number(text));
                }
                if ((text.Length <= 2) || ((text[1] != 'x') && (text[1] != 'X')))
                {
                    string objA = TryParseOctal(text);
                    if (!object.ReferenceEquals(objA, text))
                    {
                        return this.Yield(JsonToken.Number(objA));
                    }
                }
                else
                {
                    string str2 = TryParseHex(text);
                    if (!object.ReferenceEquals(str2, text))
                    {
                        return this.Yield(JsonToken.Number(str2));
                    }
                }
            }
            return this.Yield(JsonToken.String(text));
        }

        private JsonToken ParseArray()
        {
            if (this.NextClean() != '[')
            {
                throw this.SyntaxError("An array must start with '['.");
            }
            return this.Yield(JsonToken.Array(), this.ParseArrayFirstMethod);
        }

        private JsonToken ParseArrayFirst()
        {
            if (this.NextClean() == ']')
            {
                return this.Yield(JsonToken.EndArray());
            }
            this._reader.Back();
            this.Push(this.ParseArrayNextMethod);
            return this.Parse();
        }

        private JsonToken ParseArrayNext()
        {
            switch (this.NextClean())
            {
                case ',':
                case ';':
                    if (this.NextClean() == ']')
                    {
                        return this.Yield(JsonToken.EndArray());
                    }
                    this._reader.Back();
                    this.Push(this.ParseArrayNextMethod);
                    return this.Parse();

                case ']':
                    return this.Yield(JsonToken.EndArray());
            }
            throw this.SyntaxError("Expected a ',' or ']'.");
        }

        private JsonToken ParseNextMember()
        {
            switch (this.NextClean())
            {
                case ',':
                case ';':
                    if (this.NextClean() != '}')
                    {
                        this._reader.Back();
                        string text = this.Parse().Text;
                        return this.Yield(JsonToken.Member(text), this.ParseObjectMemberValueMethod);
                    }
                    return this.Yield(JsonToken.EndObject());

                case '}':
                    return this.Yield(JsonToken.EndObject());
            }
            throw this.SyntaxError("Expected a ',' or '}'.");
        }

        private JsonToken ParseObject()
        {
            if (this.NextClean() != '{')
            {
                throw this.SyntaxError("An object must begin with '{'.");
            }
            return this.Yield(JsonToken.Object(), this.ParseObjectMemberMethod);
        }

        private JsonToken ParseObjectMember()
        {
            switch (this.NextClean())
            {
                case '}':
                    return this.Yield(JsonToken.EndObject());

                case '\0':
                    throw this.SyntaxError("An object must end with '}'.");
            }
            this._reader.Back();
            string text = this.Parse().Text;
            return this.Yield(JsonToken.Member(text), this.ParseObjectMemberValueMethod);
        }

        private JsonToken ParseObjectMemberValue()
        {
            char ch = this.NextClean();
            if (ch == '=')
            {
                if (this._reader.Next() != '>')
                {
                    this._reader.Back();
                }
            }
            else if (ch != ':')
            {
                throw this.SyntaxError("Expected a ':' after a key.");
            }
            this.Push(this.ParseNextMemberMethod);
            return this.Parse();
        }

        private Continuation Pop()
        {
            return (Continuation) this._stack.Pop();
        }

        private void Push(Continuation continuation)
        {
            if (this._stack == null)
            {
                this._stack = new Stack(6);
            }
            this._stack.Push(continuation);
        }

        protected override JsonToken ReadTokenImpl()
        {
            if (this._stack == null)
            {
                return JsonToken.EOF();
            }
            if (this._stack.Count == 0)
            {
                this._stack = null;
                this._reader = null;
                return JsonToken.EOF();
            }
            return this.Pop()();
        }

        private JsonException SyntaxError(string message)
        {
            return this.SyntaxError(message, null);
        }

        private JsonException SyntaxError(string message, Exception inner)
        {
            if (this.LineNumber > 0)
            {
                message = string.Format("{0} See line {1}, position {2}.", message, this.LineNumber.ToString("N0"), this.LinePosition.ToString("N0"));
            }
            return new JsonException(message, inner);
        }

        private static string TryParseHex(string s)
        {
            try
            {
                return long.Parse(s.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
            }
            catch (OverflowException)
            {
                return s;
            }
            catch (FormatException)
            {
                return s;
            }
        }

        private static string TryParseOctal(string s)
        {
            long num = 0L;
            try
            {
                for (int i = 1; i < s.Length; i++)
                {
                    char ch = s[i];
                    if ((ch < '0') || (ch > '8'))
                    {
                        return s;
                    }
                    num = (num * 8L) | (ch - '0');
                }
            }
            catch (OverflowException)
            {
                return s;
            }
            return num.ToString(CultureInfo.InvariantCulture);
        }

        private JsonToken Yield(JsonToken token)
        {
            return this.Yield(token, null);
        }

        private JsonToken Yield(JsonToken token, Continuation continuation)
        {
            if (continuation != null)
            {
                this.Push(continuation);
            }
            return token;
        }

        public int LineNumber
        {
            get
            {
                return this._reader.LineNumber;
            }
        }

        public int LinePosition
        {
            get
            {
                return this._reader.LinePosition;
            }
        }

        private Continuation ParseArrayFirstMethod
        {
            get
            {
                if (this._methodParseArrayFirst == null)
                {
                    this._methodParseArrayFirst = new Continuation(this.ParseArrayFirst);
                }
                return this._methodParseArrayFirst;
            }
        }

        private Continuation ParseArrayNextMethod
        {
            get
            {
                if (this._methodParseArrayNext == null)
                {
                    this._methodParseArrayNext = new Continuation(this.ParseArrayNext);
                }
                return this._methodParseArrayNext;
            }
        }

        private Continuation ParseMethod
        {
            get
            {
                if (this._methodParse == null)
                {
                    this._methodParse = new Continuation(this.Parse);
                }
                return this._methodParse;
            }
        }

        private Continuation ParseNextMemberMethod
        {
            get
            {
                if (this._methodParseNextMember == null)
                {
                    this._methodParseNextMember = new Continuation(this.ParseNextMember);
                }
                return this._methodParseNextMember;
            }
        }

        private Continuation ParseObjectMemberMethod
        {
            get
            {
                if (this._methodParseObjectMember == null)
                {
                    this._methodParseObjectMember = new Continuation(this.ParseObjectMember);
                }
                return this._methodParseObjectMember;
            }
        }

        private Continuation ParseObjectMemberValueMethod
        {
            get
            {
                if (this._methodParseObjectMemberValue == null)
                {
                    this._methodParseObjectMemberValue = new Continuation(this.ParseObjectMemberValue);
                }
                return this._methodParseObjectMemberValue;
            }
        }

        private delegate JsonToken Continuation();
    }
}

