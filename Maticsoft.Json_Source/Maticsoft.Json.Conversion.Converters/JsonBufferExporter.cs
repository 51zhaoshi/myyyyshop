namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;

    public class JsonBufferExporter : ExporterBase
    {
        public JsonBufferExporter() : base(typeof(JsonBuffer))
        {
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            writer.WriteFromReader(((JsonBuffer) value).CreateReader());
        }
    }
}

