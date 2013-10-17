namespace Maticsoft.Json
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class JsonNull : IObjectReference
    {
        public const string Text = "null";
        public static readonly JsonNull Value = new JsonNull();

        private JsonNull()
        {
        }

        public static bool LogicallyEquals(object o)
        {
            return ((o == null) || (o.Equals(Value) || Convert.IsDBNull(o)));
        }

        object IObjectReference.GetRealObject(StreamingContext context)
        {
            return Value;
        }

        public override string ToString()
        {
            return "null";
        }
    }
}

