namespace Maticsoft.Json
{
    using System;

    [Serializable]
    public sealed class JsonRecorder : JsonWriterBase
    {
        private int _count;
        private JsonToken[] _tokens;

        public JsonReader CreatePlayer()
        {
            if ((this.Bracket != JsonWriterBracket.Pending) && (this.Bracket != JsonWriterBracket.Closed))
            {
                throw new InvalidOperationException("JSON data cannot be read before it is complete.");
            }
            JsonToken[] destinationArray = new JsonToken[this._count + 2];
            if (this._count > 0)
            {
                Array.Copy(this._tokens, 0, destinationArray, 1, this._count);
            }
            destinationArray[0] = JsonToken.BOF();
            destinationArray[destinationArray.Length - 1] = JsonToken.EOF();
            return new JsonPlayer(destinationArray);
        }

        public void Playback(JsonWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            writer.WriteFromReader(this.CreatePlayer());
        }

        public static JsonRecorder Record(JsonReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            JsonRecorder recorder = new JsonRecorder();
            recorder.WriteFromReader(reader);
            return recorder;
        }

        private void Write(JsonToken token)
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
        }

        protected override void WriteBooleanImpl(bool value)
        {
            this.Write(JsonToken.Boolean(value));
        }

        protected override void WriteEndArrayImpl()
        {
            this.Write(JsonToken.EndArray());
        }

        protected override void WriteEndObjectImpl()
        {
            this.Write(JsonToken.EndObject());
        }

        protected override void WriteMemberImpl(string name)
        {
            this.Write(JsonToken.Member(name));
        }

        protected override void WriteNullImpl()
        {
            this.Write(JsonToken.Null());
        }

        protected override void WriteNumberImpl(string value)
        {
            this.Write(JsonToken.Number(value));
        }

        protected override void WriteStartArrayImpl()
        {
            this.Write(JsonToken.Array());
        }

        protected override void WriteStartObjectImpl()
        {
            this.Write(JsonToken.Object());
        }

        protected override void WriteStringImpl(string value)
        {
            this.Write(JsonToken.String(value));
        }

        [Serializable]
        private sealed class JsonPlayer : JsonReaderBase
        {
            private int _index;
            private readonly JsonToken[] _tokens;

            public JsonPlayer(JsonToken[] tokens)
            {
                this._tokens = tokens;
            }

            protected override JsonToken ReadTokenImpl()
            {
                return this._tokens[++this._index];
            }
        }
    }
}

