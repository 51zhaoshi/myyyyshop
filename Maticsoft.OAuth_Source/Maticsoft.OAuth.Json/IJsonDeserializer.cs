namespace Maticsoft.OAuth.Json
{
    using System;

    public interface IJsonDeserializer
    {
        object Deserialize(JsonValue value, JsonMapper mapper);
    }
}

