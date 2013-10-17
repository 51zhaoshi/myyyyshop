namespace Maticsoft.Json
{
    using System;
    using System.IO;

    public class JsonTextWriter : JsonWriterBase
    {
        private int _indent;
        private char[] _indentBuffer;
        private bool _newLine;
        private bool _prettyPrint;
        private readonly TextWriter _writer;

        public JsonTextWriter() : this(null)
        {
        }

        public JsonTextWriter(TextWriter writer)
        {
            this._writer = (writer != null) ? writer : new StringWriter();
        }

        public override void Flush()
        {
            this._writer.Flush();
        }

        private bool IsNonEmptyArray()
        {
            return ((this.Bracket == JsonWriterBracket.Array) && (this.Index > 0));
        }

        private void OnWritingValue()
        {
            if (this.IsNonEmptyArray())
            {
                this.WriteDelimiter(',');
                this.PrettySpace();
            }
        }

        private void PrettyIndent()
        {
            if (this._prettyPrint && this._newLine)
            {
                if (this._indent > 0)
                {
                    int count = this._indent * 4;
                    if ((this._indentBuffer == null) || (this._indentBuffer.Length < count))
                    {
                        this._indentBuffer = new string(' ', count * 4).ToCharArray();
                    }
                    this._writer.Write(this._indentBuffer, 0, count);
                }
                this._newLine = false;
            }
        }

        private void PrettyLine()
        {
            if (this._prettyPrint)
            {
                this._writer.WriteLine();
                this._newLine = true;
            }
        }

        private void PrettySpace()
        {
            if (this._prettyPrint)
            {
                this.WriteDelimiter(' ');
            }
        }

        public override string ToString()
        {
            StringWriter writer = this._writer as StringWriter;
            if (writer == null)
            {
                return base.ToString();
            }
            return writer.ToString();
        }

        protected override void WriteBooleanImpl(bool value)
        {
            this.WriteScalar(value ? "true" : "false");
        }

        private void WriteDelimiter(char ch)
        {
            this.PrettyIndent();
            this._writer.Write(ch);
        }

        protected override void WriteEndArrayImpl()
        {
            if (this.IsNonEmptyArray())
            {
                this.PrettySpace();
            }
            this.WriteDelimiter(']');
        }

        protected override void WriteEndObjectImpl()
        {
            if (this.Index > 0)
            {
                this.PrettyLine();
                this._indent--;
            }
            this.WriteDelimiter('}');
        }

        protected override void WriteMemberImpl(string name)
        {
            if (this.Index > 0)
            {
                this.WriteDelimiter(',');
                this.PrettyLine();
            }
            else
            {
                this.PrettyLine();
                this._indent++;
            }
            this.WriteStringImpl(name);
            this.WriteDelimiter(':');
            this.PrettySpace();
        }

        protected override void WriteNullImpl()
        {
            this.WriteScalar("null");
        }

        protected override void WriteNumberImpl(string value)
        {
            this.WriteScalar(value);
        }

        private void WriteScalar(string text)
        {
            this.OnWritingValue();
            this.PrettyIndent();
            this._writer.Write(text);
        }

        protected override void WriteStartArrayImpl()
        {
            this.OnWritingValue();
            this.WriteDelimiter('[');
            this.PrettySpace();
        }

        protected override void WriteStartObjectImpl()
        {
            this.OnWritingValue();
            this.WriteDelimiter('{');
            this.PrettySpace();
        }

        protected override void WriteStringImpl(string value)
        {
            this.WriteScalar(JsonString.Enquote(value));
        }

        protected TextWriter InnerWriter
        {
            get
            {
                return this._writer;
            }
        }

        public bool PrettyPrint
        {
            get
            {
                return this._prettyPrint;
            }
            set
            {
                this._prettyPrint = value;
            }
        }
    }
}

