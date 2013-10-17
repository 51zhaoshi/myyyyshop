namespace Maticsoft.Json
{
    using System;

    public sealed class JsonBufferWriter : JsonTokenWriterBase
    {
        private readonly JsonBufferStorage _storage;

        public JsonBufferWriter() : this(0x10)
        {
        }

        public JsonBufferWriter(int initialCapacity)
        {
            if (initialCapacity < 0)
            {
                throw new ArgumentOutOfRangeException("initialCapacity", initialCapacity, null);
            }
            this._storage = new JsonBufferStorage(initialCapacity);
        }

        public JsonBuffer GetBuffer()
        {
            if (this.Depth > 0)
            {
                base.AutoComplete();
            }
            return this._storage.ToBuffer();
        }

        protected override void Write(JsonToken token)
        {
            this._storage.Write(token);
        }
    }
}

