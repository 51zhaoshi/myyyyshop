namespace Maticsoft.Json
{
    using System;

    public sealed class JsonBufferReader : JsonReaderBase
    {
        private readonly JsonBuffer _buffer;
        private int _index;

        public JsonBufferReader(JsonBuffer buffer) : this(buffer, -1)
        {
        }

        private JsonBufferReader(JsonBuffer buffer, int index)
        {
            if (buffer.IsEmpty)
            {
                throw new ArgumentException("buffer");
            }
            this._buffer = buffer;
            this._index = index;
        }

        public JsonBuffer BufferValue()
        {
            if (base.EOF)
            {
                return JsonBuffer.Empty;
            }
            JsonTokenClass tokenClass = base.TokenClass;
            if (tokenClass.IsTerminator || (tokenClass == JsonTokenClass.Member))
            {
                this.Read();
            }
            int start = this._index;
            base.Skip();
            return this._buffer.Slice(start, this._index);
        }

        protected override JsonToken ReadTokenImpl()
        {
            if (this._buffer.IsStructured)
            {
                if (++this._index < this._buffer.Length)
                {
                    return this._buffer[this._index];
                }
            }
            else
            {
                switch (++this._index)
                {
                    case 0:
                        return JsonToken.Array();

                    case 1:
                        return this._buffer[0];

                    case 2:
                        return JsonToken.EndArray();
                }
            }
            return JsonToken.EOF();
        }
    }
}

