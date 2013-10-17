namespace Maticsoft.Json.Conversion
{
    using Maticsoft.Json;
    using System;

    public interface IExporter
    {
        void Export(ExportContext context, object value, JsonWriter writer);

        Type InputType { get; }
    }
}

