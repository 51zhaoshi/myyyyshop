namespace Maticsoft.Json.Conversion
{
    using Maticsoft.Json;
    using System;

    public interface IObjectMemberExporter
    {
        void Export(ExportContext context, JsonWriter writer, object source);
    }
}

