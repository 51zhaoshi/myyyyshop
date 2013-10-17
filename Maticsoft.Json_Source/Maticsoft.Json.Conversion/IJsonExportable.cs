namespace Maticsoft.Json.Conversion
{
    using Maticsoft.Json;
    using System;

    public interface IJsonExportable
    {
        void Export(ExportContext context, JsonWriter writer);
    }
}

