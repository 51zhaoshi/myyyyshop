namespace Maticsoft.Json
{
    using System;

    public abstract class JsonTokenWriterBase : JsonWriterBase
    {
        protected JsonTokenWriterBase()
        {
        }

        protected abstract void Write(JsonToken token);
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
    }
}

