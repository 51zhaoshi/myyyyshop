namespace Maticsoft.Json
{
    using System;

    public abstract class JsonReaderBase : JsonReader
    {
        private int _depth;
        private int _maxDepth = 100;
        private JsonToken _token = JsonToken.BOF();

        protected JsonReaderBase()
        {
        }

        public sealed override bool Read()
        {
            if (!base.EOF)
            {
                if (this.Depth > this.MaxDepth)
                {
                    throw new Exception("Maximum allowed depth has been exceeded.");
                }
                if ((base.TokenClass == JsonTokenClass.EndObject) || (base.TokenClass == JsonTokenClass.EndArray))
                {
                    this._depth--;
                }
                this._token = this.ReadTokenImpl();
                if ((base.TokenClass == JsonTokenClass.Object) || (base.TokenClass == JsonTokenClass.Array))
                {
                    this._depth++;
                }
            }
            return !base.EOF;
        }

        protected abstract JsonToken ReadTokenImpl();

        public sealed override int Depth
        {
            get
            {
                return this._depth;
            }
        }

        public sealed override int MaxDepth
        {
            get
            {
                return this._maxDepth;
            }
            set
            {
                this._maxDepth = value;
            }
        }

        public sealed override JsonToken Token
        {
            get
            {
                return this._token;
            }
        }
    }
}

