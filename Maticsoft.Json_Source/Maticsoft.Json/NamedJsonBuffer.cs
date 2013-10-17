namespace Maticsoft.Json
{
    using System;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct NamedJsonBuffer
    {
        public static readonly NamedJsonBuffer Empty;
        private readonly string _name;
        private readonly JsonBuffer _buffer;
        public NamedJsonBuffer(string name, JsonBuffer buffer)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (buffer.IsEmpty)
            {
                throw new ArgumentException(null, "buffer");
            }
            this._name = Mask.NullString(name);
            this._buffer = buffer;
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }
        public JsonBuffer Buffer
        {
            get
            {
                return this._buffer;
            }
        }
        public bool IsEmpty
        {
            get
            {
                return ((this._name == null) && this._buffer.IsEmpty);
            }
        }
        public bool Equals(NamedJsonBuffer other)
        {
            return ((this.Name == other.Name) && this.Buffer.Equals(other.Buffer));
        }

        public override bool Equals(object obj)
        {
            return ((obj is NamedJsonBuffer) && this.Equals((NamedJsonBuffer) obj));
        }

        public override int GetHashCode()
        {
            if (!this.IsEmpty)
            {
                return (this.Name.GetHashCode() ^ this.Buffer.GetHashCode());
            }
            return 0;
        }

        public override string ToString()
        {
            if (!this.IsEmpty)
            {
                return (Mask.EmptyString(this.Name, "(anonymous)") + ": " + this.Buffer);
            }
            return string.Empty;
        }

        static NamedJsonBuffer()
        {
            Empty = new NamedJsonBuffer();
        }
    }
}

