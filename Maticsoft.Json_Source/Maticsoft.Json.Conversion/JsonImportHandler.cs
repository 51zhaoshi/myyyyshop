namespace Maticsoft.Json.Conversion
{
    using Maticsoft.Json;
    using System;
    using System.Runtime.CompilerServices;

    public delegate object JsonImportHandler(Type type, JsonReader reader);
}

