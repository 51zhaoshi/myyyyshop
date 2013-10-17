namespace Maticsoft.Json.Conversion
{
    using Maticsoft.Json;
    using System;
    using System.Runtime.CompilerServices;

    public delegate void JsonExportHandler(object value, JsonWriter writer);
}

