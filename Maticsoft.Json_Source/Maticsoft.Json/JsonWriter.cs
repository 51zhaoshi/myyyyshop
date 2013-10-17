namespace Maticsoft.Json
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Threading;

    public abstract class JsonWriter : IDisposable
    {
        private EventHandler Disposed;

        public event EventHandler Disposed
        {
            add
            {
                EventHandler handler2;
                EventHandler disposed = this.Disposed;
                do
                {
                    handler2 = disposed;
                    EventHandler handler3 = (EventHandler) Delegate.Combine(handler2, value);
                    disposed = Interlocked.CompareExchange<EventHandler>(ref this.Disposed, handler3, handler2);
                }
                while (disposed != handler2);
            }
            remove
            {
                EventHandler handler2;
                EventHandler disposed = this.Disposed;
                do
                {
                    handler2 = disposed;
                    EventHandler handler3 = (EventHandler) Delegate.Remove(handler2, value);
                    disposed = Interlocked.CompareExchange<EventHandler>(ref this.Disposed, handler3, handler2);
                }
                while (disposed != handler2);
            }
        }

        protected JsonWriter()
        {
        }

        public void AutoComplete()
        {
            if (this.Depth == 0)
            {
                throw new InvalidOperationException();
            }
            if (this.Bracket == JsonWriterBracket.Member)
            {
                this.WriteNull();
            }
            while (this.Depth > 0)
            {
                if (this.Bracket != JsonWriterBracket.Object)
                {
                    if (this.Bracket != JsonWriterBracket.Array)
                    {
                        throw new Exception("Implementation error.");
                    }
                    this.WriteEndArray();
                }
                else
                {
                    this.WriteEndObject();
                    continue;
                }
            }
        }

        public virtual void Close()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.OnDisposed(EventArgs.Empty);
            }
        }

        public virtual void Flush()
        {
        }

        private void OnDisposed(EventArgs e)
        {
            EventHandler disposed = this.Disposed;
            if (disposed != null)
            {
                disposed(this, e);
            }
        }

        void IDisposable.Dispose()
        {
            this.Close();
        }

        public abstract void WriteBoolean(bool value);
        public abstract void WriteEndArray();
        public abstract void WriteEndObject();
        public virtual void WriteFromReader(JsonReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            if (reader.MoveToContent())
            {
                if (reader.TokenClass == JsonTokenClass.String)
                {
                    this.WriteString(reader.Text);
                }
                else if (reader.TokenClass == JsonTokenClass.Number)
                {
                    this.WriteNumber(reader.Text);
                }
                else if (reader.TokenClass == JsonTokenClass.Boolean)
                {
                    this.WriteBoolean(reader.Text == "true");
                }
                else if (reader.TokenClass == JsonTokenClass.Null)
                {
                    this.WriteNull();
                }
                else if (reader.TokenClass == JsonTokenClass.Array)
                {
                    this.WriteStartArray();
                    reader.Read();
                    while (reader.TokenClass != JsonTokenClass.EndArray)
                    {
                        this.WriteFromReader(reader);
                    }
                    this.WriteEndArray();
                }
                else
                {
                    if (reader.TokenClass != JsonTokenClass.Object)
                    {
                        throw new JsonException(string.Format("{0} not expected.", reader.TokenClass));
                    }
                    reader.Read();
                    this.WriteStartObject();
                    while (reader.TokenClass != JsonTokenClass.EndObject)
                    {
                        this.WriteMember(reader.ReadMember());
                        this.WriteFromReader(reader);
                    }
                    this.WriteEndObject();
                }
                reader.Read();
            }
        }

        public abstract void WriteMember(string name);
        public abstract void WriteNull();
        public void WriteNumber(byte value)
        {
            this.WriteNumber(value.ToString(CultureInfo.InvariantCulture));
        }

        public void WriteNumber(decimal value)
        {
            this.WriteNumber(value.ToString(CultureInfo.InvariantCulture));
        }

        public void WriteNumber(double value)
        {
            if (double.IsNaN(value))
            {
                throw new ArgumentOutOfRangeException("value");
            }
            this.WriteNumber(value.ToString(CultureInfo.InvariantCulture));
        }

        public void WriteNumber(short value)
        {
            this.WriteNumber(value.ToString(CultureInfo.InvariantCulture));
        }

        public void WriteNumber(int value)
        {
            this.WriteNumber(value.ToString(CultureInfo.InvariantCulture));
        }

        public void WriteNumber(long value)
        {
            this.WriteNumber(value.ToString(CultureInfo.InvariantCulture));
        }

        public void WriteNumber(float value)
        {
            if (float.IsNaN(value))
            {
                throw new ArgumentOutOfRangeException("value");
            }
            this.WriteNumber(value.ToString(CultureInfo.InvariantCulture));
        }

        public abstract void WriteNumber(string value);
        public abstract void WriteStartArray();
        public abstract void WriteStartObject();
        public void WriteString(char[] chars)
        {
            this.WriteString(chars, 0, chars.Length);
        }

        public abstract void WriteString(string value);
        public virtual void WriteString(char[] chars, int offset, int length)
        {
            this.WriteString(new string(chars, offset, length));
        }

        public void WriteStringArray(params string[] values)
        {
            if (values == null)
            {
                this.WriteNull();
            }
            else
            {
                this.WriteStartArray();
                foreach (string str in values)
                {
                    if (JsonNull.LogicallyEquals(str))
                    {
                        this.WriteNull();
                    }
                    else
                    {
                        this.WriteString(str);
                    }
                }
                this.WriteEndArray();
            }
        }

        public void WriteStringArray(IEnumerable values)
        {
            if (values == null)
            {
                this.WriteNull();
            }
            else
            {
                this.WriteStartArray();
                foreach (object obj2 in values)
                {
                    if (JsonNull.LogicallyEquals(obj2))
                    {
                        this.WriteNull();
                    }
                    else
                    {
                        this.WriteString(obj2.ToString());
                    }
                }
                this.WriteEndArray();
            }
        }

        public abstract JsonWriterBracket Bracket { get; }

        public abstract int Depth { get; }

        public abstract int Index { get; }

        public abstract int MaxDepth { get; set; }
    }
}

