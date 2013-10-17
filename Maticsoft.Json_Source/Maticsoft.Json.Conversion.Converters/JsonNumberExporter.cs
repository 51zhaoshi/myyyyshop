namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;

    public sealed class JsonNumberExporter : ExporterBase
    {
        public JsonNumberExporter() : base(typeof(JsonNumber))
        {
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            writer.WriteNumber(((JsonNumber) value).ToString());
        }
    }
}

