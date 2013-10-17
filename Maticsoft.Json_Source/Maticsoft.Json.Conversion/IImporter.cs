namespace Maticsoft.Json.Conversion
{
    using Maticsoft.Json;
    using System;

    public interface IImporter
    {
        object Import(ImportContext context, JsonReader reader);

        Type OutputType { get; }
    }
}

