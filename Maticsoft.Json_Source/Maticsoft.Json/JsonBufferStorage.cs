namespace Maticsoft.Json
{
    using System;
    using System.Reflection;

    internal sealed class JsonBufferStorage
    {
        private int _count;
        private JsonToken[] _tokens;

        internal JsonBufferStorage(int initialCapacity)
        {
            if (initialCapacity > 0)
            {
                this._tokens = new JsonToken[initialCapacity];
            }
        }

        public JsonBuffer ToBuffer()
        {
            return new JsonBuffer(this, 0, this._count);
        }

        public JsonBufferStorage Write(params JsonToken[] tokens)
        {
            foreach (JsonToken token in tokens)
            {
                this.Write(token);
            }
            return this;
        }

        public JsonBufferStorage Write(JsonToken token)
        {
            if (this._tokens == null)
            {
                this._tokens = new JsonToken[0x10];
            }
            else if (this._count == this._tokens.Length)
            {
                JsonToken[] array = new JsonToken[this._tokens.Length * 2];
                this._tokens.CopyTo(array, 0);
                this._tokens = array;
            }
            this._tokens[this._count++] = token;
            return this;
        }

        public JsonToken this[int index]
        {
            get
            {
                return this._tokens[index];
            }
        }

        public int Length
        {
            get
            {
                return this._count;
            }
        }
    }
}

