namespace Maticsoft.OAuth.Json
{
    using Maticsoft.OAuth.Util;
    using System;
    using System.Collections.Generic;

    public class JsonObject : JsonValue
    {
        public JsonObject() : base(JsonValue.JsonValueType.Object, new Dictionary<string, JsonValue>())
        {
        }

        public void AddValue(string name, JsonValue value)
        {
            ArgumentUtils.AssertNotNull(name, "name");
            ArgumentUtils.AssertNotNull(value, "value");
            IDictionary<string, JsonValue> dictionary = (IDictionary<string, JsonValue>) base.value;
            if (dictionary.ContainsKey(name))
            {
                throw new JsonException(string.Format("An entry with the name '{0}' already exists in the JSON object structure.", name));
            }
            dictionary.Add(name, value);
        }

        public override bool ContainsName(string name)
        {
            return ((IDictionary<string, JsonValue>) base.value).ContainsKey(name);
        }

        public override ICollection<string> GetNames()
        {
            return ((IDictionary<string, JsonValue>) base.value).Keys;
        }

        public override JsonValue GetValue(string name)
        {
            JsonValue value2;
            ArgumentUtils.AssertNotNull(name, "name");
            IDictionary<string, JsonValue> dictionary = (IDictionary<string, JsonValue>) base.value;
            if (dictionary.TryGetValue(name, out value2))
            {
                return value2;
            }
            return null;
        }

        public override ICollection<JsonValue> GetValues()
        {
            return ((IDictionary<string, JsonValue>) base.value).Values;
        }
    }
}

