namespace Maticsoft.Json.Conversion
{
    using Maticsoft.Json;
    using System;

    public interface IJsonImportable
    {
        void Import(ImportContext context, JsonReader reader);
    }
}

