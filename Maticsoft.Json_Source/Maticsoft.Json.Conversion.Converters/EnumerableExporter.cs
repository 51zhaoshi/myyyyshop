namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.Collections;

    public sealed class EnumerableExporter : ExporterBase
    {
        public EnumerableExporter(Type inputType) : base(inputType)
        {
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            IEnumerable enumerable = (IEnumerable) value;
            writer.WriteStartArray();
            foreach (object obj2 in enumerable)
            {
                context.Export(obj2, writer);
            }
            writer.WriteEndArray();
        }
    }
}

