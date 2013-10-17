namespace Maticsoft.OAuth.Json
{
    using System;

    public interface IJsonSerializer
    {
        JsonValue Serialize(object obj, JsonMapper mapper);
    }
}

