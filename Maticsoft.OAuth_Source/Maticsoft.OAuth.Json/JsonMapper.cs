namespace Maticsoft.OAuth.Json
{
    using System;
    using System.Collections.Generic;

    public class JsonMapper
    {
        private IDictionary<Type, IJsonDeserializer> deserializers = new Dictionary<Type, IJsonDeserializer>();
        private IDictionary<Type, IJsonSerializer> serializers = new Dictionary<Type, IJsonSerializer>();

        public bool CanDeserialize(Type type)
        {
            if (!(type == typeof(JsonValue)) && !this.deserializers.ContainsKey(type))
            {
                return false;
            }
            return true;
        }

        public bool CanSerialize(Type type)
        {
            if (!(type == typeof(JsonValue)) && !this.serializers.ContainsKey(type))
            {
                return false;
            }
            return true;
        }

        public T Deserialize<T>(JsonValue value)
        {
            IJsonDeserializer deserializer;
            if (!this.deserializers.TryGetValue(typeof(T), out deserializer))
            {
                throw new JsonException(string.Format("Could not find deserializer for type '{0}'.", typeof(T)));
            }
            return (T) deserializer.Deserialize(value, this);
        }

        public void RegisterDeserializer(Type type, IJsonDeserializer deserializer)
        {
            this.deserializers[type] = deserializer;
        }

        public void RegisterSerializer(Type type, IJsonSerializer serializer)
        {
            this.serializers[type] = serializer;
        }

        public JsonValue Serialize(object obj)
        {
            IJsonSerializer serializer;
            Type key = obj.GetType();
            if (!this.serializers.TryGetValue(key, out serializer))
            {
                throw new JsonException(string.Format("Could not find serializer for type '{0}'.", key));
            }
            return serializer.Serialize(obj, this);
        }
    }
}

