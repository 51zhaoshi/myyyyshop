namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;

    public class BooleanExporter : ExporterBase
    {
        public BooleanExporter() : base(typeof(bool))
        {
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            writer.WriteBoolean((bool) value);
        }
    }
}

