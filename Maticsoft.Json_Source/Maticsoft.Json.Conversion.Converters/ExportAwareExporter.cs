namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;

    public sealed class ExportAwareExporter : ExporterBase
    {
        public ExportAwareExporter(Type type) : base(type)
        {
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            ((IJsonExportable) value).Export(context, writer);
        }
    }
}

