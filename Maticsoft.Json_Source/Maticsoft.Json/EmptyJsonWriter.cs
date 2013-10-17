namespace Maticsoft.Json
{
    using System;

    public sealed class EmptyJsonWriter : JsonWriterBase
    {
        protected override void WriteBooleanImpl(bool value)
        {
        }

        protected override void WriteEndArrayImpl()
        {
        }

        protected override void WriteEndObjectImpl()
        {
        }

        protected override void WriteMemberImpl(string name)
        {
        }

        protected override void WriteNullImpl()
        {
        }

        protected override void WriteNumberImpl(string value)
        {
        }

        protected override void WriteStartArrayImpl()
        {
        }

        protected override void WriteStartObjectImpl()
        {
        }

        protected override void WriteStringImpl(string value)
        {
        }
    }
}

