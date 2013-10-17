namespace Maticsoft.Json
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct JsonBuffer
    {
        public static readonly JsonBuffer Empty;
        private readonly JsonBufferStorage _storage;
        private readonly int _start;
        private readonly int _end;
        private static readonly JsonBuffer _null;
        private static readonly JsonBuffer _true;
        private static readonly JsonBuffer _false;
        static JsonBuffer()
        {
            Empty = new JsonBuffer();
            JsonToken[] tokens = new JsonToken[] { JsonToken.Null(), JsonToken.True(), JsonToken.False() };
            JsonBuffer buffer = new JsonBufferStorage(5).Write(JsonToken.Array()).Write(tokens).Write(JsonToken.EndArray()).ToBuffer();
            _null = buffer.Slice(1, 2);
            _true = buffer.Slice(2, 3);
            _false = buffer.Slice(3, 4);
        }

        internal JsonBuffer(JsonBufferStorage storage, int start, int end)
        {
            this._storage = storage;
            this._start = start;
            this._end = end;
        }

        public static JsonBuffer From(string json)
        {
            return From(JsonText.CreateReader(json));
        }

        public static JsonBuffer From(JsonToken token)
        {
            JsonTokenClass class2 = token.Class;
            if (class2 == JsonTokenClass.Null)
            {
                return _null;
            }
            if (!class2.IsScalar)
            {
                throw new ArgumentException("Token must represent a JSON scalar value or null.", "token");
            }
            if (class2 == JsonTokenClass.Boolean)
            {
                if (!token.Equals(JsonToken.True()))
                {
                    return _false;
                }
                return _true;
            }
            JsonBufferStorage storage = new JsonBufferStorage(1);
            storage.Write(token);
            return storage.ToBuffer();
        }

        public static JsonBuffer From(JsonReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            JsonBufferReader reader2 = reader as JsonBufferReader;
            if (reader2 != null)
            {
                return reader2.BufferValue();
            }
            if (!reader.MoveToContent())
            {
                return Empty;
            }
            if (reader.TokenClass == JsonTokenClass.Member)
            {
                reader.Read();
            }
            bool flag = (reader.TokenClass == JsonTokenClass.Array) || (reader.TokenClass == JsonTokenClass.Object);
            JsonBufferWriter writer = new JsonBufferWriter();
            writer.WriteFromReader(reader);
            JsonBuffer buffer = writer.GetBuffer();
            if (!flag)
            {
                reader2 = buffer.CreateReader();
                reader2.MoveToContent();
                reader2.Read();
                buffer = reader2.BufferValue();
            }
            return buffer;
        }

        public JsonToken this[int index]
        {
            get
            {
                if ((index < 0) || (index >= this.Length))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                return this._storage[this._start + index];
            }
        }
        public int Length
        {
            get
            {
                return (this._end - this._start);
            }
        }
        private JsonToken FirstToken
        {
            get
            {
                return this._storage[this._start];
            }
        }
        public bool IsEmpty
        {
            get
            {
                return (this.Length == 0);
            }
        }
        public bool IsNull
        {
            get
            {
                return ((this.Length == 1) && (this.FirstToken.Class == JsonTokenClass.Null));
            }
        }
        public bool IsScalar
        {
            get
            {
                return ((this.Length == 1) && this.FirstToken.Class.IsScalar);
            }
        }
        public bool IsStructured
        {
            get
            {
                return ((!this.IsEmpty && !this.IsNull) && !this.IsScalar);
            }
        }
        public bool IsObject
        {
            get
            {
                return (this.IsStructured && (this.FirstToken.Class == JsonTokenClass.Object));
            }
        }
        public bool IsArray
        {
            get
            {
                return (this.IsStructured && (this.FirstToken.Class == JsonTokenClass.Array));
            }
        }
        public JsonBufferReader CreateReader()
        {
            if (this.IsEmpty)
            {
                throw new InvalidOperationException();
            }
            JsonBufferReader reader = new JsonBufferReader(this);
            if (!this.IsStructured)
            {
                reader.ReadToken(JsonTokenClass.Array);
            }
            return reader;
        }

        public bool GetBoolean()
        {
            return this.CreateReader().ReadBoolean();
        }

        public string GetString()
        {
            return this.CreateReader().ReadString();
        }

        public JsonNumber GetNumber()
        {
            return this.CreateReader().ReadNumber();
        }

        public int GetArrayLength()
        {
            return this.GetArray(null, 0, 0x7fffffff);
        }

        public JsonBuffer[] GetArray()
        {
            JsonBuffer[] values = new JsonBuffer[this.GetArrayLength()];
            this.GetArray(values);
            return values;
        }

        public int GetArray(JsonBuffer[] values)
        {
            return this.GetArray(values, 0, values.Length);
        }

        public int GetArray(JsonBuffer[] values, int index, int count)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", index, null);
            }
            JsonBufferReader reader = this.CreateReader();
            if (!reader.MoveToContent())
            {
                throw new JsonException("Unexpected EOF.");
            }
            if (reader.TokenClass == JsonTokenClass.Null)
            {
                return 0;
            }
            reader.ReadToken(JsonTokenClass.Array);
            int num = 0;
            while (reader.TokenClass != JsonTokenClass.EndArray)
            {
                if (count-- == 0)
                {
                    return ~num;
                }
                if (values != null)
                {
                    values[index++] = reader.BufferValue();
                }
                else
                {
                    reader.Skip();
                }
                num++;
            }
            return num;
        }

        public int GetMemberCount()
        {
            return this.GetMembers(null);
        }

        public int GetMembers(NamedJsonBuffer[] members)
        {
            return this.GetMembers(members, 0, (members != null) ? members.Length : 0x7fffffff);
        }

        public int GetMembers(NamedJsonBuffer[] members, int index, int count)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", index, null);
            }
            JsonBufferReader reader = this.CreateReader();
            if (!reader.MoveToContent())
            {
                throw new JsonException("Unexpected EOF.");
            }
            if (reader.TokenClass == JsonTokenClass.Null)
            {
                return 0;
            }
            reader.ReadToken(JsonTokenClass.Object);
            int num = 0;
            while (reader.TokenClass == JsonTokenClass.Member)
            {
                if (count-- == 0)
                {
                    return ~num;
                }
                if (members != null)
                {
                    members[index++] = new NamedJsonBuffer(reader.Text, reader.BufferValue());
                }
                else
                {
                    reader.Skip();
                }
                num++;
            }
            return num;
        }

        public NamedJsonBuffer[] GetMembersArray()
        {
            NamedJsonBuffer[] members = new NamedJsonBuffer[this.GetMemberCount()];
            this.GetMembers(members);
            return members;
        }

        public override int GetHashCode()
        {
            int num = 0;
            for (int i = 0; i < this.Length; i++)
            {
                JsonToken token = this[i];
                num ^= token.GetHashCode();
            }
            return num;
        }

        public override bool Equals(object obj)
        {
            return ((obj is JsonBuffer) && this.Equals((JsonBuffer) obj));
        }

        public bool Equals(JsonBuffer other)
        {
            if (this.Length != other.Length)
            {
                return false;
            }
            for (int i = 0; i < this.Length; i++)
            {
                JsonToken token = this[i];
                if (!token.Equals(other[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<NamedJsonBuffer> GetMembers()
        {
            JsonBufferReader iteratorVariable0 = this.CreateReader();
            iteratorVariable0.ReadToken(JsonTokenClass.Object);
            while (true)
            {
                if (iteratorVariable0.TokenClass != JsonTokenClass.Member)
                {
                    yield break;
                }
                yield return new NamedJsonBuffer(iteratorVariable0.Text, iteratorVariable0.BufferValue());
            }
        }

        public override string ToString()
        {
            if (this.IsEmpty)
            {
                return string.Empty;
            }
            if (this.IsNull)
            {
                return "null";
            }
            if (this.FirstToken.Class == JsonTokenClass.String)
            {
                return JsonString.Enquote(this.FirstToken.Text);
            }
            if (this.IsScalar)
            {
                return this.FirstToken.Text;
            }
            StringBuilder sb = new StringBuilder();
            JsonText.CreateWriter(sb).WriteFromReader(this.CreateReader());
            return sb.ToString();
        }

        public static bool operator ==(JsonBuffer lhs, JsonBuffer rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(JsonBuffer lhs, JsonBuffer rhs)
        {
            return !lhs.Equals(rhs);
        }

        internal JsonBuffer Slice(int start, int end)
        {
            if (start != end)
            {
                return new JsonBuffer(this._storage, start, end);
            }
            return Empty;
        }
        [CompilerGenerated]
        private sealed class <GetMembers>d__0 : IEnumerable<NamedJsonBuffer>, IEnumerable, IEnumerator<NamedJsonBuffer>, IEnumerator, IDisposable
        {
            private int <>1__state;
            private NamedJsonBuffer <>2__current;
            public JsonBuffer <>3__<>4__this;
            public JsonBuffer <>4__this;
            private int <>l__initialThreadId;
            public JsonBufferReader <reader>5__1;

            [DebuggerHidden]
            public <GetMembers>d__0(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
            }

            private bool MoveNext()
            {
                switch (this.<>1__state)
                {
                    case 0:
                        this.<>1__state = -1;
                        this.<reader>5__1 = this.<>4__this.CreateReader();
                        this.<reader>5__1.ReadToken(JsonTokenClass.Object);
                        break;

                    case 1:
                        this.<>1__state = -1;
                        break;

                    default:
                        goto Label_0085;
                }
                if (this.<reader>5__1.TokenClass == JsonTokenClass.Member)
                {
                    this.<>2__current = new NamedJsonBuffer(this.<reader>5__1.Text, this.<reader>5__1.BufferValue());
                    this.<>1__state = 1;
                    return true;
                }
            Label_0085:
                return false;
            }

            [DebuggerHidden]
            IEnumerator<NamedJsonBuffer> IEnumerable<NamedJsonBuffer>.GetEnumerator()
            {
                JsonBuffer.<GetMembers>d__0 d__;
                if ((Thread.CurrentThread.ManagedThreadId == this.<>l__initialThreadId) && (this.<>1__state == -2))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new JsonBuffer.<GetMembers>d__0(0) {
                        <>4__this = this.<>4__this
                    };
                }
                d__.<>4__this = this.<>3__<>4__this;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.System.Collections.Generic.IEnumerable<Maticsoft.Json.NamedJsonBuffer>.GetEnumerator();
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            void IDisposable.Dispose()
            {
            }

            NamedJsonBuffer IEnumerator<NamedJsonBuffer>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.<>2__current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.<>2__current;
                }
            }
        }
    }
}

