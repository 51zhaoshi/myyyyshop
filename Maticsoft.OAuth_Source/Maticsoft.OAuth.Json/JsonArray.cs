namespace Maticsoft.OAuth.Json
{
    using Maticsoft.OAuth.Util;
    using System;
    using System.Collections.Generic;

    public class JsonArray : JsonValue
    {
        public JsonArray(params JsonValue[] values) : base(JsonValue.JsonValueType.Array, new List<JsonValue>(values))
        {
        }

        public void AddValue(JsonValue value)
        {
            ArgumentUtils.AssertNotNull(value, "value");
            ((IList<JsonValue>) base.value).Add(value);
        }

        public override JsonValue GetValue(int index)
        {
            IList<JsonValue> list = (IList<JsonValue>) base.value;
            if (index < list.Count)
            {
                return list[index];
            }
            return null;
        }

        public override ICollection<JsonValue> GetValues()
        {
            return (IList<JsonValue>) base.value;
        }
    }
}

