namespace Maticsoft.Json
{
    using System;
    using System.Collections;

    public class JsonImportingWriter : JsonWriterBase
    {
        private JsonArray _array;
        private string _member;
        private readonly Stack _memberStack = new Stack();
        private JsonObject _object;
        private object _value;
        private readonly Stack _valueStack = new Stack();

        private void Pop()
        {
            object obj2 = this._value;
            object obj3 = this._valueStack.Pop();
            this._member = (string) this._memberStack.Pop();
            if (obj3 != null)
            {
                this._object = obj3 as JsonObject;
                this._array = (this._object == null) ? ((JsonArray) obj3) : null;
                this._value = obj3;
                this.WriteValue(obj2);
            }
        }

        private void Push()
        {
            this._valueStack.Push(this._value);
            this._memberStack.Push(this._member);
            this._array = null;
            this._object = null;
            this._value = null;
            this._member = null;
        }

        protected override void WriteBooleanImpl(bool value)
        {
            this.WriteValue(value);
        }

        protected override void WriteEndArrayImpl()
        {
            this.Pop();
        }

        protected override void WriteEndObjectImpl()
        {
            this.Pop();
        }

        protected override void WriteMemberImpl(string name)
        {
            this._member = name;
        }

        protected override void WriteNullImpl()
        {
            this.WriteValue(null);
        }

        protected override void WriteNumberImpl(string value)
        {
            this.WriteValue(new JsonNumber(value));
        }

        protected override void WriteStartArrayImpl()
        {
            this.Push();
            this._value = this._array = new JsonArray();
        }

        protected override void WriteStartObjectImpl()
        {
            this.Push();
            this._value = this._object = new JsonObject();
        }

        protected override void WriteStringImpl(string value)
        {
            this.WriteValue(value);
        }

        private void WriteValue(object value)
        {
            if (this.IsObject)
            {
                this._object[this._member] = value;
                this._member = null;
            }
            else
            {
                this._array.Add(value);
            }
        }

        public bool IsArray
        {
            get
            {
                return (this._array != null);
            }
        }

        public bool IsObject
        {
            get
            {
                return (this._object != null);
            }
        }

        public object Value
        {
            get
            {
                return this._value;
            }
        }
    }
}

