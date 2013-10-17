namespace Maticsoft.Json
{
    using Maticsoft.Json.Diagnostics;
    using System;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct JsonToken
    {
        private readonly JsonTokenClass _class;
        private readonly string _text;
        private JsonToken(JsonTokenClass clazz) : this(clazz, null)
        {
        }

        private JsonToken(JsonTokenClass clazz, string text)
        {
            this._class = clazz;
            this._text = text;
        }

        public JsonTokenClass Class
        {
            get
            {
                return this._class;
            }
        }
        public string Text
        {
            get
            {
                return this._text;
            }
        }
        public override string ToString()
        {
            if (this.Text != null)
            {
                return (this.Class.Name + ":" + DebugString.Format(this.Text));
            }
            return this.Class.Name;
        }

        public override int GetHashCode()
        {
            return (this.Class.GetHashCode() ^ ((this.Text == null) ? 0 : this.Text.GetHashCode()));
        }

        public override bool Equals(object obj)
        {
            return (((obj != null) && (obj is JsonToken)) && this.Equals((JsonToken) obj));
        }

        public bool Equals(JsonToken other)
        {
            if (!this.Class.Equals(other.Class))
            {
                return false;
            }
            if (this.Text != null)
            {
                return this.Text.Equals(other.Text);
            }
            return true;
        }

        public static JsonToken Null()
        {
            return new JsonToken(JsonTokenClass.Null, "null");
        }

        public static JsonToken Array()
        {
            return new JsonToken(JsonTokenClass.Array);
        }

        public static JsonToken EndArray()
        {
            return new JsonToken(JsonTokenClass.EndArray);
        }

        public static JsonToken Object()
        {
            return new JsonToken(JsonTokenClass.Object);
        }

        public static JsonToken EndObject()
        {
            return new JsonToken(JsonTokenClass.EndObject);
        }

        public static JsonToken BOF()
        {
            return new JsonToken(JsonTokenClass.BOF);
        }

        public static JsonToken EOF()
        {
            return new JsonToken(JsonTokenClass.EOF);
        }

        public static JsonToken String(string text)
        {
            return new JsonToken(JsonTokenClass.String, Mask.NullString(text));
        }

        public static JsonToken Boolean(bool value)
        {
            return new JsonToken(JsonTokenClass.Boolean, value ? "true" : "false");
        }

        public static JsonToken True()
        {
            return Boolean(true);
        }

        public static JsonToken False()
        {
            return Boolean(false);
        }

        public static JsonToken Number(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            if (text.Length == 0)
            {
                throw new ArgumentException("Number text cannot zero in length.", "text");
            }
            return new JsonToken(JsonTokenClass.Number, text);
        }

        public static JsonToken Member(string name)
        {
            return new JsonToken(JsonTokenClass.Member, name);
        }
    }
}

