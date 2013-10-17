namespace Maticsoft.Json
{
    using System;
    using System.Threading;

    public abstract class JsonReader : IDisposable
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

        protected JsonReader()
        {
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

        public bool MoveToContent()
        {
            if (this.EOF)
            {
                return false;
            }
            if (this.TokenClass.IsTerminator)
            {
                return this.Read();
            }
            return true;
        }

        private void OnDisposed(EventArgs e)
        {
            EventHandler disposed = this.Disposed;
            if (disposed != null)
            {
                disposed(this, e);
            }
        }

        public abstract bool Read();
        public bool ReadBoolean()
        {
            return (this.ReadToken(JsonTokenClass.Boolean) == "true");
        }

        public string ReadMember()
        {
            return this.ReadToken(JsonTokenClass.Member);
        }

        public void ReadNull()
        {
            this.ReadToken(JsonTokenClass.Null);
        }

        public JsonNumber ReadNumber()
        {
            return new JsonNumber(this.ReadToken(JsonTokenClass.Number));
        }

        public string ReadString()
        {
            return this.ReadToken(JsonTokenClass.String);
        }

        public string ReadToken(JsonTokenClass token)
        {
            string text;
            int depth = this.Depth;
            if (!token.IsTerminator)
            {
                this.MoveToContent();
            }
            if (((depth == 0) && (this.TokenClass == JsonTokenClass.Array)) && (token.IsScalar || (token == JsonTokenClass.Null)))
            {
                this.Read();
                text = this.ReadToken(token);
                this.ReadToken(JsonTokenClass.EndArray);
                return text;
            }
            if (this.TokenClass != token)
            {
                throw new JsonException(string.Format("Found {0} where {1} was expected.", this.TokenClass, token));
            }
            text = this.Text;
            this.Read();
            return text;
        }

        public void Skip()
        {
            if (this.MoveToContent())
            {
                if ((this.TokenClass == JsonTokenClass.Object) || (this.TokenClass == JsonTokenClass.Array))
                {
                    this.StepOut();
                }
                else if (this.TokenClass == JsonTokenClass.Member)
                {
                    this.Read();
                    this.Skip();
                }
                else
                {
                    this.Read();
                }
            }
        }

        public void StepOut()
        {
            int depth = this.Depth;
            if (depth == 0)
            {
                throw new InvalidOperationException();
            }
            while ((this.Depth > depth) || ((this.TokenClass != JsonTokenClass.EndObject) && (this.TokenClass != JsonTokenClass.EndArray)))
            {
                this.Read();
            }
            this.Read();
        }

        void IDisposable.Dispose()
        {
            this.Close();
        }

        public override string ToString()
        {
            return this.Token.ToString();
        }

        public abstract int Depth { get; }

        public bool EOF
        {
            get
            {
                return (this.TokenClass == JsonTokenClass.EOF);
            }
        }

        public abstract int MaxDepth { get; set; }

        public string Text
        {
            get
            {
                return this.Token.Text;
            }
        }

        public abstract JsonToken Token { get; }

        public JsonTokenClass TokenClass
        {
            get
            {
                return this.Token.Class;
            }
        }
    }
}

