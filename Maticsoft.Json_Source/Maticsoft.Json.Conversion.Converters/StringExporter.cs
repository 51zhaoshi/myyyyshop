namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;

    public sealed class StringExporter : ExporterBase
    {
        public StringExporter() : this(typeof(string))
        {
        }

        public StringExporter(Type type) : base(type)
        {
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            writer.WriteString(value.ToString());
        }
    }
}

